using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Temp : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 获取水平输入
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f; // 横板游戏不需要垂直移动
    }

    void FixedUpdate()
    {
        // 正常移动
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}
