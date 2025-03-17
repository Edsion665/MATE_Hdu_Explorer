/****************************************************************************

* MATE�Ŷ�

* �ļ���: algo_kalman.c

* ���ݼ�����kalman�˲���غ���

* �ļ���ʷ��

* �汾��		����	    ����		    ˵��
* V1.1.0 	2025-03-16	 ������		 �������ļ�

****************************************************************************/
#include "drv_hal_conf.h"
#include "config.h"
#include "stdint.h"  // ��ӱ�׼����ͷ�ļ���ȷ�� int16_t �Ѷ���
#include "algo_kalman.h"
// �����˲���ʵ��
kalman_controller R_encoderkal;
kalman_controller L_encoderkal;
lpf_controller lpf_controller_l, lpf_controller_r;


// һ�׹����˲�����ʼ��
void lpf_init(lpf_controller* lpf) {
    lpf->last_out = 0;
}

//һ�׵�ͨ�˲�������
int16_t lpf_update(lpf_controller* lpf, int16_t input) {
    // �����˲�����������м�Ȩƽ��
    return lpf->last_out = (int16_t)(lpf->alpha * input + (1 - lpf->alpha) * lpf->last_out);
}

// �������˲�������
float kalman_update(kalman_controller *klm, float input) {
    // Ԥ��Э�����
    klm->Now_P = klm->LastP + klm->Q;
    // ���㿨��������
    klm->Kg = klm->Now_P / (klm->Now_P + klm->R);
    // �������Ź���
    klm->out = klm->out + klm->Kg * (input - klm->out);
    // ����Э����
    klm->LastP = (1 - klm->Kg) * klm->Now_P;
    return klm->out;
}

// �������˲�����ʼ��
void kalman_init(kalman_controller *klm, float klm_Q, float klm_R) {
    klm->LastP = 1.03;  // ��ʼЭ����
    klm->Now_P = 0;     // ��ǰЭ����
    klm->out = 0;       // ��ʼ���
    klm->Kg = 0;        // ��ʼ����������
    klm->Q = klm_Q;     // ��������Э����
    klm->R = klm_R;     // ��������Э����
}
