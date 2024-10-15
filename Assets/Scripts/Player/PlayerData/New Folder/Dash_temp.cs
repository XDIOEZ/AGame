using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_temp : MonoBehaviour
{
    public float moveSpeed = 5f;        // 移动速度
    public float dashSpeed = 10f;        // 冲刺速度
    public float dashDuration = 0.2f;     // 冲刺持续时间
    public float dashCooldown = 1f;       // 冲刺冷却时间

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
        // 获取输入
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        // 检测冲刺
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown && moveDirection != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            // 移动角色
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
            yield return null; // 等待下一帧
        }

        isDashing = false;
    }
}
