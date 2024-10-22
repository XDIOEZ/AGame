using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制玩家跳跃行为的类，包含跳跃的力度、地面检测和跳跃残留时间（Coyote Time）逻辑。
/// </summary>
public class PlayerJump_Temp : MonoBehaviour
{
    [Header("跳跃设置")]
    [SerializeField] private float jumpForce = 5f; // 跳跃的力度
    [SerializeField] private float jumpCoyoteTime = 0.05f; // 跳跃残留时间

    [Header("地面检测设置")]
    [SerializeField] private LayerMask groundLayer; // 地面层，用于检测玩家是否在地面上
    [SerializeField] private Transform groundCheck; // 用于检测地面的Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // 射线检测的距离

    private Rigidbody2D rb; // 玩家刚体组件
    private bool isGrounded; // 玩家是否在地面上
    private float coyoteTimeCounter; // 用于计时跳跃残留时间
    private float jumpTimer; // 跳跃计时器
    [SerializeField] private float jumpDuration = 1f; // 跳跃有效检测的持续时间


    // 记录垂直方向的移动
    public int verticalMovementDirection = 0; // 1表示向上，-1表示向下，0表示静止

    // 引用冲刺状态的脚本
    [SerializeField] private PlayerDash_Temp playerDash; // 冲刺控制脚本的引用

    /// <summary>
    /// 初始化玩家的刚体组件。
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 每帧更新玩家的跳跃逻辑，包括地面检测和处理输入。
    /// </summary>
    void Update()
    {
        CheckGroundStatus();
        HandleJumpInput(KeyCode.Space);
        UpdateVerticalMovementDirection();
    }

    /// <summary>
    /// 检查玩家是否在地面上，并更新 Coyote Time 计时器。
    /// </summary>
    private void CheckGroundStatus()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // 更新 Coyote Time 计时器
        coyoteTimeCounter = isGrounded ? jumpCoyoteTime : coyoteTimeCounter - Time.deltaTime;
    }

    /// <summary>
    /// 处理跳跃输入。
    /// </summary>
    /// <param name="jumpKey">跳跃按键</param>
    private void HandleJumpInput(KeyCode jumpKey)
    {
        // 检查是否正在冲刺，若是则不进行跳跃
        if (playerDash != null && playerDash.isDashing)
            return; // 如果正在冲刺，则跳过跳跃处理

        // 如果在上升阶段不允许冲刺
        if (rb.velocity.y > 0.1f)
            return; // 如果在上升阶段，则跳过跳跃处理

        if (Input.GetKeyDown(jumpKey) && coyoteTimeCounter > 0)
        {
            Jump();
        }
    }

    /// <summary>
    /// 处理跳跃动作，修改刚体的垂直速度以实现跳跃。
    /// </summary>
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        coyoteTimeCounter = 0;
        verticalMovementDirection = 1; // 记录为向上移动
        jumpTimer = jumpDuration; // 重置跳跃计时器
    }


    /// <summary>
    /// 更新垂直移动方向，检查在0.1秒内的运动状态
    /// </summary>
    private void UpdateVerticalMovementDirection()
    {
        // 如果跳跃计时器已过期，重置方向
        if (jumpTimer <= 0)
        {
            verticalMovementDirection = 0;
            return; // 退出，跳过检测
        }

        // 更新跳跃计时器
        jumpTimer -= Time.deltaTime;

        // 根据当前的垂直速度设置方向
        if (rb.velocity.y > 0.1f)
            verticalMovementDirection = 1; // 向上移动
        else if (rb.velocity.y < -0.1f)
            verticalMovementDirection = -1; // 向下移动

        // 可选: 你可以在这里输出当前的垂直移动方向到调试日志中
        //Debug.Log("Vertical Movement Direction: " + verticalMovementDirection);
    }


    /// <summary>
    /// 绘制检测地面的调试线条，用于可视化检测范围。
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
