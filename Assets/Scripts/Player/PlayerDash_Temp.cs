using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制玩家冲刺行为的类，包含冲刺速度、持续时间以及地面检测逻辑。
/// </summary>
public class PlayerDash_Temp : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 12f; // 冲刺速度
    [SerializeField] private float dashSpeedFactor = 1.0f; // 冲刺速度比例因子
    [SerializeField] private float dashDuration = 0.2f; // 冲刺持续时间
    [SerializeField] private float dashUpwardForce = 5f; // 斜向上的冲刺力
    [SerializeField] private float horizontalDashFactor = 1.0f; // 水平冲刺系数
    [SerializeField] private float verticalDashFactor = 1.0f; // 垂直冲刺系数
    [SerializeField] private LayerMask groundLayer; // 地面层，用于检测玩家是否在地面上
    [SerializeField] private Transform groundCheck; // 用于检测地面的Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // 射线检测的距离

    [SerializeField] private Rigidbody2D rb; // 玩家刚体组件
    [SerializeField] private bool isGrounded; // 玩家是否在地面上
    [SerializeField] private bool canDash = true; // 是否可以冲刺
    [SerializeField] private bool isDashing = false; // 是否正在冲刺
    [SerializeField] private float dashTime; // 当前冲刺剩余时间
    [SerializeField] private float lastInputDirection = 0f; // 最近的输入方向
    [SerializeField] private float inputDirectionCheckTime = 0.1f; // 用于检测玩家输入方向的时间
    [SerializeField] private float inputDirectionTimer; // 输入方向计时器
    [SerializeField] private bool applyUpwardForce = false; // 是否应用斜向上的力

    /// <summary>
    /// 初始化玩家的刚体组件。
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 每帧更新冲刺逻辑，包括地面检测、输入方向记录和冲刺输入处理。
    /// </summary>
    void Update()
    {
        CheckGroundStatus(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer); // 检查玩家是否在地面上
        RecordInputDirection(Input.GetAxisRaw("Horizontal")); // 记录玩家的输入方向
        HandleDashInput(KeyCode.LeftShift); // 处理冲刺输入
    }

    /// <summary>
    /// 在物理更新中处理冲刺逻辑。
    /// </summary>
    void FixedUpdate()
    {
        if (isDashing)
        {
            PerformDash(lastInputDirection, dashSpeed * dashSpeedFactor, dashUpwardForce); // 执行冲刺
        }
    }

    /// <summary>
    /// 检查玩家是否在地面上，并重置冲刺状态。
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
            canDash = true;
        }
    }

    /// <summary>
    /// 记录玩家的输入方向。
    /// </summary>
    /// <param name="horizontalInput">水平输入值</param>
    private void RecordInputDirection(float horizontalInput)
    {
        if (horizontalInput != 0)
        {
            lastInputDirection = horizontalInput;
            inputDirectionTimer = inputDirectionCheckTime;
        }
        else if (inputDirectionTimer > 0)
        {
            inputDirectionTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 处理冲刺输入。
    /// </summary>
    /// <param name="dashKey">冲刺按键</param>
    private void HandleDashInput(KeyCode dashKey)
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartDash();
        }
    }

    /// <summary>
    /// 在物理更新中处理冲刺逻辑。
    /// </summary>
    /// <param name="direction">冲刺方向</param>
    /// <param name="speed">冲刺速度</param>
    /// <param name="upwardForce">斜向上的冲刺力</param>
    private void PerformDash(float direction, float speed, float upwardForce)
    {
        float horizontalForce = direction * speed * horizontalDashFactor;
        float verticalForce = 0f;

        // 检测是否单独按下了 "W" 键，如果是，则只添加向上的力，并将水平力设为0
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            horizontalForce = 0f; // 不添加左右方向的力
            verticalForce = upwardForce * verticalDashFactor; // 向上的力
        }
        else
        {
            // 如果按下了 "S" 键，则垂直力向下，否则根据applyUpwardForce决定是否向上
            verticalForce = Input.GetKey(KeyCode.S) ? -upwardForce * verticalDashFactor : (applyUpwardForce ? upwardForce * verticalDashFactor : 0f);
        }

        Vector2 dashForce = new Vector2(horizontalForce, verticalForce);

        // 使用Impulse模式来立即应用力，模拟瞬时冲刺效果
        rb.AddForce(dashForce, ForceMode2D.Impulse);

        dashTime -= Time.fixedDeltaTime;
        if (dashTime <= 0)
        {
            EndDash();
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
