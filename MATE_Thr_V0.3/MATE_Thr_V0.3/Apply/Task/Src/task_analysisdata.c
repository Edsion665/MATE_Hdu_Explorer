#include "task_conf.h"
#include "usercode.h"
#include "config.h"

static char AnalysisData[5][15];
static int AnalysisNum = 0;

/* åˆ†æžä»Žä¸Šä½æœºå¤„æŽ¥æ”¶åˆ°çš„æ•°æ® */
void Task_AnalysisData(uint8_t *DataBuf)
{
    // å°†å˜é‡æ¸…0
    rt_memset(AnalysisData, 0, sizeof(AnalysisData));
    HandleModeInfo HMInfo = {" ", " ", 0, 0, 0, " "};
    AutoModeInfo AMInfo = {" ", 0};
    DepthControlInfo DCInfo = {0};
    AnalysisNum = 0;

    // æ ¹æ®æ ¼å¼åˆ‡åˆ†å­—ç¬¦ä¸²å­˜æ”¾åˆ°äºŒç»´æ•°ç»„ä¸­
    AnalysisNum = sscanf((char *)DataBuf, "%s %s %s %s %s",
                         AnalysisData[0],
                         AnalysisData[1],
                         AnalysisData[2],
                         AnalysisData[3],
                         AnalysisData[4]);
     printf("%d ,%s,%s,%s,%s,%s\r\n",AnalysisNum,AnalysisData[0],AnalysisData[1],AnalysisData[2],AnalysisData[3],AnalysisData[4]);

    if (!AnalysisNum)
        return;
    /* æ ¹æ®å¸§å¤´é€‰æ‹©ä»»åŠ¡ */

    // æ‰‹æŸ„æ‘‡æ†ä¸Žæ‰³æœºå€¼  "@ æ ‡è¯† æ•°å€¼ ï¼ˆæ˜¯å¦æŒ‰ä¸‹ï¼‰$"  
    else if (!rt_strcmp(AnalysisData[0], "@"))
    {
			// æ›´æ–° HMInfo çš„å†…å®¹
        if (AnalysisNum >= 3) {
            strcpy(HMInfo.Change, AnalysisData[1]); 
            HMInfo.fNum[0] = strtof(AnalysisData[2],NULL);
					  HMInfo.fNum[1] = strtof(AnalysisData[3],NULL);
        }
				
				printf("%s %f\r\n",HMInfo.Change, HMInfo.fNum[0]);

        // å°†æ•°æ®å‘é€ç»™æ‰‹æŸ„æŽ§åˆ¶çº¿ç¨‹
        rt_mq_send(HandleModemq, &HMInfo, sizeof(HandleModeInfo));
    }

    // æ‰‹æŸ„æŒ‰é”®çŠ¶æ€   1ï¼š"@æŒ‰é”®æ ‡è¯†$"   2æ¨¡å¼æŒ‰é”®ï¼ˆ8,9ï¼‰ï¼šâ€œ@æŒ‰é”®æ ‡è¯† 0/1â€
    else if (AnalysisData[0][0] == '@')
    {
				if(!rt_strcmp(AnalysisData[0], "@MRU0$ ")){
					HMInfo.fNum[1] = 0;
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MRB0$")){
					HMInfo.fNum[1] = 1;
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MRD0$")){
					HMInfo.fNum[1] = 2;
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MRF0$")){
					HMInfo.fNum[1] = 3;
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MMB0$")){
					HMInfo.fNum[1] = 4;
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MRR0$")){
					HMInfo.fNum[1] = 5;
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MRL0$")){
					HMInfo.fNum[1] = 6;
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MMF0$")){
					HMInfo.fNum[1] = 7;
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MDM0")){
					HMInfo.fNum[1] = 8;
					HMInfo.fNum[2] = strtof(AnalysisData[1],NULL);
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MDP0")){
					HMInfo.fNum[1] = 9;
					HMInfo.fNum[2] = strtof(AnalysisData[1],NULL);
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MMD0$")){
					HMInfo.fNum[1] = 10;
				}
					
				else if(!rt_strcmp(AnalysisData[0], "@MMU0$")){
					HMInfo.fNum[1] = 11;
				}
					
				
				strcpy(HMInfo.Change, AnalysisData[0]);
        // å°†æ•°æ®å‘é€ç»™æ‰‹æŸ„æŽ§åˆ¶çº¿ç¨‹
			
				printf("%s %f\r\n",HMInfo.Change, HMInfo.fNum[1]);
			
        rt_mq_send(HandleModemq, &HMInfo, sizeof(HandleModeInfo));
    }

    // è‡ªåŠ¨å·¡çº¿æ¨¡å¼è§’åº¦å€¼
    else if (!rt_strcmp(AnalysisData[0], "LP"))
    {
        // ä¿å­˜æ•°æ®ï¼ŒLP è§’åº¦ ä½ç½®åç§»
        AMInfo.BlackAngle = strtof(AnalysisData[1], NULL);
        AMInfo.CenterShift = strtof(AnalysisData[2], NULL);
        rt_mq_urgent(AutoModemq, &AMInfo, sizeof(AutoModeInfo));
    }

    // å®šæ·±æ•°å€¼
    else if (!rt_strcmp(AnalysisData[0], "D"))
    {
        DCInfo.setDepth = strtof(AnalysisData[1], NULL);
        rt_mq_send(DepthControlmq, &DCInfo, sizeof(DepthControlInfo));
    }

    // æ¨¡å¼åˆ‡æ¢å‘½ä»¤
    else if (!rt_strcmp(AnalysisData[0], "MODE"))
    {
        // printf("%s\r\n",AnalysisData[1]);
        if (!rt_strcmp(AnalysisData[1], "AUTO"))
        {
            // æŒ‚èµ·æ‰‹æŸ„æŽ§åˆ¶æ¨¡å¼ï¼Œå¯åŠ¨è‡ªåŠ¨å·¡çº¿æ¨¡å¼
            rt_memcpy(HMInfo.ModeChange, "AUTO START", sizeof("AUTO START"));
            rt_mq_send(HandleModemq, &HMInfo, sizeof(HandleModeInfo));
        }
        else if (!rt_strcmp(AnalysisData[1], "HANDLE"))
        {
            // æŒ‚èµ·è‡ªåŠ¨å·¡çº¿æ¨¡å¼ï¼Œå¯åŠ¨æ‰‹æŸ„æ¨¡å¼
            rt_memcpy(AMInfo.ModeChange, "HANDLE START", sizeof("HANDLE START"));
            rt_mq_send(AutoModemq, &AMInfo, sizeof(AutoModeInfo));
        }
    }

    // PIDå€¼
    else if (!rt_strcmp(AnalysisData[0], "PID"))
    {
        // æ·±åº¦çŽ¯PID
        if (!rt_strcmp(AnalysisData[1], "DepthPID"))
        {
            HMInfo.fNum[0] = strtof(AnalysisData[2], NULL);
            HMInfo.fNum[1] = strtof(AnalysisData[3], NULL);
            HMInfo.fNum[2] = strtof(AnalysisData[4], NULL);

            // æ›´æ–°æ·±åº¦çŽ¯PID
            Algo_PID_Update(&DepthPID, HMInfo.fNum);
            printf("DepthPID %.2f %.2f %.2f\r\n", DepthPID.fKp, DepthPID.fKi, DepthPID.fKd);
        }
        // è‰å‘çŽ¯PID
        else if (!rt_strcmp(AnalysisData[1], "YawPID"))
        {
            HMInfo.fNum[0] = strtof(AnalysisData[2], NULL);
            HMInfo.fNum[1] = strtof(AnalysisData[3], NULL);
            HMInfo.fNum[2] = strtof(AnalysisData[4], NULL);

            // æ›´æ–°è‰å‘çŽ¯PID
            Algo_PID_Update(&YawPID, HMInfo.fNum);
            printf("YawPID %.2f %.2f %.2f\r\n", YawPID.fKp, YawPID.fKi, YawPID.fKd);
        }
        // è§’åº¦çŽ¯PID
        else if (!rt_strcmp(AnalysisData[1], "AngleLoopPID"))
        {
            HMInfo.fNum[0] = strtof(AnalysisData[2], NULL);
            HMInfo.fNum[1] = strtof(AnalysisData[3], NULL);
            HMInfo.fNum[2] = strtof(AnalysisData[4], NULL);

            // æ›´æ–°è‰è§’åº¦çŽ¯PID
            Algo_PID_Update(&AngleLoopPID, HMInfo.fNum);
            printf("AngleLoopPID %.2f %.2f %.2f\r\n", AngleLoopPID.fKp, AngleLoopPID.fKi, AngleLoopPID.fKd);
        }
        // ä½ç½®çŽ¯PID
        else if (!rt_strcmp(AnalysisData[1], "PositionLoopPID"))
        {
            HMInfo.fNum[0] = strtof(AnalysisData[2], NULL);
            HMInfo.fNum[1] = strtof(AnalysisData[3], NULL);
            HMInfo.fNum[2] = strtof(AnalysisData[4], NULL);

            // æ›´æ–°ä½ç½®çŽ¯PID
            Algo_PID_Update(&PositionLoopPID, HMInfo.fNum);
            printf("PositionLoopPID %.2f %.2f %.2f\r\n", PositionLoopPID.fKp, PositionLoopPID.fKi, PositionLoopPID.fKd);
        }
        // å·¡çº¿çŽ¯PID
        //  else if(!rt_strcmp(AnalysisData[1],"LinePatrolPID"))
        //  {
        //      HMInfo.fNum[0] = strtof(AnalysisData[2],NULL);
        //      HMInfo.fNum[1] = strtof(AnalysisData[3],NULL);
        //      HMInfo.fNum[2] = strtof(AnalysisData[4],NULL);

        //     //æ›´æ–°å·¡çº¿çŽ¯PID
        //     Algo_PID_Update(&LinePatrolPID,HMInfo.fNum);
        //     printf("LinePatrolPID %.2f %.2f %.2f\r\n",LinePatrolPID.fKp,LinePatrolPID.fKi,LinePatrolPID.fKd);
        // }
    }
}