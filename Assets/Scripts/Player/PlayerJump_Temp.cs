using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制玩家跳跃行为的类，包含跳跃的力度、地面检测和跳跃残留时间（Coyote Time）逻辑。
/// </summary>
public class PlayerJump_Temp : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; // 跳跃的力度
    [SerializeField] private LayerMask groundLayer; // 地面层，用于检测玩家是否在地面上
    [SerializeField] private Transform groundCheck; // 用于检测地面的Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // 射线检测的距离，可调整的数值
    [SerializeField] private float jumpCoyoteTime = 0.05f; // 跳跃残留时间

    [SerializeField] private Rigidbody2D rb; // 玩家刚体组件
    [SerializeField] private bool isGrounded; // 玩家是否在地面上
    [SerializeField] private float coyoteTimeCounter; // 用于计时跳跃残留时间

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
        // 使用射线检测玩家是否接触地面
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // 如果玩家接触地面，重置 Coyote Time 计时器
        if (isGrounded)
        {
            coyoteTimeCounter = jumpCoyoteTime;
        }
        else
        {
            // 如果玩家不在地面上，减少 Coyote Time 计时器的值
            coyoteTimeCounter -= Time.deltaTime;
        }

        // 检测跳跃输入，当玩家按下空格键并且 Coyote Time 计时器大于 0 时允许跳跃
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0)
        {
            Jump();
        }
    }

    /// <summary>
    /// 处理跳跃动作，修改刚体的垂直速度以实现跳跃。
    /// </summary>
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 保持水平速度不变，修改垂直速度来实现跳跃
        coyoteTimeCounter = 0; // 跳跃后立即清零残留时间，防止多次跳跃
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
