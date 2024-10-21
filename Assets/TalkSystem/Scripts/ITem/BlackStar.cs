using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStar : MonoBehaviour
{
    public float speed = 5f;
    public float angle = 45f; // �Ƕȣ���λ�Ƕ�
    public float rotationSpeed = 100f; // ��ת�ٶ�

    private GameObject blackStars;
    private GameObject effect;


    void Start()
    {
        blackStars = transform.Find("BlackStar").gameObject;
        effect= transform.Find("ParticleWakeline").gameObject;
        // ���Ƕ�ת��Ϊ����
        float radians = angle * Mathf.Deg2Rad;

        // ����ˮƽ�ʹ�ֱ�ٶȷ���
        float horizontalSpeed = speed * Mathf.Cos(radians);
        float verticalSpeed = speed * Mathf.Sin(radians);

        // ��������ĳ�ʼ�ٶ�
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    void Update()
    {
        // ������ת�ٶȸ����������ת�Ƕ�
        blackStars.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            // ����ֹͣ������ƶ�
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rotationSpeed = 0;
            Destroy(effect,0.05f);
        }
    }
}
