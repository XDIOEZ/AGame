using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    // ��״̬����ʱ��ʼ�����������ڲ��Ź�����Ч�ȣ�
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ������������ӹ���ǰ��׼���߼������粥����Ч
        //Debug.Log("Boss��ʼ����");

        animator.SetTrigger("Move"); // ȷ�� "Move" ������ Animator �ж�����ƶ�״̬�Ĵ���������
       // Debug.Log("������ɣ������ƶ�״̬");
    }

    // ��״̬����ʱ��ÿ֡��
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �˴�������ӹ����߼�����������ײ������˺�
    }

    // ��״̬�˳�ʱ�л����ƶ�״̬
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �л����ƶ�״̬
       
    }
}
