using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("�ٶ�����")]
    public float moveSpeed = 5f;         // �ƶ��ٶ�
    public float dashSpeed = 10f;        // ����ٶ�
    public float jumpSpeed = 7f;         // ��Ծ�ٶ�
    public float XDashSpeedFactor = 1f;  // ˮƽ�����ٶ�����
    public float YDashSpeedFactor = 1f;  // ��ֱ�����ٶ�����
    public float VerticalSpeedCorrectionValue = 1f; // ��ֱ�ٶ�����ֵ

    [Header("��������")]
    public Vector2 moveDirection;
    public Vector2 dashDirection;
    public int PlayerDirection; // ��ҷ���
    private int lastPlayerDirection; // ��һ�εķ���

    [Header("ʱ������")]
    public float dashDuration = 0.2f;     // ��̳���ʱ��
    public float dashTimer;               // ��̼�ʱ��

    [Header("״̬����")]
    public bool isDashing = false;       // ���״̬
    public int dashCount = 1;             // ��̴�����Ĭ��Ϊ1�γ��
    public bool isJumping = false;        // ��Ծ״̬
    public int jumpCount = 1;             // ��Ծ������Ĭ��Ϊ1����Ծ

    public bool isGrounded;               // ����Ƿ��ڵ�����
    public float jumpTimer;               // ��Ծ��ʱ��
    public bool isMoving;                 // �Ƿ������ƶ�

    [Header("�������")]
    public LayerMask groundLayer = 1 << 8; // ����㣬�������߼��
    public Transform groundCheckPoint;     // ������㣬ͨ��������ҽ��µ�λ��
    public float groundCheckRadius = 1.5f; // ���߼��ľ��룬����΢��
    public Rigidbody2D rb2d;
    public float jumpGraceTime = 0.1f;     // ��Ծ�󲻽��е������ʱ��

    [Header("��������")]
    public bool isLockingMove; // �Ƿ������ƶ�
    public bool isLockingDash; // �Ƿ��������
    public bool isLockingJump; // �Ƿ�������Ծ

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  // ��ȡ Rigidbody2D ���
        groundCheckPoint = transform; // Ĭ�ϵ������Ϊ��ҽ���λ��
        lastPlayerDirection = PlayerDirection; // ��ʼ����һ�εķ���
    }

    private void Update()
    {
        // �ƶ�
        if (!isDashing && !isLockingMove)
        {
            rb2d.velocity = new Vector2(moveDirection.x, rb2d.velocity.y);
            Move();
        }

        // ��Ծ
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0 && !isDashing && !isLockingJump)
        {
            Jump();
        }

        // ���
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCount > 0 && !isDashing && !isLockingDash)
        {
            Dash();
        }

        // ���״̬����
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }

        // ������Ծ��ʱ��
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // ���е����⣨ֻ�е����ڳ��״̬�£�
        if (!isDashing)
        {
            CheckIfGrounded();
        }
    }

    void Move()
    {
        // ��ȡ���ˮƽ����
        float moveInput = Input.GetAxis("Horizontal");

        // Ŀ���ٶ� = ˮƽ���� * �ƶ��ٶ�
        moveDirection = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);

        // ������ҷ���
        int lastPlayerDirection = PlayerDirection; // ��¼��һ�εķ���

        if (moveInput != 0)
        {
            PlayerDirection = moveInput > 0 ? 1 : -1; // 1��ʾ�ң�-1��ʾ��
            isMoving = true; // ��������ƶ�

            // ��鷽���Ƿ�ı�
            if (PlayerDirection != lastPlayerDirection)
            {
                EventCenter.Instance.EventTrigger<object>("PlayerDirectionChanged", this);
                Debug.Log("PlayerDirectionChanged");

                lastPlayerDirection = PlayerDirection; // ������һ�εķ���
            }

            // ������ҵķ�������ת��ɫ��Transform
            Vector3 localScale = transform.localScale;
            localScale.x = PlayerDirection > 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
        else
        {
            isMoving = false; // ���ֹͣ�ƶ�
        }
    }


    void Jump()
    {
        // ��Ծʱ�������ϵ��ٶ�
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        jumpCount--; // ������Ծ����
        jumpTimer = jumpGraceTime; // ������Ծ��ʱ��
        isJumping = true; // ������Ծ״̬Ϊ��
    }

    void Dash()
    {
        // ��ȡ��ҵ����벢��һ��
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        dashDirection = new Vector2(inputX * XDashSpeedFactor, inputY * YDashSpeedFactor).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(PlayerDirection, VerticalSpeedCorrectionValue).normalized; // Ĭ����ǰ����
        }

        // ���ó���ٶ�
        rb2d.velocity = new Vector2(dashDirection.x * dashSpeed, dashDirection.y * dashSpeed);

        // ���״̬
        isDashing = true;

        dashCount--; // ÿ�γ�̼���һ�γ�̴���
        dashTimer = dashDuration;
    }

    void EndDash()
    {
        isDashing = false;
        // ��̽������״̬�ָ�
    }

    // ���߼������Ƿ��ڵ�����
    void CheckIfGrounded()
    {
        if (jumpTimer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckRadius, groundLayer);
            isGrounded = hit.collider != null;

            if (isGrounded)
            {
                jumpCount = 1; // �ָ���Ծ����
                dashCount = 1; // �ָ���̴���
                isJumping = false; // ������Ծ״̬
            }
        }
        Debug.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * groundCheckRadius, Color.red);
    }
}
