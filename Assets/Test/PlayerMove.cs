using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("速度设置")]
    public float moveSpeed = 5f;         // 移动速度
    public float dashSpeed = 10f;        // 冲刺速度
    public float jumpSpeed = 7f;         // 跳跃速度
    public float XDashSpeedFactor = 1f;  // 水平方向速度因子
    public float YDashSpeedFactor = 1f;  // 垂直方向速度因子
    public float VerticalSpeedCorrectionValue = 1f; // 垂直速度修正值

    [Header("方向设置")]
    public Vector2 moveDirection;
    public Vector2 dashDirection;
    public int PlayerDirection; // 玩家方向
    private int lastPlayerDirection; // 上一次的方向

    [Header("时间设置")]
    public float dashDuration = 0.2f;     // 冲刺持续时间
    public float dashTimer;               // 冲刺计时器

    [Header("状态设置")]
    public bool isDashing = false;       // 冲刺状态
    public int dashCount = 1;             // 冲刺次数，默认为1次冲刺
    public bool isJumping = false;        // 跳跃状态
    public int jumpCount = 1;             // 跳跃次数，默认为1次跳跃

    public bool isGrounded;               // 玩家是否在地面上
    public float jumpTimer;               // 跳跃计时器
    public bool isMoving;                 // 是否正在移动

    [Header("检测设置")]
    public LayerMask groundLayer = 1 << 8; // 地面层，用于射线检测
    public Transform groundCheckPoint;     // 射线起点，通常放在玩家脚下的位置
    public float groundCheckRadius = 1.5f; // 射线检测的距离，可以微调
    public Rigidbody2D rb2d;
    public float jumpGraceTime = 0.1f;     // 跳跃后不进行地面检测的时间

    [Header("锁定设置")]
    public bool isLockingMove; // 是否锁定移动
    public bool isLockingDash; // 是否锁定冲刺
    public bool isLockingJump; // 是否锁定跳跃

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  // 获取 Rigidbody2D 组件
        groundCheckPoint = transform; // 默认地面检测点为玩家脚下位置
        lastPlayerDirection = PlayerDirection; // 初始化上一次的方向
    }

    private void Update()
    {
        // 移动
        if (!isDashing && !isLockingMove)
        {
            rb2d.velocity = new Vector2(moveDirection.x, rb2d.velocity.y);
            Move();
        }

        // 跳跃
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0 && !isDashing && !isLockingJump)
        {
            Jump();
        }

        // 冲刺
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCount > 0 && !isDashing && !isLockingDash)
        {
            Dash();
        }

        // 冲刺状态更新
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }

        // 更新跳跃计时器
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // 进行地面检测（只有当不在冲刺状态下）
        if (!isDashing)
        {
            CheckIfGrounded();
        }
    }

    void Move()
    {
        // 获取玩家水平输入
        float moveInput = Input.GetAxis("Horizontal");

        // 目标速度 = 水平输入 * 移动速度
        moveDirection = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);

        // 设置玩家方向
        int lastPlayerDirection = PlayerDirection; // 记录上一次的方向

        if (moveInput != 0)
        {
            PlayerDirection = moveInput > 0 ? 1 : -1; // 1表示右，-1表示左
            isMoving = true; // 玩家正在移动

            // 检查方向是否改变
            if (PlayerDirection != lastPlayerDirection)
            {
                EventCenter.Instance.EventTrigger<object>("PlayerDirectionChanged", this);
                Debug.Log("PlayerDirectionChanged");

                lastPlayerDirection = PlayerDirection; // 更新上一次的方向
            }

            // 根据玩家的方向来翻转角色的Transform
            Vector3 localScale = transform.localScale;
            localScale.x = PlayerDirection > 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
        else
        {
            isMoving = false; // 玩家停止移动
        }
    }


    void Jump()
    {
        // 跳跃时赋予向上的速度
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        jumpCount--; // 减少跳跃次数
        jumpTimer = jumpGraceTime; // 重置跳跃计时器
        isJumping = true; // 设置跳跃状态为真
    }

    void Dash()
    {
        // 获取玩家的输入并归一化
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        dashDirection = new Vector2(inputX * XDashSpeedFactor, inputY * YDashSpeedFactor).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(PlayerDirection, VerticalSpeedCorrectionValue).normalized; // 默认向当前方向
        }

        // 设置冲刺速度
        rb2d.velocity = new Vector2(dashDirection.x * dashSpeed, dashDirection.y * dashSpeed);

        // 冲刺状态
        isDashing = true;

        dashCount--; // 每次冲刺减少一次冲刺次数
        dashTimer = dashDuration;
    }

    void EndDash()
    {
        isDashing = false;
        // 冲刺结束后的状态恢复
    }

    // 射线检测玩家是否在地面上
    void CheckIfGrounded()
    {
        if (jumpTimer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckRadius, groundLayer);
            isGrounded = hit.collider != null;

            if (isGrounded)
            {
                jumpCount = 1; // 恢复跳跃次数
                dashCount = 1; // 恢复冲刺次数
                isJumping = false; // 重置跳跃状态
            }
        }
        Debug.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * groundCheckRadius, Color.red);
    }
}
