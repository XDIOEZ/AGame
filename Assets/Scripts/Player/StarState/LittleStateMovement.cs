using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}

/// <summary>
/// LittleStateMovement类控制小人状态下玩家的移动行为。
/// <para>使用说明：</para>
/// <para>1. 将此脚本挂载到玩家的小人状态游戏对象上。</para>
/// <para>2. 在Unity编辑器中，确保将PlayerLittleState和PlayerMove组件的引用正确设置。</para>
/// <para>3. 调整moveSpeed属性以更改移动速度。</para>
/// <para>4. 通过设置上下左右的布尔值（isUp, isDown, isLeft, isRight）来控制移动方向。</para>
/// <para>5. 确保在需要移动的情况下调用ProcessInput()方法来处理输入。</para>
/// <para>6. 当玩家触发冲刺或跳跃时，组件自动切换回大人状态。</para>
/// </summary>
public class LittleStateMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f; // 移动速度

    [SerializeField]
    private bool canMove = true; // 表示玩家是否可以移动

    [SerializeField]
    private Vector2 moveDirection; // 移动方向

    [SerializeField]
    private PlayerLittleState playerLittleState; // 引用 PlayerLittleState 组件

    [SerializeField]
    private Rigidbody2D rb2D; // 父对象的 Rigidbody2D 组件
    public PlayerMove playerMove;

    [Header("光球移动方向")]
    public bool isUp = false; // 上
    public bool isDown = false; // 下
    public bool isLeft = false; // 左
    public bool isRight = false; // 右

    void Start()
    {
        playerLittleState = GetComponent<PlayerLittleState>(); // 获取 PlayerLittleState 组件
        // 获取父对象的 PlayerMove 组件
        playerMove = GetComponentInParent<PlayerMove>();

        EventCenter.Instance.AddEventListener<Direction>("光球移动", OnLightBallMove);
    }

    void OnEnable()
    {
        canMove = true; // 激活组件时允许移动
    }

    void OnDisable()
    {
        canMove = false; // 失活组件时禁止移动
        moveDirection = Vector2.zero; // 清零移动方向
    }

    void Update()
    {
        ProcessInput(); // 处理输入
        if (playerMove.isDashing == true)
        {
            playerMove.dashCount++;
            playerLittleState.ChangeToBig();
        }
        if (playerMove.isJumping == true)
        {
            playerMove.jumpCount++;
            playerLittleState.ChangeToBig();
        }
        ResetDirection(); // 归位
    }

    private void ResetDirection()
    {
        isUp = false;
        isDown = false;
        isLeft = false;
        isRight = false;
    }

    private float DirectionTransform(bool positive, bool negative)
    {
        if (positive && !negative)
            return -1;
        if (negative && !positive)
            return 1;
        return 0;
    }

    private void ProcessInput()
    {
        if (canMove)
        {
            // 获取 WASD 输入
            float horizontalInput = DirectionTransform(isLeft, isRight);
            float verticalInput = DirectionTransform(isDown, isUp);

            // 仅允许朝一个方向移动
            if (horizontalInput != 0)
            {
                moveDirection = new Vector2(horizontalInput, 0).normalized; // 水平移动
                canMove = false; // 设为不可以继续移动
            }
            else if (verticalInput != 0)
            {
                moveDirection = new Vector2(0, verticalInput).normalized; // 垂直移动
                canMove = false; // 设为不可以继续移动
            }

            // 如果有移动方向，执行移动
            if (moveDirection != Vector2.zero)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        Rigidbody2D rb2D = GetComponentInParent<Rigidbody2D>(); // 获取父对象的 Rigidbody2D 组件
        // 使用 Rigidbody2D 进行平移，根据移动方向和速度
        rb2D.velocity = moveDirection * moveSpeed;
    }

    private void OnLightBallMove(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                isUp = true;
                break;
            case Direction.Down:
                isDown = true;
                break;
            case Direction.Left:
                isLeft = true;
                break;
            case Direction.Right:
                isRight = true;
                break;
        }
        canMove = true; // 允许移动
    }
}
