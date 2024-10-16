using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ҳ����Ϊ���࣬��������ٶȡ�����ʱ���Լ��������߼���
/// </summary>
public class PlayerDash_Temp : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 12f; // ����ٶ�
    [SerializeField] private float dashSpeedFactor = 1.0f; // ����ٶȱ�������
    [SerializeField] private float dashDuration = 0.2f; // ��̳���ʱ��
    [SerializeField] private float dashUpwardForce = 5f; // б���ϵĳ����
    [SerializeField] private float horizontalDashFactor = 1.0f; // ˮƽ���ϵ��
    [SerializeField] private float verticalDashFactor = 1.0f; // ��ֱ���ϵ��
    [SerializeField] private LayerMask Ground; // ����㣬���ڼ������Ƿ��ڵ�����
    [SerializeField] private Transform groundCheck; // ���ڼ������Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // ���߼��ľ���

    [SerializeField] private float ˮƽ�޿��Ƴ��ϵ�� = 0.5f; // �����ȴʱ��

    [SerializeField] private Rigidbody2D rb; // ��Ҹ������
    [SerializeField] private bool isGrounded; // ����Ƿ��ڵ�����
    [SerializeField] private bool canDash = true; // �Ƿ���Գ��
    [SerializeField] private bool isDashing = false; // �Ƿ����ڳ��
    [SerializeField] private float dashTime; // ��ǰ���ʣ��ʱ��
    [SerializeField] private float lastInputDirection; // ��������뷽��
    [SerializeField] private float inputDirectionCheckTime = 0.1f; // ���ڼ��������뷽���ʱ��
    [SerializeField] private float inputDirectionTimer; // ���뷽���ʱ��
    [SerializeField] private bool applyUpwardForce; // �Ƿ�Ӧ��б���ϵ���


    [SerializeField] private int remainingDashes = 1; // ʣ���̴���

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGroundStatus();
        RecordInputDirection(Input.GetAxisRaw("Horizontal"));
        HandleDashInput(KeyCode.LeftShift);

        if (isGrounded && isDashing == false)
        {
            remainingDashes = 1; // ���ó�̴���
            canDash = true;
            Debug.Log("Grounded!");
        }
        if(remainingDashes <= 0)
        {
            canDash = false; // ��̴�������
            Debug.Log("No more dashes left!");
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            PerformDash(lastInputDirection, dashSpeed * dashSpeedFactor, dashUpwardForce);
        }
    }

    private void CheckGroundStatus()
    {
        // �������Ƿ��ڵ����ϣ�����Ӱ���̵�����
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, Ground);
    }

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

    private void HandleDashInput(KeyCode dashKey)
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartDash();
        }
    }

    private void PerformDash(float direction, float speed, float upwardForce)
    {
        float horizontalForce = 0f;
        float verticalForce = 0f;

        // ��� "W" ���Ƿ���
        if (Input.GetKey(KeyCode.W))
        {
            // ������ϵ���
            verticalForce = upwardForce * verticalDashFactor;

            // ���ͬʱ���� "A" �� "D"�������������Ļ���������ˮƽ��
            if (Input.GetKey(KeyCode.A))
            {
                horizontalForce = -horizontalDashFactor * speed; // ��ˮƽ���
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalForce = horizontalDashFactor * speed; // ��ˮƽ���
            }
        }
        // ��� "S" ���Ƿ���
        else if (Input.GetKey(KeyCode.S))
        {
            // ������ "S" �������ô�ֱ��Ϊ����
            verticalForce = -upwardForce * verticalDashFactor;
            if (Input.GetKey(KeyCode.A))
            {
                horizontalForce = -horizontalDashFactor * speed; // ��ˮƽ���
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalForce = horizontalDashFactor * speed; // ��ˮƽ���
            }
        }
        // ����Ƿ��ڿ�����û�а������������
        else if (isDashing)
        {
            // �ڿ��е������³�̰�ťʱ������ lastInputDirection ִ��ˮƽ���
            horizontalForce = lastInputDirection * speed * horizontalDashFactor;

            // ������� "A" �� "D" Ҳ�ܽ���ˮƽ���
            if (Input.GetKey(KeyCode.A))
            {
                horizontalForce = -horizontalDashFactor * speed; // ��ˮƽ���
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalForce = horizontalDashFactor * speed; // ��ˮƽ���
            }
        }
        else
        {
            // Ĭ������²���Ӵ�ֱ��
            verticalForce = applyUpwardForce ? upwardForce * verticalDashFactor : 0f;
        }

        // �γɳ�̵���
        Vector2 dashForce = new Vector2(horizontalForce, verticalForce);
        // Ӧ�ó����
        rb.AddForce(dashForce, ForceMode2D.Impulse);

        // ���³�̵�ʣ��ʱ��
        dashTime -= Time.fixedDeltaTime;
        // ������ʱ����������ý�����̷���
        if (dashTime <= 0)
        {
            EndDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        remainingDashes--; // ��̺���ٳ�̴���
        Debug.Log("Dashing!");
        // ֻ���ڿ���ʱ��Ӧ��б���ϵ���
        applyUpwardForce = !isGrounded;
    }

    private void EndDash()
    {
        isDashing = false;
        applyUpwardForce = false;

        // �����أ����ó�̴���
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
