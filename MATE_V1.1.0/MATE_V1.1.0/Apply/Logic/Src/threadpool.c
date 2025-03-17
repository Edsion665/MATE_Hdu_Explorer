#include "usercode.h"		/* usercode头文件 */
#include "threadpool.h"		/* threadpool头文件 */
#include "drv_hal_conf.h"   /* SGA库头文件配置 */
#include "task_conf.h"      /* task层头文件配置 */
#include "ocd_conf.h"       /* OCD层头文件配置 */
#include "dev_conf.h"		/* Dev层头文件配置 */
#include "algo_conf.h"		/* Algo层头文件配置 */
#include "config.h"			/* I/O配置头文件配置 */

//存储8个推进器的数据和3个舵机和探照灯的数据
PWMInfo_T PWMInfo = {1500,1500,1500,1500,1500,1500,1500,1500,1500,1500,1500,1100};

/* 读取上位机数据线程 */
void DataFromIPC(void* paramenter)
{
    uint8_t ReceBuf[100];
    uint8_t ReceNum = 0;

    while(1)
    {
        if(rt_sem_take(DataFromIPC_Sem,RT_WAITING_FOREVER) == RT_EOK)
        {
            //平时调试使用串口3，正式使用采用串口1,修改PRINTF_UART宏即可
            if(PRINTF_UART == USART3)
                ReceNum = Drv_Uart_Receive_DMA(&Uart3,ReceBuf);
            else if(PRINTF_UART == USART1)
                ReceNum = Drv_Uart_Receive_DMA(&Uart1,ReceBuf);
            
            if(ReceNum != 0)
            
            {
                //printf("%s\r\n",ReceBuf);
                //分析数据
                Task_AnalysisData(ReceBuf);

                //将临时变量清0
                rt_memset(ReceBuf,0,ReceNum);
                ReceNum = 0;
            }
        }
        Drv_Delay_Ms(1);    //让出CPU资源给低优先级线程
    }
}

/* 读取JY901S数据线程 */
void JY901SReadThread(void* paramenter)
{
    while(1)
    {
        //如果获取到信号量，说明接收到数据
		if(rt_sem_take(JY901S_Sem,RT_WAITING_FOREVER) == RT_EOK)
		{
            //如果成功处理完成数据
			if(OCD_JY901_DataProcess(&JY901S))
            {
                //数据转换
                OCD_JY901_DataConversion(&JY901S);
                //打印角速度和加速度
                 //OCD_JY901_Printf(&JY901S);
            }
		}
        Drv_Delay_Ms(100);    //让出CPU资源给其它线程
		rt_thread_yield();
    }
}

/* 读取MS5837数据线程▼ */
void MS5837ReadThread(void* paramenter)
{
    DepthControlInfo DCInfo;
    while(1)
    {
        OCD_MS5837_GetData(&MS5837);
        if(MS5837.fDepth == 153150.250000)  //未接MS5837的错误数据
            MS5837.fDepth = 0;
        //printf("M %0.2f\r\n",MS5837.fDepth);
        // printf("T %0.2f\r\n",MS5837.fTemperature);
				float DepthFliter = Median_Flitering_Output(MS5837.fDepth);
        if(cnt == 0)	//开机时把当前位置置为目标位置
        {
            cnt = 1;
            DCInfo.setDepth = MS5837.fDepth;
            rt_mq_send(DepthControlmq,&DCInfo,sizeof(DepthControlInfo));
        }
        Drv_Delay_Ms(60);
    }
}

/* 手柄控制线程 */
void HANDLE_MODE(void* parameter)
{
    HandleModeInfo HMInfo;
    AutoModeInfo ClearBuf;
    // 设置一个合理的超时时间，比如 100ms 参数可以根据实际测试整定
    const rt_tick_t timeout = rt_tick_from_millisecond(100);

    while (1)
    {
        if(rt_mq_recv(HandleModemq, &HMInfo, sizeof(HandleModeInfo), timeout) == RT_EOK)
        {
            // 收到数据，处理消息
            if(!rt_strcmp(HMInfo.ModeChange, "AUTO START"))
            {
                //将AutoModemq消息队列中所有内容清空
				while(1)
                {
                    if(rt_mq_recv(AutoModemq,&ClearBuf,sizeof(AutoModeInfo),RT_WAITING_NO) != RT_EOK)
                        break;
                }
                Task_Thruster_AllStop();
                printf("Switch to AUTO Mode\r\n");
                rt_enter_critical();
                rt_thread_suspend(rt_thread_self());
                rt_thread_resume(thread5);
                rt_exit_critical();
                rt_schedule();
            }
            else
            {
                Task_HandleMode_Process(HMInfo);
                // 可选：短暂延时给其它任务执行机会
                rt_thread_mdelay(5);
            }
        }
        else // 超时：没有收到消息
        {
            // 当上位机松手不再发数据时，
            // 此处执行清零或停止控制动作
            Task_HandleMode_Clear();
            // 如果需要，也可以额外停止推进器等
            // 这时 HANDLE_MODE 会继续阻塞等待新数据
        }
    }
}

/* 运动控制主线程 */
void MotionControl(void* paramenter)
{
    while(1)
    {
		Task_Motion_Process();
        Drv_Delay_Ms(100);
    }
}

/* 自动控制线程 */
void AUTO_MODE(void* paramenter)
{
    AutoModeInfo AMInfo;
    HandleModeInfo ClearBuf;

    //挂起
    rt_thread_suspend(rt_thread_self());
    rt_schedule();
    while(1)
    {
        //自动控制消息队列接收到数据，将切换到手柄模式
        if(!rt_strcmp(AMInfo.ModeChange,"HANDLE START"))
        {
            //将HandleModemq队列中所有内容清空
            while(1)
            {
                if(rt_mq_recv(HandleModemq,&ClearBuf,sizeof(HandleModeInfo),RT_WAITING_NO) != RT_EOK)
                    break;
            }
            Task_Thruster_AllStop();                //所有推进器停转

            printf("Switch to HANDLE Mode\r\n");
            rt_enter_critical();                    //调度器上锁
            rt_thread_suspend(rt_thread_self());    //挂起本线程
            rt_thread_resume(thread4);              //恢复手动控制线程
            rt_exit_critical();                     //调度器解锁
            rt_schedule();                          //立即执行一次调度
        }
        else
        {
            //自动模式处理函数，根据消息队列中传来的黑线角度改变推进器PWM
            Task_AutoMode_Process(AMInfo);
        }

        // printf("AUTO\r\n");
        // Drv_Delay_Ms(1000);
        Drv_Delay_Ms(1);    //让出CPU资源给低优先级线程
    }
}

/* 定深控制 */
void DepthControl(void* paramenter)
{
    DepthControlInfo DCInfo;
    float ExpDepth = 0.0f;
    float CurrDepth = 0.0f;

    while(1)
    {
        //定深数据消息队列接收到数据，将开始定深控制
        if(rt_mq_recv(DepthControlmq,&DCInfo,sizeof(DepthControlInfo),RT_WAITING_NO) == RT_EOK)
        {
            ExpDepth = DCInfo.setDepth;
            //printf("Exp:%0.2f\r\n",ExpDepth);
        }

        //printf("Exp:%0.2f\r\n",ExpDepth);
        //获取当前深度
        CurrDepth = MS5837.fDepth;
        //定深控制函数，开关打开开启定深
        if(DepthFlag & 0x02)
			task_DepthControl_Process(CurrDepth,ExpDepth);
		
		printf("%f",PIDOut);

        Drv_Delay_Ms(60);    //每隔一段时间进行一次定深
    }
}


/*俯仰、机械爪及探照灯*/
void PlusControl(void* paramenter)
{
	PWMInfo.PWMout[claw_shouder_Speed] = 1000;
	PWMInfo.PWMout[claw_elbow_Speed] = Servo_Angle_To_HightTime(CLAW_CATCH_STOP_ANGLE);
	PWMInfo.PWMout[claw_catchSpeed] = 1900;
    PWMInfo.PWMout[light_level_1] = 1500;
	Mode_control |= 0x80; //Mode_control第八位为未初始化标志位
    while(1)                                                                    
    {
		//左摇杆上下控制大臂
		if(left_rocker == 0)
        {
			;
        }
		else if(left_rocker == 2)
		{
			PWMInfo.PWMout[claw_shouder_Speed] += 5;
			if(PWMInfo.PWMout[claw_shouder_Speed] > 1170)
            PWMInfo.PWMout[claw_shouder_Speed] = 1170;
			if(PWMInfo.PWMout[claw_shouder_Speed] > 2500) 
            PWMInfo.PWMout[claw_shouder_Speed] = 2500;
			if(PWMInfo.PWMout[claw_shouder_Speed] < 500) 
            PWMInfo.PWMout[claw_shouder_Speed] = 500;
		}
		else if(left_rocker == 1)
		{
			PWMInfo.PWMout[claw_shouder_Speed] -= 5;
			if(PWMInfo.PWMout[claw_shouder_Speed] < 800 )
            PWMInfo.PWMout[claw_shouder_Speed] = 800;
			if(PWMInfo.PWMout[claw_shouder_Speed] > 2500) 
            PWMInfo.PWMout[claw_shouder_Speed] = 2500;
			if(PWMInfo.PWMout[claw_shouder_Speed] < 500) 
            PWMInfo.PWMout[claw_shouder_Speed] = 500;
		}
        else{ ;}

        //右摇杆上下控制小臂
        if(right_rocker == 0)
        { 
			
        }
		else if(right_rocker == 1)
		{
			PWMInfo.PWMout[claw_elbow_Speed] -= CLAW_STEP+8;
			if(PWMInfo.PWMout[claw_elbow_Speed] > 2500) 
            PWMInfo.PWMout[claw_elbow_Speed] = 2500;
			if(PWMInfo.PWMout[claw_elbow_Speed] < 500) 
            PWMInfo.PWMout[claw_elbow_Speed] = 500;
		}
		else if(right_rocker == 2)
		{
			PWMInfo.PWMout[claw_elbow_Speed] += CLAW_STEP+8;
			if(PWMInfo.PWMout[claw_elbow_Speed] > 2500) 
            PWMInfo.PWMout[claw_elbow_Speed] = 2500;
			if(PWMInfo.PWMout[claw_elbow_Speed] < 500) 
            PWMInfo.PWMout[claw_elbow_Speed] = 500;
		}    
        //摄像头云台
        else if(right_rocker == 3)
        {
            PWMInfo.PWMout[light_level_1] += 15;
            if(PWMInfo.PWMout[light_level_1] > 1790) 
            PWMInfo.PWMout[light_level_1] = 1790;
            // right_rocker = 0;
        }
        else if(right_rocker == 4)
        {
            PWMInfo.PWMout[light_level_1] -= 15;
            if(PWMInfo.PWMout[light_level_1] < 1450) 
            PWMInfo.PWMout[light_level_1] = 1450;
            // right_rocker = 0;
        }
        else{ ;}

		//RB和LB控制夹和松
		if(Mode_control & 0x02)
		{
//			PWMInfo.PWMout[claw_catchSpeed] -= CLAW_STEP+5;
//			if(PWMInfo.PWMout[claw_catchSpeed] < 1300) 
            PWMInfo.PWMout[claw_catchSpeed] = 1300;

		}
		else if(Mode_control & 0x04)
		{
//			PWMInfo.PWMout[claw_catchSpeed] += CLAW_STEP+5;
//			if(PWMInfo.PWMout[claw_catchSpeed] > 1900) 
            PWMInfo.PWMout[claw_catchSpeed] = 1900;
			// if(PWMInfo.PWMout[claw_catchSpeed] > 2500) 
            // PWMInfo.PWMout[claw_catchSpeed] = 2500;
			// if(PWMInfo.PWMout[claw_catchSpeed] < 500) 
            // PWMInfo.PWMout[claw_catchSpeed] = 500;
		}
		else{ ;}
		

        //调试用
        //printf("Y %d %d %d\r\n",PWMInfo.PWMout[claw_shouder_Speed],PWMInfo.PWMout[claw_elbow_Speed],PWMInfo.PWMout[claw_catchSpeed]);
		//printf("L %d\r\n",PWMInfo.PWMout[light_level_1]);
        //写入机械臂及灯光pwm值
        Task_Servo_AllStart(PWMInfo.PWMout);
		//延时
		Drv_Delay_Ms(100);
    }
}

/* 汇报PWMout值 */
void ReportPWMout(void* paramenter)
{
    while(1)
    {
    //   printf("h: %d %d %d %d\r\n",
	// 		PWMInfo.PWMout[h_wheel1_speed],
	// 		PWMInfo.PWMout[h_wheel2_speed],
	// 		PWMInfo.PWMout[h_wheel3_speed],
	// 		PWMInfo.PWMout[h_wheel4_speed] );
			
	// 		printf("v: %d %d %d %d\r\n \r\n",
	// 		PWMInfo.PWMout[v_wheel1_speed],
	// 		PWMInfo.PWMout[v_wheel2_speed],
	// 		PWMInfo.PWMout[v_wheel3_speed],
	// 		PWMInfo.PWMout[v_wheel4_speed] );
    //     printf("big:%d small:%d zhua:%d camera:%d\r\n",
    //              PWMInfo.PWMout[claw_shouder_Speed],
    //              PWMInfo.PWMout[claw_elbow_Speed],
    //              PWMInfo.PWMout[claw_catchSpeed],
    //              PWMInfo.PWMout[light_level_1]);
        Drv_Delay_Ms(500);    
    }
}

/* ph测试线程 */
void pH_outcome(void* paramenter)
{  
	//挂起
    rt_thread_suspend(rt_thread_self());
    rt_schedule();
	
    Get_pH_Value(&pH_adc);
    Drv_Delay_Ms(1000);
}

/* 测试线程 */
void TestThread(void* paramenter)
{
		Drv_GPIO_Reset(&demoGPIO[2]);
		Drv_Delay_Ms(1000);
		Drv_GPIO_Set(&demoGPIO[2]);
		Drv_Delay_Ms(1000);
}

