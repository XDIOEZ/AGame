using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : StateMachineBehaviour
{
    [Header("搜索设置")]
    [SerializeField]
    private float searchRadius = 20f; // 搜索敌人的范围
    [SerializeField]
    private LayerMask enemyLayer; // 指定敌人所在的图层


    [Header("AI 数据")]
    [SerializeField]
    private AIData aiData; // AI 数据

    [Header("搜索间隔设置")]
    [SerializeField]
    private float searchInterval = 1f; // 搜索间隔时间
    private float searchTimer = 0f; // 用于计时的变量

    [Header("状态切换设置")]
    [SerializeField]
    private string FoundAction; // 切换到下一个状态的动作

    // 进入状态时获取父对象的 AI 数据
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
    }

    // 在每帧更新中搜索敌人
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        searchTimer += Time.deltaTime;

        if (searchTimer >= searchInterval)
        {
            bool enemyFound = SearchForEnemies(animator.transform.position);
            if (enemyFound && FoundAction != "")
            {
                animator.SetTrigger(FoundAction); // 切换到自定义状态
            }
            searchTimer = 0f; // 重置计时器
        }
    }

    // 封装的敌人搜索函数，返回是否找到敌人
    private bool SearchForEnemies(Vector3 position)
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(position, searchRadius, enemyLayer);
        //Debug.Log("敌人数量: " + enemiesInRange.Length);

        if (enemiesInRange.Length > 0)
        {
            // 找到最近的敌人
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

            // 将最近的敌人设置为目标
            if (closestEnemy != null)
            {
                aiData.enemyTargetPosition = closestEnemy; // 赋值给 enemyTargetPosition
                //Debug.Log("设置移动目标为: " + closestEnemy.name);
            }

            return true; // 找到敌人
        }
        else //在此添加一个条件,避免清除随机标识位置
        {
           
            return false; // 没有找到敌人
        }
    }

    // 绘制搜索范围以便在 Scene 视图中可视化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (aiData != null)
        {
            Gizmos.DrawWireSphere(aiData.transform.position, searchRadius);
        }
    }
}
