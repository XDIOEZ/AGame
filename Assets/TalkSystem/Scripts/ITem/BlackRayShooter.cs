using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRayShooter : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    public float damage;
    public Color startColor = Color.clear;
    public Color endColor = Color.black;

    private SpriteRenderer spriteRenderer;
    private Vector2 direction;
    private float distanceTraveled;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = transform.right; // �������߳�ʼ����������
        distanceTraveled = 0f;
    }

    void Update()
    {
        // �������ߵ�λ��
        //transform.position += (Vector3)direction * speed * Time.deltaTime;
        distanceTraveled += speed * Time.deltaTime;
        Debug.Log(distanceTraveled);
        // ������ߵľ��볬���������룬��������
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }

        // ��������Ƿ��������ײ


        // �������ߵĳ��Ⱥ���ɫ
        float t = Mathf.Clamp01(distanceTraveled / maxDistance);
        // spriteRenderer.color = Color.Lerp(startColor, endColor, t);
        transform.localScale = new Vector3(t, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("001");
            // ���������˺�
            // hit.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
           // Destroy(collision);
        }
    }
}