using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制玩家移动行为的类，支持平滑的水平移动和阻尼效果。
/// </summary>
public class PlayerMovement_Temp : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // 移动速度
    [SerializeField] private Rigidbody2D rb; // 角色的 Rigidbody2D 组件
    [SerializeField] private Vector2 movement; // 移动输入向量

    // 用于平滑停止的阻尼参数
    [SerializeField] private float damping = 0.1f; // 阻尼，用于平滑停止

    // 朝向
    public Vector2 lookDirection;

    /// <summary>
    /// 初始化玩家的刚体组件。
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lookDirection = new Vector2(1, 0);
    }

    /// <summary>
    /// 更新输入向量，每帧检测玩家的水平移动输入。
    /// </summary>
    void Update()
    {
        // 获取水平输入
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f; // 横板游戏不需要垂直移动

        GetDirection();
    }

    /// <summary>
    /// 使用固定时间步长处理物理更新，更新玩家的移动速度。
    /// </summary>
    void FixedUpdate()
    {
        UpdatePlayerMovement(movement.x * moveSpeed); // 更新玩家移动
    }

    /// <summary>
    /// 更新玩家的移动速度，使用力的方式实现移动。
    /// </summary>
    /// <param name="targetSpeed">目标速度</param>
    private void UpdatePlayerMovement(float targetSpeed)
    {
        // 计算期望的速度和当前速度的差异
        float speedDifference = targetSpeed - rb.velocity.x;

        // 将力施加到刚体上，使用阻尼
        float force = speedDifference / Time.fixedDeltaTime; // 计算需要施加的力
        force *= (1f - damping); // 应用阻尼

        // 施加力
        rb.AddForce(new Vector2(force, 0), ForceMode2D.Force);

        // 反转朝向
        if (movement.x != 0 && Mathf.Sign(movement.x) != Mathf.Sign(transform.localScale.x))
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    // 记录玩家输入
    public void GetDirection()
    {
        if (movement.x != 0)
        {
            lookDirection = movement;
            
        }
    }
}
