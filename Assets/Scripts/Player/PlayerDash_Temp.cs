using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制玩家冲刺行为的类，包含冲刺速度、持续时间以及地面检测逻辑。
/// </summary>
public class PlayerDash_Temp : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 12f; // 冲刺速度
    [SerializeField] private float dashDuration = 0.2f; // 冲刺持续时间
    [SerializeField] private float dashUpwardForce = 5f; // 斜向上的冲刺力
    [SerializeField] private LayerMask groundLayer; // 地面层，用于检测玩家是否在地面上
    [SerializeField] private Transform groundCheck; // 用于检测地面的Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // 射线检测的距离

    [SerializeField] private Rigidbody2D rb; // 玩家刚体组件
    [SerializeField] private bool isGrounded; // 玩家是否在地面上
    [SerializeField] private bool canDash = true; // 是否可以冲刺
    [SerializeField] private bool isDashing = false; // 是否正在冲刺
    [SerializeField] private float dashTime; // 当前冲刺剩余时间
    private float lastInputDirection = 0f; // 最近的输入方向
    private float inputDirectionCheckTime = 0.1f; // 用于检测玩家输入方向的时间
    private float inputDirectionTimer; // 输入方向计时器
    private bool applyUpwardForce = false; // 是否应用斜向上的力

    /// <summary>
    /// 初始化玩家的刚体组件。
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 更新冲刺逻辑，包括地面检测、输入方向记录和冲刺输入处理。
    /// </summary>
    void Update()
    {
        // 使用射线检测玩家是否在地面上
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // 当玩家在地面上时，重置冲刺状态
        if (isGrounded)
        {
            canDash = true;
        }

        // 记录玩家的输入方向
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            lastInputDirection = horizontalInput;
            inputDirectionTimer = inputDirectionCheckTime; // 重置计时器
        }
        else if (inputDirectionTimer > 0)
        {
            inputDirectionTimer -= Time.deltaTime; // 减少计时器
        }

        // 处理冲刺输入
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartDash();
        }
    }

    /// <summary>
    /// 在物理更新中处理冲刺逻辑，设置冲刺速度。
    /// </summary>
    void FixedUpdate()
    {
        if (isDashing)
        {
            // 处理冲刺状态，根据记录的输入方向设置冲刺速度
            float verticalVelocity = applyUpwardForce ? dashUpwardForce : rb.velocity.y;
            rb.velocity = new Vector2(lastInputDirection * dashSpeed, verticalVelocity);

            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0)
            {
                EndDash();
            }
        }
    }

    /// <summary>
    /// 启动冲刺，设置冲刺状态和时间。
    /// </summary>
    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        canDash = false; // 冲刺后不能再次冲刺，直到落地

        // 只有在空中时才应用斜向上的力
        applyUpwardForce = !isGrounded;
    }

    /// <summary>
    /// 结束冲刺，重置冲刺状态。
    /// </summary>
    void EndDash()
    {
        isDashing = false;
        applyUpwardForce = false;
    }

    /// <summary>
    /// 绘制检测地面的调试线条，用于可视化检测范围。
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
