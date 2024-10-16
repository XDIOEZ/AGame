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
    [SerializeField] private LayerMask Ground; // 地面层，用于检测玩家是否在地面上
    [SerializeField] private Transform groundCheck; // 用于检测地面的Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // 射线检测的距离

    [SerializeField] private float 水平无控制冲刺系数 = 0.5f; // 冲刺冷却时间

    [SerializeField] private Rigidbody2D rb; // 玩家刚体组件
    [SerializeField] private bool isGrounded; // 玩家是否在地面上
    [SerializeField] private bool canDash = true; // 是否可以冲刺
    [SerializeField] private bool isDashing = false; // 是否正在冲刺
    [SerializeField] private float dashTime; // 当前冲刺剩余时间
    [SerializeField] private float lastInputDirection; // 最近的输入方向
    [SerializeField] private float inputDirectionCheckTime = 0.1f; // 用于检测玩家输入方向的时间
    [SerializeField] private float inputDirectionTimer; // 输入方向计时器
    [SerializeField] private bool applyUpwardForce; // 是否应用斜向上的力


    [SerializeField] private int remainingDashes = 1; // 剩余冲刺次数

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGroundStatus();
        RecordInputDirection(Input.GetAxisRaw("Horizontal"));
        HandleDashInput(KeyCode.LeftShift);

        if (isGrounded && isDashing == false)
        {
            remainingDashes = 1; // 重置冲刺次数
            canDash = true;
            Debug.Log("Grounded!");
        }
        if(remainingDashes <= 0)
        {
            canDash = false; // 冲刺次数用完
            Debug.Log("No more dashes left!");
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            PerformDash(lastInputDirection, dashSpeed * dashSpeedFactor, dashUpwardForce);
        }
    }

    private void CheckGroundStatus()
    {
        // 检查玩家是否在地面上，但不影响冲刺的能力
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, Ground);
    }

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

    private void HandleDashInput(KeyCode dashKey)
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartDash();
        }
    }

    private void PerformDash(float direction, float speed, float upwardForce)
    {
        float horizontalForce = 0f;
        float verticalForce = 0f;

        // 检查 "W" 键是否按下
        if (Input.GetKey(KeyCode.W))
        {
            // 添加向上的力
            verticalForce = upwardForce * verticalDashFactor;

            // 如果同时按下 "A" 或 "D"，则在向上力的基础上增加水平力
            if (Input.GetKey(KeyCode.A))
            {
                horizontalForce = -horizontalDashFactor * speed; // 左水平冲刺
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalForce = horizontalDashFactor * speed; // 右水平冲刺
            }
        }
        // 检查 "S" 键是否按下
        else if (Input.GetKey(KeyCode.S))
        {
            // 当按下 "S" 键，设置垂直力为向下
            verticalForce = -upwardForce * verticalDashFactor;
            if (Input.GetKey(KeyCode.A))
            {
                horizontalForce = -horizontalDashFactor * speed; // 左水平冲刺
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalForce = horizontalDashFactor * speed; // 右水平冲刺
            }
        }
        // 检查是否在空中且没有按下其他方向键
        else if (isDashing)
        {
            // 在空中单独按下冲刺按钮时，根据 lastInputDirection 执行水平冲刺
            horizontalForce = lastInputDirection * speed * horizontalDashFactor;

            // 如果单按 "A" 或 "D" 也能进行水平冲刺
            if (Input.GetKey(KeyCode.A))
            {
                horizontalForce = -horizontalDashFactor * speed; // 左水平冲刺
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalForce = horizontalDashFactor * speed; // 右水平冲刺
            }
        }
        else
        {
            // 默认情况下不添加垂直力
            verticalForce = applyUpwardForce ? upwardForce * verticalDashFactor : 0f;
        }

        // 形成冲刺的力
        Vector2 dashForce = new Vector2(horizontalForce, verticalForce);
        // 应用冲刺力
        rb.AddForce(dashForce, ForceMode2D.Impulse);

        // 更新冲刺的剩余时间
        dashTime -= Time.fixedDeltaTime;
        // 如果冲刺时间结束，调用结束冲刺方法
        if (dashTime <= 0)
        {
            EndDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        remainingDashes--; // 冲刺后减少冲刺次数
        Debug.Log("Dashing!");
        // 只有在空中时才应用斜向上的力
        applyUpwardForce = !isGrounded;
    }

    private void EndDash()
    {
        isDashing = false;
        applyUpwardForce = false;

        // 如果落地，重置冲刺次数
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
