#include "task_conf.h"
#include "usercode.h"
#include "config.h"

extern PWMInfo_T PWMInfo;

extern DepthControlInfo DCInfo;

void Task_HandleMode_Clear(void)
{
		x_y_z_pitch = 0;
		right_rocker = 0;
		left_rocker = 0;
		Mode_control = 0;
		speed_kH = 0;
	    speed_kV = 0;
}

void Task_HandleMode_Process(HandleModeInfo HMInfo)
{
	DepthControlInfo DCInfo;
		if(!rt_strcmp(HMInfo.Change, "MS"))         //左摇杆--平移
			{
				switch((int)HMInfo.fNum[0])
				{
					case 0:{                                //右
						left_rocker = 3;    
						break;
					}
					case 1:{                                //右前
						x_y_z_pitch |= 0x40;
						x_y_z_pitch |= 0x20;
						break;
					}
					case 2:{                                //前
						x_y_z_pitch |= 0x20;
						break;
					}
					case 3:{                                //左前
						x_y_z_pitch |= 0x80;
						x_y_z_pitch |= 0x20;
						break;
					}
					case 4:{                                //左
						left_rocker = 4;
						break;
					}
					case 5:{                                //左后
						x_y_z_pitch |= 0x80;
						x_y_z_pitch |= 0x10;
						break;
					}
					case 6:{                                //后
						x_y_z_pitch |= 0x10;
						break;
					}
					case 7:{                                //右后
						x_y_z_pitch |= 0x80;
						x_y_z_pitch |= 0x10;
						break;
					}
				}			
			}
			else if(!rt_strcmp(HMInfo.Change, "MP"))         //右摇杆--左右转向--上下深度
			{
				switch((int)HMInfo.fNum[0])
				{
					case 0:{             
						x_y_z_pitch |= 0x40;               //右转
						break;
					}
					case 1:{    
						if((int)HMInfo.fNum[1])
						{
							x_y_z_pitch |= 0x08;
							break;
						}
						else
						{
							x_y_z_pitch &= ~0x08;
							DCInfo.setDepth = MS5837.fDepth;
							rt_mq_send(DepthControlmq,&DCInfo,sizeof(DepthControlInfo));
							break;
						}
					}
					case 2:{                               
						x_y_z_pitch |= 0x80;                  //左转
						break;
					}
					case 3:{         
						if((int)HMInfo.fNum[1])
						{
							x_y_z_pitch |= 0x04;
							break;
						}
						else
						{
							x_y_z_pitch &= ~0x04;
							DCInfo.setDepth = MS5837.fDepth;
							rt_mq_send(DepthControlmq,&DCInfo,sizeof(DepthControlInfo));
							break;
						}
					}
				}
			}
			else if(!rt_strcmp(HMInfo.Change, "MT0"))       //扳机
			{
				
			}
			else									        //十字键
			{
				switch((int)HMInfo.fNum[1])
				{
					case 0:{							//针筒抽
						//right_rocker = 1;
						break;
					}
					case 1:{							//下机械臂上
						right_rocker = 1;
						break;
					}
					case 2:{								//针筒压
						//left_rocker = 1;
						break;
					}
					case 3:{								//下机械臂下
						right_rocker = 2;
						break;
					}
					case 4:{								//摄像机云台下
						right_rocker = 4;
						break;
					}
					case 5:{								//pitch仰
						x_y_z_pitch |= 0x02;
						break;
					}
					case 6:{								//pitch俯
						x_y_z_pitch |= 0x01;
						break;
					}
					case 7:{								//摄像机云台上
						right_rocker = 3;
						break;
					}
					case 8:{								//慢速状态切换
					//奇偶计次
						SpeedMode = (SpeedMode + 1) % 2;  // 0 ⇄ 1
						break;
					}
					case 9:{								//定深开关键
						//计数器
						if(((DepthFlag >> 1 ) % 2) == 0)//如果第一位是0
							{
								DepthFlag |= 0x02;
							}//设置为1
						else
						{
							DepthFlag &= ~0x02;//清除
							PIDOut = 0.0;
							DCInfo.setDepth = MS5837.fDepth;
							rt_mq_send(DepthControlmq,&DCInfo,sizeof(DepthControlInfo));
						}
						break;

					}
					case 10:{								//上机械臂夹取
						Mode_control |= 2;
						break;
					}
					case 11:{								//上机械臂夹取
						Mode_control |= 4;
						break;
					}
					case 12:{								//左摇杆按下（待定）
						right_rocker = 0;
						break;
					}
					case 13:{								//右摇杆按下（待定）
						left_rocker = 0;
						break;
					}
				}
			}
		}

