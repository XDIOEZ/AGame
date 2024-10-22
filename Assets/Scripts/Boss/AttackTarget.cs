using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillAnimationState
{
    public string skillName;
    public float coolDownTime;
    public float triggerProbability;

    public float attackCoolDownTime = 0f; // 独立的攻击冷却计时器

    [SerializeField]
    private Vector2 releaseDistanceRange = new Vector2(1f, 5f); // 技能释放距离范围（最小和最大值）

    public Vector2 ReleaseDistanceRange => releaseDistanceRange; // 对外公开的释放距离范围
}

public class AttackTarget : StateMachineBehaviour
{
    

    private AIData aiData;

    [Header("技能检查间隔")]
    [SerializeField]
    private float skillCheckInterval = 0.5f; // 设置技能检查的时间间隔（单位：秒）
    [SerializeField]
    private float skillCheckTimer = 0f; // 用于计时的变量
    [Header("攻击技能设置")]
    [SerializeField]
    private SkillAnimationState[] SkillList; // 攻击动画触发器名
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 获取 AI 数据
        aiData = animator.gameObject.transform.parent?.GetComponent<AIData>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (aiData == null || aiData.enemyTargetPosition == null)
        {
            Debug.LogWarning("AIData 或 enemyTargetPosition 不可用，无法进行攻击检查。");
            return;
        }

        // 计算当前位置和目标位置之间的距离
        float distanceToTarget;

        skillCheckTimer += Time.deltaTime;
// 遍历技能列表，更新冷却计时器
        foreach (var skill in SkillList)
        {
            skill.attackCoolDownTime -= Time.deltaTime; // 减少技能冷却时间
        }
        // 遍历技能列表，更新冷却计时器并检查攻击范围
        if (skillCheckTimer >= skillCheckInterval)
        {
            // 计算当前位置和目标位置之间的距离
             distanceToTarget = Vector2.Distance(aiData.currentPosition.position, aiData.enemyTargetPosition.position);
            //Debug.Log("开始遍历");
            // 遍历技能列表，更新冷却计时器并检查攻击范围
            for (int i = 0; i < SkillList.Length; i++)
            {
                var skill = SkillList[i];

                // 更新当前技能的冷却计时器
                

                // 检查攻击范围并切换状态
                if (CheckAttackRangeAndSwitchState(animator, distanceToTarget, skill))
                {
                    // 如果技能释放成功，跳出循环
                    aiData.MaxSkillInput++;
                    
                    break;
                }
            }
            skillCheckTimer = 0f;
        }
    }
    // 检查攻击范围并切换状态
    private bool CheckAttackRangeAndSwitchState(Animator animator, float distanceToTarget, SkillAnimationState skill)
    {
        // 检查是否在攻击范围和冷却时间
        if (distanceToTarget <= skill.ReleaseDistanceRange.y && skill.attackCoolDownTime <= 0)
        {
            // 检查技能释放距离是否在设定范围内
            if (distanceToTarget >= skill.ReleaseDistanceRange.x && distanceToTarget <= skill.ReleaseDistanceRange.y)
            {
                // 检查随机值是否小于等于传入的触发概率
                if (Random.value <= skill.triggerProbability)
                {
                    animator.SetTrigger(skill.skillName);
                    skill.attackCoolDownTime = skill.coolDownTime; // 重置当前技能的冷却时间
                    
                    //Debug.Log($"释放{skill.skillName}技能。");
                    return true; // 成功释放技能，返回 true
                }
            }
        }

        return false; // 未成功释放技能，返回 false
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
