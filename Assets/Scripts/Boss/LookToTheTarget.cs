using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToTheTarget : StateMachineBehaviour
{
    [SerializeField]
    private AIData aiData;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��ȡ AI ����
        aiData = animator.gameObject.transform.parent?.GetComponent<AIData>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��� AIData �Ƿ�Ϊ�ջ� enemyTargetPosition �Ƿ�Ϊ��
        if (aiData == null || aiData.enemyTargetPosition == null || aiData.transform.position == null)
        {
            return; // ���Ϊ�գ���������
        }

        // ��ȡ��ɫ��Ŀ��λ��
        Vector3 currentPosition = aiData.transform.position;
        Vector3 targetPosition = aiData.enemyTargetPosition.position;

        // ����Ŀ��ķ���
        float directionToTarget = targetPosition.x - currentPosition.x;

        // ����ɫ�Ƿ�����Ŀ��λ�ã�Ĭ�ϳ������ң�x ��������
        if ((directionToTarget > 0 && aiData.transform.localScale.x < 0) ||
            (directionToTarget < 0 && aiData.transform.localScale.x > 0))
        {
            // �����ɫδ����Ŀ��λ�ã���ת��ɫ�ĳ���
            Vector3 scale = aiData.transform.localScale;
            scale.x *= -1;
            aiData.transform.localScale = scale;
        }
    }
}
