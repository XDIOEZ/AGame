using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash_Temp : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 12f; // ����ٶ�
    [SerializeField] private float dashDuration = 0.2f; // ��̳���ʱ��
    [SerializeField] private float dashUpwardForce = 5f; // б���ϵĳ����
    [SerializeField] private LayerMask groundLayer; // ����㣬���ڼ������Ƿ��ڵ�����
    [SerializeField] private Transform groundCheck; // ���ڼ������Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // ���߼��ľ���

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canDash = true; // �Ƿ���Գ��
    [SerializeField] private bool isDashing = false; // �Ƿ����ڳ��
    [SerializeField] private float dashTime;
    private float lastInputDirection = 0f; // ��������뷽��
    private float inputDirectionCheckTime = 0.1f; // ���ڼ��������뷽���ʱ��
    private float inputDirectionTimer;
    private bool applyUpwardForce = false; // �Ƿ�Ӧ��б���ϵ���

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ʹ�����߼������Ƿ��ڵ�����
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // ������ڵ�����ʱ�����ó��״̬
        if (isGrounded)
        {
            canDash = true;
        }

        // ��¼��ҵ����뷽��
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            lastInputDirection = horizontalInput;
            inputDirectionTimer = inputDirectionCheckTime; // ���ü�ʱ��
        }
        else if (inputDirectionTimer > 0)
        {
            inputDirectionTimer -= Time.deltaTime; // ���ټ�ʱ��
        }

        // ����������
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // ������״̬�����ݼ�¼�����뷽�����ó���ٶ�
            float verticalVelocity = applyUpwardForce ? dashUpwardForce : rb.velocity.y;
            rb.velocity = new Vector2(lastInputDirection * dashSpeed, verticalVelocity);

            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0)
            {
                EndDash();
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        canDash = false; // ��̺����ٴγ�̣�ֱ�����

        // ֻ���ڿ���ʱ��Ӧ��б���ϵ���
        applyUpwardForce = !isGrounded;
    }

    void EndDash()
    {
        isDashing = false;
        applyUpwardForce = false;
    }

    private void OnDrawGizmosSelected()
    {
        // ���Ƽ������Gizmos�����ڵ���
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
