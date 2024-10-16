using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������ƶ���Ϊ���֧࣬��ƽ����ˮƽ�ƶ�������Ч����
/// </summary>
public class PlayerMovement_Temp : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // �ƶ��ٶ�
    [SerializeField] private float acceleration = 10f; // ���ٶȣ����ڿ������Ĵ�С
    [SerializeField] private Rigidbody2D rb; // ��ɫ�� Rigidbody2D ���
    [SerializeField] private Vector2 movement; // �ƶ���������

    // ����ƽ��ֹͣ���������
    [SerializeField] private float damping = 0.1f; // ���ᣬ����ƽ��ֹͣ

    /// <summary>
    /// ��ʼ����ҵĸ��������
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ��������������ÿ֡�����ҵ�ˮƽ�ƶ����롣
    /// </summary>
    void Update()
    {
        // ��ȡˮƽ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f; // �����Ϸ����Ҫ��ֱ�ƶ�
    }

    /// <summary>
    /// ʹ�ù̶�ʱ�䲽������������£�������ҵ��ƶ��ٶȡ�
    /// </summary>
    void FixedUpdate()
    {
        UpdatePlayerMovement(movement.x * moveSpeed, damping); // ��������ƶ�
    }

    /// <summary>
    /// ������ҵ��ƶ��ٶȣ�ʹ�������ƶ���Ҷ�����ֱ�������ٶȡ�
    /// </summary>
    /// <param name="targetSpeed">Ŀ���ٶ�</param>
    /// <param name="damping">����ֵ</param>
    private void UpdatePlayerMovement(float targetSpeed, float damping)
    {
        // ���㵱ǰ�ٶ���Ŀ���ٶ�֮��Ĳ���
        float speedDifference = targetSpeed - rb.velocity.x;

        // ����������������ݲ���������
        float force = speedDifference * acceleration * (1f - damping);

        // ���˲ʱ����������ҵ��ٶ�
        rb.AddForce(new Vector2(force, 0f), ForceMode2D.Force);
    }
}
