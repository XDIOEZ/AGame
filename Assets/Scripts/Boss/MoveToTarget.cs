using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // 导入 DOTween 的命名空间

public class MoveToTarget : StateMachineBehaviour
{
    [Header("移动设置")]
    [SerializeField]
    private string EndAction; // 移动到目标位置后切换的动作

    [SerializeField]
    private float arrivalThreshold = 0.1f; // 到达目标位置的阈值
    [SerializeField]
    private Vector2 speedRange = new Vector2(0.5f, 5f); // 移动速度范围（最小和最大速度）
    [SerializeField]
    private float moveSpeed; // 存储随机的移动速度

    private AIData aiData;
    private Tweener moveTweener; // 存储移动的 Tweener

    // 在进入状态时获取 AI 数据
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (aiData == null)
        {
            GameObject parentObject = animator.gameObject.transform.parent?.gameObject;
            if (parentObject != null)
            {
                aiData = parentObject.GetComponent<AIData>(); // 获取 AI 数据
            }
        }
        moveSpeed = Random.Range(speedRange.x, speedRange.y);
        // 确保移动速度在指定范围内
        moveSpeed = Mathf.Clamp(moveSpeed, speedRange.x, aiData.entity.Data.MoveSpeed);

    }

    // 在状态更新时检查是否到达目标位置
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (aiData.enemyTargetPosition != null)
        {
            Debug.Log("Boss开始移动");
            Vector3 targetPosition = aiData.enemyTargetPosition.position;
            float distance = Vector3.Distance(aiData.transform.position, targetPosition);
            float duration = distance / moveSpeed; // 根据距离计算动画持续时间

            // 使用 DOTween 实现平滑移动
            moveTweener = aiData.transform.DOMove(targetPosition, duration)
                .SetEase(Ease.Linear) // 线性插值，保持恒定速度
                .OnComplete(() =>
                {
                    // 移动完成后切换到下一个动作
                    if (!string.IsNullOrEmpty(EndAction))
                    {
                        animator.SetTrigger(EndAction);
                    }
                });
        }
    }


    // 在退出状态时停止移动
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 如果移动 Tweener 仍在运行，则停止它
        if (moveTweener != null && moveTweener.IsActive())
        {
            moveTweener.Kill();
        }
    }
}
