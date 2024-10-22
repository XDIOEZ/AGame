using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : StateMachineBehaviour
{
    [Header("��������")]
    [SerializeField]
    private float searchRadius = 20f; // �������˵ķ�Χ
    [SerializeField]
    private LayerMask enemyLayer; // ָ���������ڵ�ͼ��


    [Header("AI ����")]
    [SerializeField]
    private AIData aiData; // AI ����

    [Header("�����������")]
    [SerializeField]
    private float searchInterval = 1f; // �������ʱ��
    private float searchTimer = 0f; // ���ڼ�ʱ�ı���

    [Header("״̬�л�����")]
    [SerializeField]
    private string FoundAction; // �л�����һ��״̬�Ķ���

    // ����״̬ʱ��ȡ������� AI ����
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
    }

    // ��ÿ֡��������������
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        searchTimer += Time.deltaTime;

        if (searchTimer >= searchInterval)
        {
            bool enemyFound = SearchForEnemies(animator.transform.position);
            if (enemyFound && FoundAction != "")
            {
                animator.SetTrigger(FoundAction); // �л����Զ���״̬
            }
            searchTimer = 0f; // ���ü�ʱ��
        }
    }

    // ��װ�ĵ������������������Ƿ��ҵ�����
    private bool SearchForEnemies(Vector3 position)
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(position, searchRadius, enemyLayer);
        //Debug.Log("��������: " + enemiesInRange.Length);

        if (enemiesInRange.Length > 0)
        {
            // �ҵ�����ĵ���
            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (var enemy in enemiesInRange)
            {
                float distance = Vector3.Distance(position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform;
                }
            }

            // ������ĵ�������ΪĿ��
            if (closestEnemy != null)
            {
                aiData.enemyTargetPosition = closestEnemy; // ��ֵ�� enemyTargetPosition
                //Debug.Log("�����ƶ�Ŀ��Ϊ: " + closestEnemy.name);
            }

            return true; // �ҵ�����
        }
        else //�ڴ����һ������,������������ʶλ��
        {
           
            return false; // û���ҵ�����
        }
    }

    // ����������Χ�Ա��� Scene ��ͼ�п��ӻ�
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (aiData != null)
        {
            Gizmos.DrawWireSphere(aiData.transform.position, searchRadius);
        }
    }
}
