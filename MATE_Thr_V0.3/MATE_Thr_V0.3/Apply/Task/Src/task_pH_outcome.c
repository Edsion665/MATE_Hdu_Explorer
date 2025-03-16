#include "task_conf.h"
#include "usercode.h"
#include "config.h"
void Get_pH_Value(tagADC_T *_tADC)
{   uint16_t pH_i;
    float Average_pH;
    for(pH_i = 0 ; pH_i < 10; pH_i++)
    {
        float voltage = Drv_ADC_PollGetValue(_tADC);  
        float pH = pH_K * voltage + pH_b;  // 计算 pH
        Average_pH += pH;
        Drv_Delay_Ms(200);   //延时取样
    }

    Average_pH /= 10;

    printf("pH:  %.2f",Average_pH);
    
    
}

