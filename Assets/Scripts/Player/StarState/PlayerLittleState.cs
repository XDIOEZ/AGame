using UnityEngine;

/// <summary>
/// PlayerLittleState��������������״̬����л�������״̬��С��״̬��
/// <para>ʹ��˵����</para>
/// <para>1. ���˽ű����ص������Ϸ�����ϡ�</para>
/// <para>2. ��Unity�༭���У�ȷ��������״̬��SpriteRenderer��Collider��
///      С��״̬��SpriteRenderer��Collider��ͨ��SerializeField���Խ��г�ʼ����</para>
/// <para>3. ����ChangeToLittle()�������л���С��״̬������ChangeToBig()�������л��ش���״̬��</para>
/// <para>4. ȷ�����ʵ���ʱ�����Update()�������Լ�����״̬���л���</para>
/// <para>5. ʹ��SwitchPlayerState()���������������Զ��л�״̬��</para>
/// </summary>
public class PlayerLittleState : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer manSpriteRenderer; // ����״̬�� SpriteRenderer

    [SerializeField]
    private BoxCollider2D manCollider; // ����״̬�� CapsuleCollider2D

    [SerializeField]
    private bool isLittle = false; // ���ٵ�ǰ״̬��Ĭ��Ϊ����״̬

    [SerializeField]
    private float initialGravityScale; // ��ʼ��������ֵ

    [SerializeField]
    private float initialDrag; // ��ʼĦ����ֵ

    [Header("������")]
    public Vector2 offset; // ���ֵ
    public float detectionRange; // ��ⷶΧ
    public LayerMask detectionObjects; // ������

    [Header("״̬")]
    public bool isContact; // �Ӵ�
    public bool canTransformToLightBall = true; // �����ɹ�������
    public bool isLightBallState = false; // ����̬

    public void Check()
    {
        isContact = Physics2D.OverlapCircle(
            (Vector2)transform.position + offset,
            detectionRange,
            detectionObjects
        );
    }

    void Start()
    {
        // ��¼��ʼ���������ź�Ħ����ֵ
        var rb2D = GetComponentInParent<Rigidbody2D>();
        initialGravityScale = rb2D.gravityScale;
        initialDrag = rb2D.drag;

        EventCenter.GetInstance().AddEventListener("����״̬�л�", SwitchPlayerState);

    }

    // ����л���С��״̬
    public void ChangeToLittle()
    {
        if (!isLittle) // ֻ���ڵ�ǰ����С��״̬ʱ���л�
        {
            // ��ȡ Rigidbody2D ���
            Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();

            // ���� Rigidbody2D Ϊ��ֹ״̬
            rb.velocity = Vector2.zero; // ���ٶ�����Ϊ��
            //rb.isKinematic = true; // ����Ϊ��̬���壬ʹ�䲻������Ӱ��

            manSpriteRenderer.enabled = false; // �رմ���״̬����Ⱦ
            manCollider.enabled = false; // �رմ���״̬����ײ��
            // ����С��״̬�����
            GetComponent<SpriteRenderer>().enabled = true; // ����С��״̬��SpriteRenderer
            GetComponent<CapsuleCollider2D>().enabled = true; // ����С��״̬��Collider
            GetComponentInParent<PlayerMove>().isLockingMove = true; // ���ô���״̬���ƶ����

            GetComponentInParent<PlayerMove>().isJumping = false; // ���ô���״̬���ƶ����
            GetComponentInParent<PlayerMove>().isDashing = false; // ���ô���״̬���ƶ����
            GetComponentInParent<PlayerMove>().isMoving = false; // ���ô���״̬���ƶ����
            GetComponentInParent<PlayerMove>().moveDirection = Vector2.zero; // ���ô���״̬���ƶ����

            GetComponent<LittleStateMovement>().enabled = true; // ����С��״̬���ƶ����
            isLittle = true; // ����״̬ΪС��
            Debug.Log("�л���С��״̬"); // ������Ϣ
            ChangeToLittleAboutGravity(); // ���ø����������

            isLightBallState = true;
        }
    }
    // ����л�Ϊ����״̬
    public void ChangeToBig()
    {
        if (isLittle) // ֻ���ڵ�ǰ��С��״̬ʱ���л�
        {
            manSpriteRenderer.enabled = true; // �������ô���״̬����Ⱦ
            manCollider.enabled = true; // �������ô���״̬����ײ��
            // �ر�С��״̬�����
            GetComponent<SpriteRenderer>().enabled = false; // �ر�С��״̬��SpriteRenderer
            GetComponent<CapsuleCollider2D>().enabled = false; // �ر�С��״̬��Collider
            GetComponentInParent<PlayerMove>().isLockingMove = false; // ���ô���״̬���ƶ����
            GetComponent<LittleStateMovement>().enabled = false; // �ر�С��״̬���ƶ����
            isLittle = false; // ����״̬Ϊ����
            Debug.Log("�л�������״̬"); // ������Ϣ
            ChangeToBigAboutGravity(); // �ָ������������

            isLightBallState = false;
        }
    }

    void Update()
    {
        Check();
        SwitchPlayerState(); // �л�״
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("�л�״̬");
            SwitchPlayerState();
        }
    }

    public void ChangeToBigAboutGravity()
    {
        // �ָ��������������Ħ����
        Rigidbody2D rb2D = GetComponentInParent<Rigidbody2D>();
        Debug.Log("�ָ������������");
        rb2D.gravityScale = initialGravityScale;
        rb2D.drag = initialDrag;
    }

    public void ChangeToLittleAboutGravity()
    {
        // ���ø����������
        var rb2D = GetComponentInParent<Rigidbody2D>(); // ��ȡ������� Rigidbody2D ���
        rb2D.gravityScale = 0; // ���ø����������Ϊ0
        rb2D.drag = 0; // ����Ħ����Ϊ0��ȷ���ٶȺ㶨
    }



    /// <summary>
    /// �л����״̬����
    /// </summary>
    public void SwitchPlayerState()
    {
        if (isLittle && isContact)
        {
            ChangeToBig(); // ��ǰ��С�ˣ��л�������
        }
        else if (canTransformToLightBall)
        {
            ChangeToLittle(); // ��ǰ�Ǵ��ˣ��л���С��
            canTransformToLightBall = false;
        }

        if (isLittle && GetComponentInParent<PlayerMove>().dashCount <= 0)
        {
            GetComponentInParent<PlayerMove>().dashCount++;
        }
        if (isLittle && GetComponentInParent<PlayerMove>().jumpCount <= 0)
        {
            GetComponentInParent<PlayerMove>().jumpCount++;
        }
    }
}
