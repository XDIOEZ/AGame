using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : EnemyHurt
{
    [Header("主要参数")]
    [Tooltip("玩家标签")]
    public string playerTag = "Player"; // 玩家标签

    [Header("声音配置")]
    [Tooltip("移动声音")]
    public string moveAudio; // 移动声音

    [Tooltip("静默声音")]
    public string idleAudio; // 静默声音

    [Tooltip("死亡声音")]
    public string deathAudio = "OnDead"; // 死亡声音

    [Header("可调节参数")]
    [Tooltip("移动速度")]
    public float moveSpeed = 2f; // 移动速度

    [Tooltip("随机游荡范围")]
    public float wanderRange = 5f; // 随机游荡范围

    [Tooltip("追踪范围")]
    public float chaseRange = 5f; // 追踪范围

    [Tooltip("追踪速度")]
    public float chaseSpeed = 4f; // 追踪速度

    [Tooltip("单次游荡时间限制")]
    public float wanderTimeLimit = 3f; // 单次游荡时间限制

    [Tooltip("搜索玩家的时间间隔")]
    public float searchInterval = 0.5f; // 搜索玩家的时间间隔

    [Tooltip("向初始位置偏移的有效距离")]
    public float biasTowardsInitialPositionDistance = 3f; // 向初始位置偏移的有效距离

    [Min(0)]
    [Tooltip("默认静默时间")]
    public float defaultIdleTime = 0f; // 默认静默时间

    [Min(0)]
    [Tooltip("最大额外静默时间")]
    public float maxIdleTimeRandom = 0f; // 最大额外静默时间

    private Transform player; // 玩家对象
    private Vector2 wanderTarget; // 随机游荡目标
    private Vector2 initialPosition; // 怪物初始位置
    private bool isChasing = false; // 是否在追踪玩家
    private float wanderTimer = 0f; // 游荡计时器
    private float waitTimeAtTarget = 0f; // 等待时间
    private bool waitingAtTarget = false; // 是否在目标点等待
    private float waitTimer = 0f; // 等待计时器

    void Start()
    {
        initialPosition = transform.position; // 记录初始位置
        StartCoroutine(SearchForPlayer()); // 启动搜索玩家的协程
        SetNewWanderTarget(); // 设置初始游荡目标
    }

    void Update()
    {
        if (player == null)
        {
            // 如果未找到玩家，仅保持游荡
            Wander();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRange)
        {
            isChasing = true; // 玩家进入追踪范围
        }
        else if (distanceToPlayer > chaseRange * 1.5f)
        {
            isChasing = false; // 玩家离开追踪范围
        }

        if (isChasing)
        {
            ChasePlayer(); // 追踪玩家
        }
        else
        {
            Wander(); // 随机游荡
        }
    }

    void Wander()
    {
        if (waitingAtTarget)
        {
            // 如果在目标点等待，更新等待计时器
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeAtTarget)
            {
                waitingAtTarget = false; // 等待结束，重新设置游荡目标
                waitTimer = 0f; // 重置等待计时器
                SetNewWanderTarget(); // 设置新的游荡目标
                wanderTimer = 0f; // 重置游荡计时器
            }
            return; // 如果在等待，不执行移动
        }

        // 添加朝向修正
        if (wanderTarget.x < transform.position.x)
        {
            // 如果目标在左侧，水平翻转
            transform.localScale = new Vector3(-1f, 1f, 1f); // 向左
        }
        else
        {
            // 如果目标在右侧，保持正常
            transform.localScale = new Vector3(1f, 1f, 1f); // 向右
        }

        // 移动到游荡目标
        transform.position = Vector2.MoveTowards(
            transform.position,
            wanderTarget,
            moveSpeed * Time.deltaTime
        );

        // 更新游荡计时器
        wanderTimer += Time.deltaTime;

        // 如果到达目标或者游荡时间超过限制，设置新的游荡目标
        if (
            Vector2.Distance(transform.position, wanderTarget) < 0.1f
            || wanderTimer >= wanderTimeLimit
        )
        {
            waitingAtTarget = true; // 开始等待
            waitTimeAtTarget = defaultIdleTime + Random.Range(0f, maxIdleTimeRandom);
            // SetNewWanderTarget();
            // wanderTimer = 0f; // 重置游荡计时器
        }
    }

    void SetNewWanderTarget()
    {
        // 计算向初始位置偏移的概率
        Vector2 randomOffset = new Vector2(
            Random.Range(-wanderRange, wanderRange),
            Random.Range(-wanderRange, wanderRange)
        );

        // 计算当前距离初始位置的距离
        float distanceFromInitial = Vector2.Distance(transform.position, initialPosition);

        // 判定偏向初始位置
        if (distanceFromInitial > biasTowardsInitialPositionDistance)
        {
            // 使用较高的概率选择接近初始位置的目标点
            if (Random.value < 0.7f) // 70%的概率选择向初始位置游荡
            {
                wanderTarget = initialPosition + randomOffset * 0.5f; // 向初始位置的目标
            }
            else
            {
                wanderTarget = (Vector2)transform.position + randomOffset; // 随机目标
            }
        }
        else
        {
            wanderTarget = (Vector2)transform.position + randomOffset; // 随机目标
        }
    }

    void ChasePlayer()
    {
        // 添加朝向修正
        if (player.position.x < transform.position.x)
        {
            // 如果目标在左侧，水平翻转
            transform.localScale = new Vector3(-1f, 1f, 1f); // 向左
        }
        else
        {
            // 如果目标在右侧，保持正常
            transform.localScale = new Vector3(1f, 1f, 1f); // 向右
        }

        // 追踪玩家
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );
    }

    private IEnumerator SearchForPlayer()
    {
        while (player == null)
        {
            player = GameObject.FindWithTag(playerTag)?.transform; // 搜索玩家
            yield return new WaitForSeconds(searchInterval);
        }
    }

    public override void EnemyDead()
    {
        MusicMgr.Instance.PlaySound(deathAudio, false);
    }
}
