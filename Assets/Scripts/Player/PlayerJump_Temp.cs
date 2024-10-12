using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump_Temp : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; // ��Ծ������
    [SerializeField] private LayerMask groundLayer; // ����㣬���ڼ������Ƿ��ڵ�����
    [SerializeField] private Transform groundCheck; // ���ڼ������Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // ���߼��ľ��룬�ɵ�������ֵ
    [SerializeField] private float jumpCoyoteTime = 0.05f; // ��Ծ����ʱ��

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float coyoteTimeCounter; // ���ڼ�ʱ��Ծ����ʱ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ʹ�����߼������Ƿ��ڵ�����
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // �����Ƿ��ڵ��������û������Ծ������ʱ��
        if (isGrounded)
        {
            coyoteTimeCounter = jumpCoyoteTime; // ���ü�ʱ��
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // �𽥼��ټ�ʱ��
        }

        // ������Ծ���룬����Ծ����ʱ����Ҳ������Ծ
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        coyoteTimeCounter = 0; // ��Ծ�������������ʱ�䣬��ֹ�����Ծ
    }

    private void OnDrawGizmosSelected()
    {
        // ���Ƽ������Gizmos�����ڵ���
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
