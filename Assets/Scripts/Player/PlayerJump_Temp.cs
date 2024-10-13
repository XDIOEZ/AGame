using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������Ծ��Ϊ���࣬������Ծ�����ȡ����������Ծ����ʱ�䣨Coyote Time���߼���
/// </summary>
public class PlayerJump_Temp : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; // ��Ծ������
    [SerializeField] private LayerMask groundLayer; // ����㣬���ڼ������Ƿ��ڵ�����
    [SerializeField] private Transform groundCheck; // ���ڼ������Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // ���߼��ľ��룬�ɵ�������ֵ
    [SerializeField] private float jumpCoyoteTime = 0.05f; // ��Ծ����ʱ��

    [SerializeField] private Rigidbody2D rb; // ��Ҹ������
    [SerializeField] private bool isGrounded; // ����Ƿ��ڵ�����
    [SerializeField] private float coyoteTimeCounter; // ���ڼ�ʱ��Ծ����ʱ��

    /// <summary>
    /// ��ʼ����ҵĸ��������
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ÿ֡������ҵ���Ծ�߼�������������ʹ������롣
    /// </summary>
    void Update()
    {
        // ʹ�����߼������Ƿ�Ӵ�����
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // �����ҽӴ����棬���� Coyote Time ��ʱ��
        if (isGrounded)
        {
            coyoteTimeCounter = jumpCoyoteTime;
        }
        else
        {
            // �����Ҳ��ڵ����ϣ����� Coyote Time ��ʱ����ֵ
            coyoteTimeCounter -= Time.deltaTime;
        }

        // �����Ծ���룬����Ұ��¿ո������ Coyote Time ��ʱ������ 0 ʱ������Ծ
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0)
        {
            Jump();
        }
    }

    /// <summary>
    /// ������Ծ�������޸ĸ���Ĵ�ֱ�ٶ���ʵ����Ծ��
    /// </summary>
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ����ˮƽ�ٶȲ��䣬�޸Ĵ�ֱ�ٶ���ʵ����Ծ
        coyoteTimeCounter = 0; // ��Ծ�������������ʱ�䣬��ֹ�����Ծ
    }

    /// <summary>
    /// ���Ƽ�����ĵ������������ڿ��ӻ���ⷶΧ��
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
