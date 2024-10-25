using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTEye : MonoBehaviour
{
    public float moveSpeed = 2f; // 移动速度
    public float smoothTime = 0.2f; // 缓动时间
    private Vector3 currentVelocity = Vector3.zero; // 当前速度
    private Rigidbody2D rb; // Rigidbody2D 组件

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }

        moveDirection.Normalize();
        Vector3 targetPosition = moveDirection * moveSpeed;

        rb.MovePosition(
            Vector3.SmoothDamp(
                transform.position,
                transform.position + targetPosition,
                ref currentVelocity,
                smoothTime
            )
        );
    }
}
