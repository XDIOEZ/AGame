using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Temp : MonoBehaviour
{
    public float moveSpeed = 5f; // �ƶ��ٶ�
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ��ȡˮƽ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f; // �����Ϸ����Ҫ��ֱ�ƶ�
    }

    void FixedUpdate()
    {
        // �����ƶ�
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}
