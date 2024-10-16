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
    /// ������ҵ��ƶ��ٶȣ�ʹ�����Բ�ֵƽ���ٶȱ仯��
    /// </summary>
    /// <param name="targetSpeed">Ŀ���ٶ�</param>
    /// <param name="damping">����ֵ</param>
    private void UpdatePlayerMovement(float targetSpeed, float damping)
    {
        /// <summary>
        /// ʹ�����Բ�ֵ��Lerp����ƽ���ٶȵı仯������ͻȻ���ٻ���١�
        /// </summary>
        float newSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, 1f - damping);

        // ������ҵ� Rigidbody2D ���ٶ�
        rb.velocity = new Vector2(newSpeed, rb.velocity.y);
        if (movement.x != 0 && Mathf.Sign(movement.x) != Mathf.Sign(transform.localScale.x))
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }


    
}
