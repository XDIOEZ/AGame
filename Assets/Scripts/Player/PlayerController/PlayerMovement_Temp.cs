using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制玩家移动行为的类，支持平滑的水平移动和阻尼效果。
/// </summary>
public class PlayerMovement_Temp : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // 移动速度
    [SerializeField] private float acceleration = 10f; // 加速度，用于控制力的大小
    [SerializeField] private Rigidbody2D rb; // 角色的 Rigidbody2D 组件
    [SerializeField] private Vector2 movement; // 移动输入向量

    // 用于平滑停止的阻尼参数
    [SerializeField] private float damping = 0.1f; // 阻尼，用于平滑停止

    /// <summary>
    /// 初始化玩家的刚体组件。
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 更新输入向量，每帧检测玩家的水平移动输入。
    /// </summary>
    void Update()
    {
        // 获取水平输入
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f; // 横板游戏不需要垂直移动
    }

    /// <summary>
    /// 使用固定时间步长处理物理更新，更新玩家的移动速度。
    /// </summary>
    void FixedUpdate()
    {
        UpdatePlayerMovement(movement.x * moveSpeed, damping); // 更新玩家移动
    }

    /// <summary>
    /// 更新玩家的移动速度，使用力来推动玩家而不是直接设置速度。
    /// </summary>
    /// <param name="targetSpeed">目标速度</param>
    /// <param name="damping">阻尼值</param>
    private void UpdatePlayerMovement(float targetSpeed, float damping)
    {
        // 计算当前速度与目标速度之间的差异
        float speedDifference = targetSpeed - rb.velocity.x;

        // 计算所需的力，根据差异来决定
        float force = speedDifference * acceleration * (1f - damping);

        // 添加瞬时力来更新玩家的速度
        rb.AddForce(new Vector2(force, 0f), ForceMode2D.Force);
    }
}
