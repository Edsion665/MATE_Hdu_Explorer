#include "task_conf.h"
#include "usercode.h"
#include "config.h"

extern PWMInfo_T PWMInfo;
extern DepthControlInfo DCInfo;

void HandleMode_data_handle()
{
	//各方向速度分量
	short x_Speed = 0;
	short y_Speed = 0;
	short YAWspeed = 0;
	short z_Speed = 0;
	short Basic_Wheel_Speed =0;
	// short z_Speed = 0;
	
	//根据储存按键数据和扳机速度系数处理出各方向速度分量的大小
	//按键
	//慢速（仅前进后退左右）
	// if((x_y_z_pitch & 0x80) && (SpeedMode == 1))
	// 	x_Speed += SLOW_SPEED;
	// if((x_y_z_pitch & 0x40) && (SpeedMode == 1))
	//     x_Speed -= SLOW_SPEED;
	// if((x_y_z_pitch & 0x20) && (SpeedMode == 1))
	// 	y_Speed += SLOW_SPEED;
	// if((x_y_z_pitch & 0x10) && (SpeedMode == 1))
	//     y_Speed -= SLOW_SPEED;
	// if((left_rocker==3) && (SpeedMode == 1))
	// 	YAWspeed = SLOW_SPEED;
	// if((left_rocker==4) && (SpeedMode == 1))
	// 	YAWspeed = -SLOW_SPEED;
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
		z_Speed = 200;
    else if(x_y_z_pitch & 0x04)
	    z_Speed = -200;
	else
		z_Speed = DepthPID_out;
 
	//计算推进器推力
	PWMInfo.PWMout[v_wheel1_speed] =  y_Speed -  x_Speed - YAWspeed + STOP_PWM_VALUE;
    PWMInfo.PWMout[v_wheel2_speed] =  y_Speed +  x_Speed - YAWspeed + STOP_PWM_VALUE;
	PWMInfo.PWMout[v_wheel3_speed] =  y_Speed +  x_Speed + YAWspeed + STOP_PWM_VALUE ;//左后反转，原因不明，暂定推进器原因
    PWMInfo.PWMout[v_wheel4_speed] =  y_Speed -  x_Speed + YAWspeed + STOP_PWM_VALUE;
	// 死区配置
	if(z_Speed == 0.0)
        Basic_Wheel_Speed = z_Speed + STOP_PWM_VALUE;
    if(z_Speed > 0.0)
	    Basic_Wheel_Speed = z_Speed + STOP_PWM_VALUE_F;
    if(z_Speed < 0.0)
	    Basic_Wheel_Speed = z_Speed + STOP_PWM_VALUE_B;
	

	PWMInfo.PWMout[h_wheel1_speed] += Basic_Wheel_Speed - PitchPID_out;  
    PWMInfo.PWMout[h_wheel2_speed] += Basic_Wheel_Speed - PitchPID_out; 
    PWMInfo.PWMout[h_wheel3_speed] += Basic_Wheel_Speed + PitchPID_out;
    PWMInfo.PWMout[h_wheel4_speed] += Basic_Wheel_Speed + PitchPID_out; 
}


/*反转放大*/
void Thruster_nagative_data_handle(float times)
{
	uint16_t row = 0;
	for(;row<8;row++)
	{
		if(PWMInfo.PWMout[row]<1500)
			PWMInfo.PWMout[row] = 1500-(1500-PWMInfo.PWMout[row])*times;
	}
}
/*电机限幅*/
void PWNOutput_limit(void)
{
	//PWM限幅1.8A 
	//电机最大转速，舵机写在另一处 
	//反转还会放大
    if(PWMInfo.PWMout[v_wheel1_speed] < 1300)  PWMInfo.PWMout[v_wheel1_speed] = 1300;
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

