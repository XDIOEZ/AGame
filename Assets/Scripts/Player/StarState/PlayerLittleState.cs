using UnityEngine;

public class PlayerLittleState : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ManSpriteRenderer; // ����״̬�� SpriteRenderer
    [SerializeField] private CapsuleCollider2D ManCollider; // ����״̬�� CapsuleCollider2D
    [SerializeField] private bool isLittle = false; // ���ٵ�ǰ״̬��Ĭ��Ϊ����״̬

    [SerializeField] private float initialGravityScale; // ��ʼ��������ֵ
    [SerializeField] private float initialDrag; // ��ʼĦ����ֵ

    void Start()
    {
        // ��¼��ʼ���������ź�Ħ����ֵ
        var rb2D = GetComponentInParent<Rigidbody2D>();
        initialGravityScale = rb2D.gravityScale;
        initialDrag = rb2D.drag;
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

        ManSpriteRenderer.enabled = false; // �رմ���״̬����Ⱦ
        ManCollider.enabled = false; // �رմ���״̬����ײ��
        // ����С��״̬�����
        GetComponent<SpriteRenderer>().enabled = true; // ����С��״̬��SpriteRenderer
        GetComponent<CapsuleCollider2D>().enabled = true; // ����С��״̬��Collider
        GetComponentInParent<PlayerMovement_Temp>().enabled = false; // ���ô���״̬���ƶ����
        GetComponent<LittleStateMovement>().enabled = true; // ����С��״̬���ƶ����
        isLittle = true; // ����״̬ΪС��
        Debug.Log("�л���С��״̬"); // ������Ϣ
        ChangeToLittleAboutGravity(); // ���ø����������
    }
}


    // ����л�Ϊ����״̬
    public void ChangeToBig()
    {
        if (isLittle) // ֻ���ڵ�ǰ��С��״̬ʱ���л�
        {
            ManSpriteRenderer.enabled = true; // �������ô���״̬����Ⱦ
            ManCollider.enabled = true; // �������ô���״̬����ײ��
            // �ر�С��״̬�����
            GetComponent<SpriteRenderer>().enabled = false; // �ر�С��״̬��SpriteRenderer
            GetComponent<CapsuleCollider2D>().enabled = false; // �ر�С��״̬��Collider
            GetComponentInParent<PlayerMovement_Temp>().enabled = true; // ���ô���״̬���ƶ����
            GetComponent<LittleStateMovement>().enabled = false; // �ر�С��״̬���ƶ����
            isLittle = false; // ����״̬Ϊ����
            Debug.Log("�л�������״̬"); // ������Ϣ
            ChangeToBigAboutGravity(); // �ָ������������
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C)) // �л�״̬
        {
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

    public void SwitchPlayerState()
    {

        if (isLittle)
        {
            ChangeToBig(); // ��ǰ��С�ˣ��л�������
        }
        else
        {
            ChangeToLittle(); // ��ǰ�Ǵ��ˣ��л���С��
        }
    }
}
