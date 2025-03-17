/****************************************************************************

* MATE�Ŷ�

* �ļ���: algo_median_F.c

* ���ݼ�������ֵ�˲���غ���

* �ļ���ʷ��

* �汾��		����	    ����		    ˵��
* V1.1.0 	2025-03-16	 ������		 �������ļ�

****************************************************************************/
#include "drv_hal_conf.h"
#include "config.h"
#include "algo_median_F.h"
float getMedian(float *buffer, int size)
{
    float temp[size]; 
    memcpy(temp, buffer, size * sizeof(float)); 
    qsort(temp, size, sizeof(float), compareFloat); 
    return temp[size / 2]; 
}


int compareFloat(const void *a, const void *b)
{
    float fa = *(const float*)a;
    float fb = *(const float*)b;
    return (fa > fb) - (fa < fb);
}

float Median_Flitering_Output(float Curr_Depth)
{
    float depthBuffer[FILTER_WINDOW_SIZE] = {0}; // ??????
    int bufferIndex = 0;  // ??????
    int dataCount = 0;  // ????????
     // ?????
     depthBuffer[bufferIndex] = Curr_Depth;
     bufferIndex = (bufferIndex + 1) % FILTER_WINDOW_SIZE;  // ??????
     if (dataCount < FILTER_WINDOW_SIZE)
     dataCount++;  // ???????

     // ????
     float depthFiltered = getMedian(depthBuffer, dataCount);
     return depthFiltered;
}
