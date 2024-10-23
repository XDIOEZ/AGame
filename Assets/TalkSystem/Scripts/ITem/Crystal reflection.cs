using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystalreflection : MonoBehaviour
{
    public float bounceForce = 10f; // �������Ĵ�С

    private void OnTriggerStay2D(Collider2D other)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����Ƿ�������
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log("����");
                // ���෴������Ӧ�÷�����
                Vector2 velocity = rb.velocity;
                if (velocity.x > 0 && velocity.x >= Mathf.Abs(velocity.y))
                {
                    // ������������ƶ���ˮƽ�ٶȴ��ڴ�ֱ�ٶȣ������󷴵�
                    rb.AddForce(-transform.right * bounceForce, ForceMode2D.Impulse);
                }
                else if (velocity.x < 0 && velocity.x <= Mathf.Abs(velocity.y))
                {
                    // ������������ƶ���ˮƽ�ٶ�С�ڴ�ֱ�ٶȣ������ҷ���
                    rb.AddForce(transform.right * bounceForce, ForceMode2D.Impulse);
                }
                else if (velocity.y > 0 && velocity.y >= Mathf.Abs(velocity.x))
                {
                    // ������������ƶ��Ҵ�ֱ�ٶȴ���ˮƽ�ٶȣ������·���
                    rb.AddForce(-transform.up * bounceForce, ForceMode2D.Impulse);
                }
                else if (velocity.y < 0 && velocity.y <= Mathf.Abs(velocity.x))
                {
                    // ������������ƶ��Ҵ�ֱ�ٶ�С��ˮƽ�ٶȣ������Ϸ���
                    rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
