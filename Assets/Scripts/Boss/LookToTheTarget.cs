using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToTheTarget : StateMachineBehaviour
{
    [SerializeField]
    private AIData aiData;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 获取 AI 数据
        aiData = animator.gameObject.transform.parent?.GetComponent<AIData>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 检查 AIData 是否为空或 enemyTargetPosition 是否为空
        if (aiData == null || aiData.enemyTargetPosition == null || aiData.transform.position == null)
        {
            return; // 如果为空，跳过操作
        }

        // 获取角色和目标位置
        Vector3 currentPosition = aiData.transform.position;
        Vector3 targetPosition = aiData.enemyTargetPosition.position;

        // 计算目标的方向
        float directionToTarget = targetPosition.x - currentPosition.x;

        // 检查角色是否面向目标位置（默认朝向是右，x 轴正方向）
        if ((directionToTarget > 0 && aiData.transform.localScale.x < 0) ||
            (directionToTarget < 0 && aiData.transform.localScale.x > 0))
        {
            // 如果角色未面向目标位置，则翻转角色的朝向
            Vector3 scale = aiData.transform.localScale;
            scale.x *= -1;
            aiData.transform.localScale = scale;
        }
    }
}
