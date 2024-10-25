using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTEye : MonoBehaviour
{
    public float moveSpeed = 8f; // 移动速度
    private Rigidbody2D rb; // Rigidbody2D 组件

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector2.right;
        }

        moveDirection.Normalize();
        
        rb.velocity = moveDirection * moveSpeed;
    }
}
