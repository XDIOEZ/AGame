using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������Ծ��Ϊ���࣬������Ծ�����ȡ����������Ծ����ʱ�䣨Coyote Time���߼���
/// </summary>
public class PlayerJump_Temp : MonoBehaviour
{
    [Header("��Ծ����")]
    [SerializeField] private float jumpForce = 5f; // ��Ծ������
    [SerializeField] private float jumpCoyoteTime = 0.05f; // ��Ծ����ʱ��

    [Header("����������")]
    [SerializeField] private LayerMask groundLayer; // ����㣬���ڼ������Ƿ��ڵ�����
    [SerializeField] private Transform groundCheck; // ���ڼ������Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // ���߼��ľ���

    private Rigidbody2D rb; // ��Ҹ������
    private bool isGrounded; // ����Ƿ��ڵ�����
    private float coyoteTimeCounter; // ���ڼ�ʱ��Ծ����ʱ��
    private float jumpTimer; // ��Ծ��ʱ��
    [SerializeField] private float jumpDuration = 1f; // ��Ծ��Ч���ĳ���ʱ��


    // ��¼��ֱ������ƶ�
    public int verticalMovementDirection = 0; // 1��ʾ���ϣ�-1��ʾ���£�0��ʾ��ֹ

    // ���ó��״̬�Ľű�
    [SerializeField] private PlayerDash_Temp playerDash; // ��̿��ƽű�������

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
        CheckGroundStatus();
        HandleJumpInput(KeyCode.Space);
        UpdateVerticalMovementDirection();
    }

    /// <summary>
    /// �������Ƿ��ڵ����ϣ������� Coyote Time ��ʱ����
    /// </summary>
    private void CheckGroundStatus()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // ���� Coyote Time ��ʱ��
        coyoteTimeCounter = isGrounded ? jumpCoyoteTime : coyoteTimeCounter - Time.deltaTime;
    }

    /// <summary>
    /// ������Ծ���롣
    /// </summary>
    /// <param name="jumpKey">��Ծ����</param>
    private void HandleJumpInput(KeyCode jumpKey)
    {
        // ����Ƿ����ڳ�̣������򲻽�����Ծ
        if (playerDash != null && playerDash.isDashing)
            return; // ������ڳ�̣���������Ծ����

        // ����������׶β�������
        if (rb.velocity.y > 0.1f)
            return; // ����������׶Σ���������Ծ����

        if (Input.GetKeyDown(jumpKey) && coyoteTimeCounter > 0)
        {
            Jump();
        }
    }

    /// <summary>
    /// ������Ծ�������޸ĸ���Ĵ�ֱ�ٶ���ʵ����Ծ��
    /// </summary>
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        coyoteTimeCounter = 0;
        verticalMovementDirection = 1; // ��¼Ϊ�����ƶ�
        jumpTimer = jumpDuration; // ������Ծ��ʱ��
    }


    /// <summary>
    /// ���´�ֱ�ƶ����򣬼����0.1���ڵ��˶�״̬
    /// </summary>
    private void UpdateVerticalMovementDirection()
    {
        // �����Ծ��ʱ���ѹ��ڣ����÷���
        if (jumpTimer <= 0)
        {
            verticalMovementDirection = 0;
            return; // �˳����������
        }

        // ������Ծ��ʱ��
        jumpTimer -= Time.deltaTime;

        // ���ݵ�ǰ�Ĵ�ֱ�ٶ����÷���
        if (rb.velocity.y > 0.1f)
            verticalMovementDirection = 1; // �����ƶ�
        else if (rb.velocity.y < -0.1f)
            verticalMovementDirection = -1; // �����ƶ�

        // ��ѡ: ����������������ǰ�Ĵ�ֱ�ƶ����򵽵�����־��
        //Debug.Log("Vertical Movement Direction: " + verticalMovementDirection);
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
