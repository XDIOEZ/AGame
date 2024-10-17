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

    // ��¼��ֱ������ƶ�
    [SerializeField]
    private int verticalMovementDirection = 0; // 1��ʾ���ϣ�-1��ʾ���£�0��ʾ��ֹ

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
        CheckGroundStatus(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        HandleJumpInput(KeyCode.Space);
        
        // ���´�ֱ�ƶ������״̬
        UpdateVerticalMovementDirection();
    }

    /// <summary>
    /// �������Ƿ��ڵ����ϣ������� Coyote Time ��ʱ����
    /// </summary>
    /// <param name="position">�����ʼλ��</param>
    /// <param name="direction">��ⷽ��</param>
    /// <param name="distance">������</param>
    /// <param name="layer">����</param>
    private void CheckGroundStatus(Vector3 position, Vector2 direction, float distance, LayerMask layer)
    {
        isGrounded = Physics2D.Raycast(position, direction, distance, layer);

        if (isGrounded)
        {
            coyoteTimeCounter = jumpCoyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    /// <summary>
    /// ������Ծ���롣
    /// </summary>
    /// <param name="jumpKey">��Ծ����</param>
    private void HandleJumpInput(KeyCode jumpKey)
    {
        // ����Ƿ����ڳ�̣������򲻽�����Ծ
        if (playerDash != null && playerDash.isDashing)
        {
            return; // ������ڳ�̣���������Ծ����
        }

        if (Input.GetKeyDown(jumpKey) && coyoteTimeCounter > 0)
        {
            Jump(jumpForce);
            verticalMovementDirection = 1; // ��¼Ϊ�����ƶ�
        }
    }

    /// <summary>
    /// ������Ծ�������޸ĸ���Ĵ�ֱ�ٶ���ʵ����Ծ��
    /// </summary>
    /// <param name="force">��Ծ����</param>
    private void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, force);
        coyoteTimeCounter = 0;
    }

    /// <summary>
    /// ���´�ֱ�ƶ����򣬼����0.1���ڵ��˶�״̬
    /// </summary>
    private void UpdateVerticalMovementDirection()
    {
        // ���ݵ�ǰ�Ĵ�ֱ�ٶ����÷���
        if (rb.velocity.y > 0.1f)
        {
            verticalMovementDirection = 1; // �����ƶ�
        }
        else if (rb.velocity.y < -0.1f)
        {
            verticalMovementDirection = -1; // �����ƶ�
        }
        else
        {
            verticalMovementDirection = 0; // ��ֹ
        }
        
        // ��ѡ: ����������������ǰ�Ĵ�ֱ�ƶ����򵽵�����־��
        Debug.Log("Vertical Movement Direction: " + verticalMovementDirection);
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
