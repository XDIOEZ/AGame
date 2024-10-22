using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : StateMachineBehaviour
{
    [Header("随机移动设置")]
    [SerializeField]
    private Vector2 moveDurationRange = new Vector2(1f, 3f); // 随机移动时间范围
    [SerializeField]
    private Vector2 moveSpeedRange = new Vector2(0.5f, 1.0f); // 移动速度范围
    [SerializeField]
    private int turnCounter = 3; // 允许的转向次数
    [SerializeField]
    private float minTurnDuration = 0.5f; // 转向持续时间的最小值
    [SerializeField]
    private float maxTurnDuration = 1.5f; // 转向持续时间的最大值

    private float moveTimer = 0f; // 随机移动计时器
    private Vector2 moveDirection; // 随机移动方向
    private AIData aiData;
    private int currentTurnCounter; // 当前转向计数器
    private float turnDuration; // 每次转向的持续时间

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 获取AI数据
        aiData = animator.gameObject.transform.parent?.GetComponent<AIData>();

        // 检查AI是否有目标，如果有目标则跳过Update的执行
        if (aiData != null && aiData.enemyTargetPosition != null)
        {
            //Debug.Log("AI有目标，跳过随机移动。");
            moveTimer = 0f;
            return;
        }

        // 初始化移动计时器
        moveTimer = Random.Range(moveDurationRange.x, moveDurationRange.y); // 随机初始化计时器

        // 随机移动方向
        ChangeMoveDirection();

        // 初始化转向计数器
        currentTurnCounter = turnCounter;
    }

    private void ChangeMoveDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
        turnDuration = Random.Range(minTurnDuration, maxTurnDuration); // 设置转向持续时间
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 检查是否有目标，如果有目标则跳过随机移动逻辑
        if (aiData == null || aiData.enemyTargetPosition != null)
        {
            return;
        }

        // 随机移动
        float randomSpeed = aiData.entity.Data.MoveSpeed * Random.Range(moveSpeedRange.x, moveSpeedRange.y);
        aiData.currentPosition.Translate(moveDirection * randomSpeed * Time.deltaTime);

        // 更新计时器
        moveTimer -= Time.deltaTime;
        turnDuration -= Time.deltaTime;

        // 如果转向持续时间到达0并且转向计数器大于0，则改变移动方向
        if (turnDuration <= 0f && currentTurnCounter > 0)
        {
            ChangeMoveDirection();
            currentTurnCounter--; // 递减转向计数器
        }

        // 如果 moveTimer <= 0，并且转向计数器归零，则进入Idle状态
        if (moveTimer <= 0f && currentTurnCounter <= 0)
        {
            animator.SetTrigger("Idle");
            Debug.Log("随机移动时间结束，进入Idle状态。!!!!!!!!!!!!!!!");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 清除计时器
        moveTimer = 0f;
       // Debug.Log("退出Move状态，重置随机移动计时器。");
    }
}
