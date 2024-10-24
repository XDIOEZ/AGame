using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystalreflection : MonoBehaviour
{
    public float pushForce = 10f; // ������С
    private float Timer;
    public float moveDuration = 0.5f;
    private bool isMoving = false;
    private bool dashKey;

    private PlayerMove Player;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EventCenter.Instance.AddEventListener<object>("IsDashValue", (object obj) =>
        {
            Player = obj as PlayerMove; // �� obj ת��Ϊ PlayerMove
            if (Player != null)
            {
                dashKey = Player.isDashing;
            }
        });
    }


    private void Update()
    {
        Timer -= Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dashKey)
        {
            // ����Ƿ�������
            if (collision.collider.CompareTag("Player"))
            {
                // ��ȡ��ײ�ĽӴ���
                ContactPoint2D contact = collision.contacts[0];

                // ������������
                Vector2 pushDirection = contact.point - (Vector2)transform.position;

                // �������������������ƶ�����
                if (Mathf.Abs(pushDirection.x) > Mathf.Abs(pushDirection.y))
                {
                    // �������������ˮƽ������ֻӦ��ˮƽ���������
                    pushDirection.y = 0f;
                }
                else
                {
                    // ������������ڴ�ֱ������ֻӦ�ô�ֱ���������
                    pushDirection.x = 0f;
                }

                // Ӧ����������ʼЭ��
                rb.AddForce(pushDirection.normalized * pushForce, ForceMode2D.Impulse);
                StartCoroutine(MoveForDuration(moveDuration));
            }
        }
    }

    private IEnumerator MoveForDuration(float duration)
    {
        isMoving = true;
        rb.isKinematic = false; // �������������Ӱ��

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // ֹͣ�ƶ�
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // �������������Ӱ��

        isMoving = false;
    }
}
