using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 控制玩家跳跃行为的类，包含跳跃的力度、地面检测和跳跃残留时间（Coyote Time）逻辑。
/// </summary>
public class PlayerJump_Temp : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] private float jumpForce = 5f; // 跳跃的力度
    [SerializeField] private LayerMask groundLayer; // 地面层，用于检测玩家是否在地面上
    [SerializeField] private Transform groundCheck; // 用于检测地面的Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // 射线检测的距离，可调整的数值
    [SerializeField] private float jumpCoyoteTime = 0.05f; // 跳跃残留时间

    [SerializeField] private Rigidbody2D rb; // 玩家刚体组件
    [SerializeField] private bool isGrounded; // 玩家是否在地面上
    [SerializeField] private float coyoteTimeCounter; // 用于计时跳跃残留时间
    public bool isJumping;

    /// <summary>
    /// 初始化玩家的刚体组件。
    /// </summary>
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 每帧更新玩家的跳跃逻辑，包括地面检测和处理输入。
    /// </summary>
    void Update()
    {
        CheckGroundStatus(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        playerController.inputActions.PlayerAction.Jump.started += JumpSetting;
    }

    private void JumpSetting(InputAction.CallbackContext context)
    {
        if ( coyoteTimeCounter > 0)
        {
            Jump(jumpForce);
        }
    }

    /// <summary>
    /// 检查玩家是否在地面上，并更新 Coyote Time 计时器。
    /// </summary>
    /// <param name="position">检测起始位置</param>
    /// <param name="direction">检测方向</param>
    /// <param name="distance">检测距离</param>
    /// <param name="layer">检测层</param>
    private void CheckGroundStatus(Vector3 position, Vector2 direction, float distance, LayerMask layer)
    {
        isGrounded = Physics2D.Raycast(position, direction, distance, layer);

        if (isGrounded)
        {
            coyoteTimeCounter = jumpCoyoteTime;
            isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

   
   

    /// <summary>
    /// 处理跳跃动作，修改刚体的垂直速度以实现跳跃。
    /// </summary>
    /// <param name="force">跳跃力度</param>
    private void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, force);
        isJumping = true;
        coyoteTimeCounter = 0;
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
