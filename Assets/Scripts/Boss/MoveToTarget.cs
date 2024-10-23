using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // ���� DOTween �������ռ�

public class MoveToTarget : StateMachineBehaviour
{
    [Header("�ƶ�����")]
    [SerializeField]
    private string EndAction; // �ƶ���Ŀ��λ�ú��л��Ķ���

    [SerializeField]
    private float arrivalThreshold = 0.1f; // ����Ŀ��λ�õ���ֵ
    [SerializeField]
    private Vector2 speedRange = new Vector2(0.5f, 5f); // �ƶ��ٶȷ�Χ����С������ٶȣ�
    [SerializeField]
    private float moveSpeed; // �洢������ƶ��ٶ�

    private AIData aiData;
    private Tweener moveTweener; // �洢�ƶ��� Tweener

    // �ڽ���״̬ʱ��ȡ AI ����
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (aiData == null)
        {
            GameObject parentObject = animator.gameObject.transform.parent?.gameObject;
            if (parentObject != null)
            {
                aiData = parentObject.GetComponent<AIData>(); // ��ȡ AI ����
            }
        }
        moveSpeed = Random.Range(speedRange.x, speedRange.y);
        // ȷ���ƶ��ٶ���ָ����Χ��
        moveSpeed = Mathf.Clamp(moveSpeed, speedRange.x, aiData.entity.Data.MoveSpeed);

    }

    // ��״̬����ʱ����Ƿ񵽴�Ŀ��λ��
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (aiData.enemyTargetPosition != null)
        {
            Debug.Log("Boss��ʼ�ƶ�");
            Vector3 targetPosition = aiData.enemyTargetPosition.position;
            float distance = Vector3.Distance(aiData.transform.position, targetPosition);
            float duration = distance / moveSpeed; // ���ݾ�����㶯������ʱ��

            // ʹ�� DOTween ʵ��ƽ���ƶ�
            moveTweener = aiData.transform.DOMove(targetPosition, duration)
                .SetEase(Ease.Linear) // ���Բ�ֵ�����ֺ㶨�ٶ�
                .OnComplete(() =>
                {
                    // �ƶ���ɺ��л�����һ������
                    if (!string.IsNullOrEmpty(EndAction))
                    {
                        animator.SetTrigger(EndAction);
                    }
                });
        }
    }


    // ���˳�״̬ʱֹͣ�ƶ�
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ����ƶ� Tweener �������У���ֹͣ��
        if (moveTweener != null && moveTweener.IsActive())
        {
            moveTweener.Kill();
        }
    }
}
