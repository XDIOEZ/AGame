using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小人状态下玩家的移动控制类.
/// </summary>
public class LittleStateMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // 移动速度
    [SerializeField] private bool canMove = true; // 表示玩家是否可以移动
    [SerializeField] private Vector2 moveDirection; // 移动方向
    [SerializeField] private PlayerLittleState playerLittleState; // 引用 PlayerLittleState 组件
    [SerializeField] private Rigidbody2D rb2D; // 父对象的 Rigidbody2D 组件
    public PlayerMove playerMove;

    [Header("光球移动方向")]
    public bool 上 = false;
    public bool 下 = false;
    public bool 左 = false;
    public bool 右 = false;

    void Start()
    {
        playerLittleState = GetComponent<PlayerLittleState>(); // 获取 PlayerLittleState 组件
        //获取父对象的PlayerMove组件
         playerMove = GetComponentInParent<PlayerMove>();
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
        if (playerMove.isDashing ==true)
        {
            playerMove.dashCount++;
            playerLittleState.ChangeToBig();
        }
        if (playerMove.isJumping == true)
        {
            playerMove.jumpCount++;
            playerLittleState.ChangeToBig();
        }
        归位();
    }
    private void 归位()
    {
        上 = false;
        下 = false;
        左 = false;
        右 = false;
    }
    private float 方向转换(bool 正, bool 反)
    {
        if (正 && !反) return -1;
        if (反 && !正) return 1;
        return 0;
    }
    private void ProcessInput()
    {
        if (canMove)
        {
            // 获取 WASD 输入
            float horizontalInput = 方向转换(左 ,右);
            float verticalInput = 方向转换(下,上);

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
}
