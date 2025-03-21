#ifndef __THREADPOOL_H_
#define __THREADPOOL_H_

#include "stdint.h"

//推进器宏定义
#define v_wheel1_speed   3       	//水平推进器   黑C9 右后
#define v_wheel2_speed   1       	//水平推进器   灰C8 右前
#define v_wheel3_speed   2       	//水平推进器   白B1 左后
#define v_wheel4_speed   0       	//水平推进器   紫B0 左前
	
#define h_wheel1_speed   4       	//左前垂直推进器B6
#define h_wheel2_speed   5       	//右前垂直推进器B7
#define h_wheel3_speed   6       	//左后垂直推进器B8
#define h_wheel4_speed   7       	//右后垂直推进器B9

#define claw_servo_syringe  9       //注射器推进舵机E9
#define claw_servo_shoulder 8       //机械爪小臂旋转舵机E13
#define claw_servo_up  		11      //上机械爪抓取舵机E14
#define claw_servo_gimbal   10      //摄像头云台
//PWM结构体
typedef struct
{
    uint16_t PWMout[12];
}PWMInfo_T;

/* 函数声明 */
void DataFromIPC(void* paramenter);
void JY901SReadThread(void* paramenter);
void circle_conduct(void);
void MS5837ReadThread(void* paramenter);
void HANDLE_MODE(void* paramenter);
void AUTO_MODE(void* paramenter);
void DepthControl(void* paramenter);
void PlusControl(void* paramenter);
void ReportPWMout(void* paramenter);
void TestThread(void* paramenter);
void MotionControl(void* paramenter);
void pH_outcome(void* paramenter);
#endif
