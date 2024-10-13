using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ҳ����Ϊ���࣬��������ٶȡ�����ʱ���Լ��������߼���
/// </summary>
public class PlayerDash_Temp : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 12f; // ����ٶ�
    [SerializeField] private float dashDuration = 0.2f; // ��̳���ʱ��
    [SerializeField] private float dashUpwardForce = 5f; // б���ϵĳ����
    [SerializeField] private LayerMask groundLayer; // ����㣬���ڼ������Ƿ��ڵ�����
    [SerializeField] private Transform groundCheck; // ���ڼ������Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // ���߼��ľ���

    [SerializeField] private Rigidbody2D rb; // ��Ҹ������
    [SerializeField] private bool isGrounded; // ����Ƿ��ڵ�����
    [SerializeField] private bool canDash = true; // �Ƿ���Գ��
    [SerializeField] private bool isDashing = false; // �Ƿ����ڳ��
    [SerializeField] private float dashTime; // ��ǰ���ʣ��ʱ��
    private float lastInputDirection = 0f; // ��������뷽��
    private float inputDirectionCheckTime = 0.1f; // ���ڼ��������뷽���ʱ��
    private float inputDirectionTimer; // ���뷽���ʱ��
    private bool applyUpwardForce = false; // �Ƿ�Ӧ��б���ϵ���

    /// <summary>
    /// ��ʼ����ҵĸ��������
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ÿ֡���³���߼������������⡢���뷽���¼�ͳ�����봦��
    /// </summary>
    void Update()
    {
        CheckGroundStatus(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer); // �������Ƿ��ڵ�����
        RecordInputDirection(Input.GetAxisRaw("Horizontal")); // ��¼��ҵ����뷽��
        HandleDashInput(KeyCode.LeftShift); // ����������
    }

    /// <summary>
    /// ����������д������߼���
    /// </summary>
    void FixedUpdate()
    {
        if (isDashing)
        {
            PerformDash(lastInputDirection, dashSpeed, dashUpwardForce); // ִ�г��
        }
    }

    /// <summary>
    /// �������Ƿ��ڵ����ϣ������ó��״̬��
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
            canDash = true;
        }
    }

    /// <summary>
    /// ��¼��ҵ����뷽��
    /// </summary>
    /// <param name="horizontalInput">ˮƽ����ֵ</param>
    private void RecordInputDirection(float horizontalInput)
    {
        if (horizontalInput != 0)
        {
            lastInputDirection = horizontalInput;
            inputDirectionTimer = inputDirectionCheckTime;
        }
        else if (inputDirectionTimer > 0)
        {
            inputDirectionTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// ���������롣
    /// </summary>
    /// <param name="dashKey">��̰���</param>
    private void HandleDashInput(KeyCode dashKey)
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartDash();
        }
    }

    /// <summary>
    /// ����������д������߼���
    /// </summary>
    /// <param name="direction">��̷���</param>
    /// <param name="speed">����ٶ�</param>
    /// <param name="upwardForce">б���ϵĳ����</param>
    private void PerformDash(float direction, float speed, float upwardForce)
    {
        float verticalVelocity = applyUpwardForce ? upwardForce : rb.velocity.y;
        rb.velocity = new Vector2(direction * speed, verticalVelocity);

        dashTime -= Time.fixedDeltaTime;
        if (dashTime <= 0)
        {
            EndDash();
        }
    }

    /// <summary>
    /// ������̣����ó��״̬��ʱ�䡣
    /// </summary>
    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        canDash = false; // ��̺����ٴγ�̣�ֱ�����

        // ֻ���ڿ���ʱ��Ӧ��б���ϵ���
        applyUpwardForce = !isGrounded;
    }

    /// <summary>
    /// ������̣����ó��״̬��
    /// </summary>
    void EndDash()
    {
        isDashing = false;
        applyUpwardForce = false;
    }

    /// <summary>
    /// ���Ƽ�����ĵ������������ڿ��ӻ���ⷶΧ��
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
