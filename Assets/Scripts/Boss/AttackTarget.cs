using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillAnimationState
{
    public string skillName;
    public float coolDownTime;
    public float triggerProbability;

    public float attackCoolDownTime = 0f; // �����Ĺ�����ȴ��ʱ��

    [SerializeField]
    private Vector2 releaseDistanceRange = new Vector2(1f, 5f); // �����ͷž��뷶Χ����С�����ֵ��

    public Vector2 ReleaseDistanceRange => releaseDistanceRange; // ���⹫�����ͷž��뷶Χ
}

public class AttackTarget : StateMachineBehaviour
{
    

    private AIData aiData;

    [Header("���ܼ����")]
    [SerializeField]
    private float skillCheckInterval = 0.5f; // ���ü��ܼ���ʱ��������λ���룩
    [SerializeField]
    private float skillCheckTimer = 0f; // ���ڼ�ʱ�ı���
    [Header("������������")]
    [SerializeField]
    private SkillAnimationState[] SkillList; // ����������������
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��ȡ AI ����
        aiData = animator.gameObject.transform.parent?.GetComponent<AIData>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (aiData == null || aiData.enemyTargetPosition == null)
        {
            Debug.LogWarning("AIData �� enemyTargetPosition �����ã��޷����й�����顣");
            return;
        }

        // ���㵱ǰλ�ú�Ŀ��λ��֮��ľ���
        float distanceToTarget;

        skillCheckTimer += Time.deltaTime;
// ���������б�������ȴ��ʱ��
        foreach (var skill in SkillList)
        {
            skill.attackCoolDownTime -= Time.deltaTime; // ���ټ�����ȴʱ��
        }
        // ���������б�������ȴ��ʱ������鹥����Χ
        if (skillCheckTimer >= skillCheckInterval)
        {
            // ���㵱ǰλ�ú�Ŀ��λ��֮��ľ���
             distanceToTarget = Vector2.Distance(aiData.currentPosition.position, aiData.enemyTargetPosition.position);
            //Debug.Log("��ʼ����");
            // ���������б�������ȴ��ʱ������鹥����Χ
            for (int i = 0; i < SkillList.Length; i++)
            {
                var skill = SkillList[i];

                // ���µ�ǰ���ܵ���ȴ��ʱ��
                

                // ��鹥����Χ���л�״̬
                if (CheckAttackRangeAndSwitchState(animator, distanceToTarget, skill))
                {
                    // ��������ͷųɹ�������ѭ��
                    aiData.MaxSkillInput++;
                    
                    break;
                }
            }
            skillCheckTimer = 0f;
        }
    }
    // ��鹥����Χ���л�״̬
    private bool CheckAttackRangeAndSwitchState(Animator animator, float distanceToTarget, SkillAnimationState skill)
    {
        // ����Ƿ��ڹ�����Χ����ȴʱ��
        if (distanceToTarget <= skill.ReleaseDistanceRange.y && skill.attackCoolDownTime <= 0)
        {
            // ��鼼���ͷž����Ƿ����趨��Χ��
            if (distanceToTarget >= skill.ReleaseDistanceRange.x && distanceToTarget <= skill.ReleaseDistanceRange.y)
            {
                // ������ֵ�Ƿ�С�ڵ��ڴ���Ĵ�������
                if (Random.value <= skill.triggerProbability)
                {
                    animator.SetTrigger(skill.skillName);
                    skill.attackCoolDownTime = skill.coolDownTime; // ���õ�ǰ���ܵ���ȴʱ��
                    
                    //Debug.Log($"�ͷ�{skill.skillName}���ܡ�");
                    return true; // �ɹ��ͷż��ܣ����� true
                }
            }
        }

        return false; // δ�ɹ��ͷż��ܣ����� false
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
