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
  	    State_control = 0;
		Light_control = 0;
		speed_kH = 0;
	    speed_kV = 0;
	    concon_YAW = 0;
	    ConnetFlag = 0;
	    Plus = 0;
        cnt = 0;
}

void Task_HandleMode_Process(HandleModeInfo HMInfo)
{
	DepthControlInfo DCInfo;
		if(!rt_strcmp(HMInfo.Change, "MS"))         //左摇杆--平移
			{
				switch((int)HMInfo.fNum[0])
				{
					case 0:{                                //右
						x_y_z_pitch |= 0x40;
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
						x_y_z_pitch |= 0x80;
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
			if(!rt_strcmp(HMInfo.Change, "MP"))         //右摇杆--左右转向--上下深度
			{
				switch((int)HMInfo.fNum[0])
				{
					case 0:{                               
						left_rocker = 3;                   //右转
						break;
					}
					case 1:{                               
						x_y_z_pitch |= 0x08;
						break;
					}
					case 2:{                               
						left_rocker = 4;                  //左转
  				//Mode_control |= 1;
						break;
					}
					case 3:{                               
						x_y_z_pitch |= 0x04;
						break;
					}
				}
			}
			
			if(!rt_strcmp(HMInfo.Change, "MT0"))       //扳机
			{
				  //HMInfo.fNum[0]；
//				Mode_control += 4;
//				Mode_control += 2;
			}
			
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
					Drv_Delay_Ms(500);
				
					break;
				}
				case 9:{								//定深开关键
					//计数器
					if(((DepthFlag >> 1 ) % 2) == 0)//如果第一位是0
						{
							DepthFlag |= 2;
						}//设置为1
					else
					{
						DepthFlag -= 2;//清除
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
	
		

////手动模式主函数
//void Task_HandleMode_Process(HandleModeInfo HMInfo)
//{

//    char key = 0;         
//    char Press = 0;           
//	
//    //存储手柄给出的按键信息和是否是按下
//    key = HMInfo.fNum[0];
//    Press = HMInfo.fNum[1];
//	
//	//信息储存
//    HandleMode_data_storage(key,Press);
//	
//}

//void HandleMode_data_storage(char key,char Press)
//{
//	DepthControlInfo DCInfo;
//	printf("key = %d , press = %d\r\n",key,Press);
//	switch (key)
//	{
//	//x_y_z_pitch
//		//x
//		case 2://左 
//			if(Press)
//			{
//				if(!(x_y_z_pitch & 0x80))
//					x_y_z_pitch += 0x80;
//			}
//			else
//			{
//				if(x_y_z_pitch & 0x80)
//					x_y_z_pitch -= 0x80;
//			}
//		break;
//			
//		case 1://右
//			if(Press)
//			{
//				if(!(x_y_z_pitch & 0x40))
//					x_y_z_pitch += 0x40;
//			}
//			else
//			{
//				if(x_y_z_pitch & 0x40)
//					x_y_z_pitch -= 0x40;
//			}
//		break;
//			
//			//y
//		case 3:
//			if(Press)
//			{
//				if(!(x_y_z_pitch & 0x20))
//					x_y_z_pitch += 0x20;
//			}
//			else
//			{
//				if(x_y_z_pitch & 0x20)
//					x_y_z_pitch -= 0x20;
//			}
//		break;
//			
//		case 0:
//			if(Press)
//			{
//				if(!(x_y_z_pitch & 0x10))
//					x_y_z_pitch += 0x10;
//			}
//			else
//			{
//				if(x_y_z_pitch & 0x10)
//					x_y_z_pitch -= 0x10;
//			}
//		break;
//			
//			//z
//		case 7://下沉
//			if(Press)
//			{
//				if(!(x_y_z_pitch & 8))
//					x_y_z_pitch += 8;
//			}
//			else
//			{
//				if(x_y_z_pitch & 8)
//					x_y_z_pitch -= 8;
//				DCInfo.setDepth = MS5837.fDepth;
//				rt_mq_send(DepthControlmq,&DCInfo,sizeof(DepthControlInfo));
//			}
//		break;
//			
//		case 4:
//			if(Press)
//			{
//				if(!(x_y_z_pitch & 4))
//					x_y_z_pitch += 4;
//			}
//			else
//			{
//				if(x_y_z_pitch & 4)
//					x_y_z_pitch -= 4;
//				DCInfo.setDepth = MS5837.fDepth;
//				rt_mq_send(DepthControlmq,&DCInfo,sizeof(DepthControlInfo));
//			}
//		break;
//			
//			//pitch俯仰
//		case 5:
//			if(Press)
//			{
//				if(!(x_y_z_pitch & 2))
//					x_y_z_pitch += 2;
//			}
//			else
//			{
//				if(x_y_z_pitch & 2)
//					x_y_z_pitch -= 2;
//			}
//		break;
//			
//		case 6:
//			if(Press)
//			{
//				if(!(x_y_z_pitch & 1))
//					x_y_z_pitch += 1;
//			}
//			else
//			{
//				if(x_y_z_pitch & 1)
//					x_y_z_pitch -= 1;
//			}
//		break;
//			
//			
//			//夹、松控制使用RB和LB
//			
//		   case 10:
//			if(Press)
//				{
//					if(!(Mode_control & 2))
//						Mode_control += 2;
//				}
//			else
//			{
//					if((Mode_control & 2))
//						Mode_control -= 2;
//			}
//	    	break;

//			case 11:
//			if(Press)
//				{
//					if(!(Mode_control & 4))
//						Mode_control += 4;
//				}
//			else
//			{
//					if((Mode_control & 4))
//						Mode_control -= 4;
//			}
//	    	break;
//			
//			case 18:
//			left_rocker = 1;
//		break;
//			
//			case 19:
//			left_rocker = 2;
//		break;
//			
//			case 20://右
//			left_rocker = 3;
//		break;
//			
//			case 21://左
//			left_rocker = 4;
//		break;
//			
//			case 23:
//				left_rocker = 0;
//	       break;

//			//右左中
//			case 14:
//				right_rocker = 1;
//				//Mode_control |= 1;
//			break;
//			
//			case 15:
//				right_rocker = 2;
//			//	Mode_control |= 1;
//			break;
//			
//			case 16://右
//			    right_rocker = 3;
//				//Mode_control |= 1;
//			break;
//			
//			case 17://左
//			    right_rocker = 4;
//				//Mode_control |= 1;
//			break;
//			
//			case 22:
//				right_rocker = 0;
//				//Mode_control |= 1;
//			break;
//			
//		   case 12:
//			if(Press)
//				{
//					if(!(State_control & 2))
//						State_control += 2;
//				}
//				else
//				{
//						//清0状态位，进入停止
//						if((State_control & 2))
//						State_control -= 2;
//				}
//	    	break;
//		   case 13:
//			if(Press)
//				{
//					if(!(State_control & 1))
//						State_control += 1;
//				}
//				else
//				{
//						//清0状态位，进入停止
//						if((State_control & 1))
//						State_control -= 1;
//				}
//	    	break;
//		//灯光控制按键
//		// //亮灯
//		// case 8:
//		// 	if(Press)
//		// 	{
//		// 		if(!(Light_control & 1))
//		// 			Light_control += 1;
//		// 	}
//		// break;
//		// //暗灯
//		// case 9:
//		// 	if(Press)
//		// 	{
//		// 		if(!(Light_control & 2))
//		// 			Light_control += 2;
//		// 	}
//		// break;

//		//start键
//			//定深总开关使用DepthFlag第一位
//			case 8:
//			if(Press)
//				{//计数器
//					if(((DepthFlag >> 1 ) % 2) == 0)//如果第一位是0
//						DepthFlag |= 2;//设置为1
//					else
//						DepthFlag -= 2;//清除
//					DCInfo.setDepth = MS5837.fDepth;
//					rt_mq_send(DepthControlmq,&DCInfo,sizeof(DepthControlInfo));
//				}
//			else
//				{
//					// Expect_angle_Init();
//					// concon_YAW = JY901S.stcAngle.ConYaw;
//					if(((DepthFlag >> 1 ) % 2) == 1)
//						DepthFlag |= 1;
//					else
//						DepthFlag &= ~1;
//				}
//			break;
//		//back键
//		//慢速状态切换
//		case 9:
//			if(Press)
//				{
//					//奇偶计次
//					if(((SpeedMode >> 1 ) % 2) == 0)
//						SpeedMode |= 2;
//					else
//						SpeedMode -= 2;
//				}
//			else
//				{
//					if(((SpeedMode >> 1 ) % 2) == 1)
//						SpeedMode |= 1;
//					else
//						SpeedMode &= ~1;
//				}
//			break;
//    }
//}
