#include "task_conf.h"
#include "usercode.h"
#include "config.h"
/*舵机初始化函数*/
void Servo_Init(PWMInfo_T *t_PWM)
{
	t_PWM -> PWMout[claw_servo_syringe] = 1000;
	t_PWM -> PWMout[claw_servo_shoulder] = 1900;
	t_PWM -> PWMout[claw_servo_up] = 1900;
	t_PWM -> PWMout[claw_servo_gimbal] = 1500;
	Mode_control |= 0x80;      //Mode_control第八位为标志位初始位	
}


/*舵机改值配置函数*/
void Servo_Write(PWMInfo_T *t_PWM)
{
    switch (Mode_control)
    {
        // 注射器推杆控制
        case 1:
            t_PWM->PWMout[claw_servo_syringe] += CLAW_STEP;
            break;
        case 3:
            t_PWM->PWMout[claw_servo_syringe] -= CLAW_STEP;
            break;

        // 机械臂大臂角度控制
        case 4:
            t_PWM->PWMout[claw_servo_shoulder] -= CLAW_STEP;
            break;
        case 2:
            t_PWM->PWMout[claw_servo_shoulder] += CLAW_STEP;
            break;

        // 摄像头云台角度控制
        case 5:
            t_PWM->PWMout[claw_servo_gimbal] += CLAW_STEP;
            break;
        case 6:
            t_PWM->PWMout[claw_servo_gimbal] -= CLAW_STEP;
            break;

        // 顶部爪箱控制
        case 7:
            t_PWM->PWMout[claw_servo_up] = 1300;
            break;
        case 8:
            t_PWM->PWMout[claw_servo_up] = 1900;
            break;

        default:
            break;
    }
}


/*舵机限幅操作函数*/
void Servo_Limit(PWMInfo_T *t_PWM)
{   
	//注射器
	if(t_PWM->PWMout[claw_servo_syringe] < 800 )   
	t_PWM->PWMout[claw_servo_syringe] = 800;
    
	if(t_PWM->PWMout[claw_servo_syringe] > 2500) 
	t_PWM->PWMout[claw_servo_syringe] = 2500;
    //小臂
	if(t_PWM->PWMout[claw_servo_shoulder] < 1000) 
	t_PWM->PWMout[claw_servo_shoulder] = 1000;  

	if(t_PWM->PWMout[claw_servo_shoulder] > 2500) 
	t_PWM->PWMout[claw_servo_shoulder] = 2500;
    //云台
	if(t_PWM->PWMout[claw_servo_gimbal] > 1790) 
	t_PWM->PWMout[claw_servo_gimbal] = 1790;

	if(t_PWM->PWMout[claw_servo_gimbal] < 1450) 
	t_PWM->PWMout[claw_servo_gimbal] = 1450;
}
