#include "task_conf.h"
#include "usercode.h"
#include "config.h"

extern PWMInfo_T PWMInfo;
extern DepthControlInfo DCInfo;

/* 运动控制主函数 */
void Task_Motion_Process(void)
{
	//将抽屉数组计算出来，存储PWMInfo，设定PWMInfo宏观值
	HandleMode_data_handle();
	// if(BalanceFlag)
 	//根据期望角度对PWMInfo进行修改,使得两个维度的旋转保持平衡，俯仰达到预期
	// 	Balance_data_handle();
	
	//限制最大输出
	PWNOutput_limit();
	
	//将反的PWM输出增大倍数
	Thruster_nagative_data_handle(1.2);
	
	//设置推进器PWN输出
	Task_Thruster_AllStart(PWMInfo.PWMout);
}

void HandleMode_data_handle()
{
	//各方向速度分量
	short x_Speed = 0;
	short y_Speed = 0;
	short YAWspeed = 0;
	short z_Speed = 0;
	
	//根据储存按键数据处理出各方向速度分量
	//按键
	//慢速（仅前进后退左右）
	if((x_y_z_pitch & 0x80) && (SpeedMode == 1))
		x_Speed += SLOW_SPEED;
	if((x_y_z_pitch & 0x40) && (SpeedMode == 1))
	    x_Speed -= SLOW_SPEED;
	if((x_y_z_pitch & 0x20) && (SpeedMode == 1))
		y_Speed += SLOW_SPEED;
	if((x_y_z_pitch & 0x10) && (SpeedMode == 1))
	    y_Speed -= SLOW_SPEED;
	if((left_rocker==3) && (SpeedMode == 1))
		YAWspeed = SLOW_SPEED;
	if((left_rocker==4) && (SpeedMode == 1))
		YAWspeed = -SLOW_SPEED;
	//原速
	if((x_y_z_pitch & 0x80) && (!(SpeedMode == 1)))
		x_Speed += MAX_HPOWER;
	if((x_y_z_pitch & 0x40) && (!(SpeedMode == 1)))
	    x_Speed -= MAX_HPOWER;
	if((x_y_z_pitch & 0x20) && (!(SpeedMode == 1)))
		y_Speed += MAX_HPOWER;
	if((x_y_z_pitch & 0x10) && (!(SpeedMode == 1)))
	    y_Speed -= MAX_HPOWER;
	if((left_rocker==3) && (!(SpeedMode == 1)))
		YAWspeed = YAWSPEED;
	if((left_rocker==4) && (!(SpeedMode == 1)))
		YAWspeed = -YAWSPEED-5;
	if(x_y_z_pitch & 0x08)
		z_Speed = 100;
    else if(x_y_z_pitch & 0x04)
	    z_Speed = -100;
	else
		z_Speed = PIDOut;
 
	//计算推进器
	PWMInfo.PWMout[v_wheel1_speed] =  -(y_Speed -  x_Speed - YAWspeed) + STOP_PWM_VALUE;//推进器反浆
    PWMInfo.PWMout[v_wheel2_speed] =  y_Speed +  x_Speed - YAWspeed + STOP_PWM_VALUE;
	PWMInfo.PWMout[v_wheel3_speed] =  -(y_Speed +  x_Speed + YAWspeed) + STOP_PWM_VALUE ;
    PWMInfo.PWMout[v_wheel4_speed] =  y_Speed -  x_Speed + YAWspeed + STOP_PWM_VALUE;
	
	if(z_Speed == 0.0)
        PWMInfo.PWMout[h_wheel1_speed] = z_Speed + STOP_PWM_VALUE;
    if(z_Speed > 0.0)
        PWMInfo.PWMout[h_wheel1_speed] = z_Speed + STOP_PWM_VALUE_F;
    if(z_Speed < 0.0)
        PWMInfo.PWMout[h_wheel1_speed] = z_Speed + STOP_PWM_VALUE_B;
	
    PWMInfo.PWMout[h_wheel2_speed] = PWMInfo.PWMout[h_wheel1_speed];
    PWMInfo.PWMout[h_wheel3_speed] = PWMInfo.PWMout[h_wheel1_speed];
    PWMInfo.PWMout[h_wheel4_speed] = PWMInfo.PWMout[h_wheel1_speed];

}

void Balance_data_handle(void)
{
 	float rollOutput = 0,pitchOutput = 0;
 	float Curr_roll = JY901S.stcAngle.ConPitch;
 	float Curr_pitch = JY901S.stcAngle.ConRoll;
 	// float Curr_yaw = concon_YAW;
	
 	//计算每个维度的PID输出
     rollOutput = Algo_PID_Calculate(&RollPID, Curr_roll, Exp_AngleInfo.Roll);
     pitchOutput = Algo_PID_Calculate(&PitchPID, Curr_pitch, Exp_AngleInfo.Pitch);
 // 	if((!(Mode_control & 0x80))&&(Mode_control & 0x01))
 // 	{	
 // 		yawOutput = Algo_PID_Calculate(&YawPID, Curr_yaw, Exp_AngleInfo.Yaw);
 // //		printf("Y %f %f\r\n",yawOutput,Exp_AngleInfo.Yaw);
 // 	}
	
 	//根据PIDout值PWMInfo进行修改
     // PWMInfo.PWMout[v_wheel1_speed] -= yawOutput;
     // PWMInfo.PWMout[v_wheel2_speed] += yawOutput;
     // PWMInfo.PWMout[v_wheel3_speed] += yawOutput;
     // PWMInfo.PWMout[v_wheel4_speed] -= yawOutput;
	
 	PWMInfo.PWMout[h_wheel1_speed] += - pitchOutput + rollOutput; 
     PWMInfo.PWMout[h_wheel2_speed] += - pitchOutput - rollOutput; 
     PWMInfo.PWMout[h_wheel3_speed] += + pitchOutput + rollOutput; 
     PWMInfo.PWMout[h_wheel4_speed] += + pitchOutput - rollOutput; 
}

void Thruster_nagative_data_handle(float k)
{
	int i = 0;
	for(;i<8;i++)
	{
		if(PWMInfo.PWMout[i]<1500)
			PWMInfo.PWMout[i] = 1500-(1500-PWMInfo.PWMout[i])*k;
	}
}

void PWNOutput_limit(void)
{
	//PWM限幅1.5A 
	//电机最大转速，舵机写在另一处 
    if(PWMInfo.PWMout[v_wheel1_speed] < 1300)  PWMInfo.PWMout[v_wheel1_speed] = 1300;//反转还会放大
    if(PWMInfo.PWMout[v_wheel1_speed] > 1700)  PWMInfo.PWMout[v_wheel1_speed] = 1700;

    if(PWMInfo.PWMout[v_wheel2_speed] < 1300)  PWMInfo.PWMout[v_wheel2_speed] = 1300;
    if(PWMInfo.PWMout[v_wheel2_speed] > 1700)  PWMInfo.PWMout[v_wheel2_speed] = 1700;
	
    if(PWMInfo.PWMout[v_wheel3_speed] < 1300)  PWMInfo.PWMout[v_wheel3_speed] = 1300;
    if(PWMInfo.PWMout[v_wheel3_speed] > 1700)  PWMInfo.PWMout[v_wheel3_speed] = 1700;
	
    if(PWMInfo.PWMout[v_wheel4_speed] < 1300)  PWMInfo.PWMout[v_wheel4_speed] = 1300;
    if(PWMInfo.PWMout[v_wheel4_speed] > 1700)  PWMInfo.PWMout[v_wheel4_speed] = 1700;
	
    if(PWMInfo.PWMout[h_wheel1_speed] < 1300)  PWMInfo.PWMout[h_wheel1_speed] = 1300;
    if(PWMInfo.PWMout[h_wheel1_speed] > 1700)  PWMInfo.PWMout[h_wheel1_speed] = 1700;
	
    if(PWMInfo.PWMout[h_wheel2_speed] < 1300)  PWMInfo.PWMout[h_wheel2_speed] = 1300;
    if(PWMInfo.PWMout[h_wheel2_speed] > 1700)  PWMInfo.PWMout[h_wheel2_speed] = 1700;
	
    if(PWMInfo.PWMout[h_wheel3_speed] < 1300)  PWMInfo.PWMout[h_wheel3_speed] = 1300;
    if(PWMInfo.PWMout[h_wheel3_speed] > 1700)  PWMInfo.PWMout[h_wheel3_speed] = 1700;
	
    if(PWMInfo.PWMout[h_wheel4_speed] < 1300)  PWMInfo.PWMout[h_wheel4_speed] = 1300;
    if(PWMInfo.PWMout[h_wheel4_speed] > 1700)  PWMInfo.PWMout[h_wheel4_speed] = 1700;
}

