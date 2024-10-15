using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_temp : MonoBehaviour
{
    public float moveSpeed = 5f;        // �ƶ��ٶ�
    public float dashSpeed = 10f;        // ����ٶ�
    public float dashDuration = 0.2f;     // ��̳���ʱ��
    public float dashCooldown = 1f;       // �����ȴʱ��

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isDashing = false;
    private float dashTime;
    private float lastDashTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ��ȡ����
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        // �����
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown && moveDirection != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            // �ƶ���ɫ
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        float dashEndTime = Time.time + dashDuration;

        while (Time.time < dashEndTime)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * dashSpeed * Time.fixedDeltaTime);
            yield return null; // �ȴ���һ֡
        }

        isDashing = false;
    }
}
