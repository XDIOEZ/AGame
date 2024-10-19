using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������ƶ���Ϊ���֧࣬��ƽ����ˮƽ�ƶ�������Ч����
/// </summary>
public class PlayerMovement_Temp : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // �ƶ��ٶ�
    [SerializeField] private Rigidbody2D rb; // ��ɫ�� Rigidbody2D ���
    [SerializeField] private Vector2 movement; // �ƶ���������

    // ����ƽ��ֹͣ���������
    [SerializeField] private float damping = 0.1f; // ���ᣬ����ƽ��ֹͣ

    // ����
    public Vector2 lookDirection;

    /// <summary>
    /// ��ʼ����ҵĸ��������
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lookDirection = new Vector2(1, 0);
    }

    /// <summary>
    /// ��������������ÿ֡�����ҵ�ˮƽ�ƶ����롣
    /// </summary>
    void Update()
    {
        // ��ȡˮƽ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f; // �����Ϸ����Ҫ��ֱ�ƶ�

        GetDirection();
    }

    /// <summary>
    /// ʹ�ù̶�ʱ�䲽������������£�������ҵ��ƶ��ٶȡ�
    /// </summary>
    void FixedUpdate()
    {
        UpdatePlayerMovement(movement.x * moveSpeed); // ��������ƶ�
    }

    /// <summary>
    /// ������ҵ��ƶ��ٶȣ�ʹ�����ķ�ʽʵ���ƶ���
    /// </summary>
    /// <param name="targetSpeed">Ŀ���ٶ�</param>
    private void UpdatePlayerMovement(float targetSpeed)
    {
        // �����������ٶȺ͵�ǰ�ٶȵĲ���
        float speedDifference = targetSpeed - rb.velocity.x;

        // ����ʩ�ӵ������ϣ�ʹ������
        float force = speedDifference / Time.fixedDeltaTime; // ������Ҫʩ�ӵ���
        force *= (1f - damping); // Ӧ������

        // ʩ����
        rb.AddForce(new Vector2(force, 0), ForceMode2D.Force);

        // ��ת����
        if (movement.x != 0 && Mathf.Sign(movement.x) != Mathf.Sign(transform.localScale.x))
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    // ��¼�������
    public void GetDirection()
    {
        if (movement.x != 0)
        {
            lookDirection = movement;
            
        }
    }
}
