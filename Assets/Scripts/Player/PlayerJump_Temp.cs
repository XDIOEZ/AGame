using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump_Temp : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; // 跳跃的力度
    [SerializeField] private LayerMask groundLayer; // 地面层，用于检测玩家是否在地面上
    [SerializeField] private Transform groundCheck; // 用于检测地面的Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // 射线检测的距离，可调整的数值
    [SerializeField] private float jumpCoyoteTime = 0.05f; // 跳跃残留时间

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float coyoteTimeCounter; // 用于计时跳跃残留时间

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 使用射线检测玩家是否在地面上
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // 根据是否在地面上重置或更新跳跃残留计时器
        if (isGrounded)
        {
            coyoteTimeCounter = jumpCoyoteTime; // 重置计时器
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // 逐渐减少计时器
        }

        // 处理跳跃输入，在跳跃残留时间内也可以跳跃
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        coyoteTimeCounter = 0; // 跳跃后立即清零残留时间，防止多次跳跃
    }

    private void OnDrawGizmosSelected()
    {
        // 绘制检测地面的Gizmos，用于调试
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
