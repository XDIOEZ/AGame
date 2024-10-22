using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour
{
    [Header("�ȴ�ʱ��&&��ʱ��")]
    [SerializeField]
    private float waitTimer = 3f; // �ȴ�ʱ��
    [SerializeField]
    private float timer; // ��ʱ��
    [Header("״̬�л�����")]
    [SerializeField]
    private  string EndAction; // �ƶ�����

    // ����״̬ʱ���ü�ʱ��
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f; // ���ü�ʱ��
    }

    // ��ÿ֡�����м��ȴ�ʱ��
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime; // ���¼�ʱ��

        // ����ȴ�ʱ��ﵽ�趨ֵ���л��� Move ����
        if (timer >= waitTimer && EndAction!= "")
        {
            animator.SetTrigger(EndAction);
        }
    }

    // �뿪״̬ʱ�����ʱ��
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f; // ���ü�ʱ��
    }
}
