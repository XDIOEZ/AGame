using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : SingletonAutoMono<NpcController>
{
    private Animator npcAnimator;

    public Transform[] patrolPoints; // Ѳ�ߵ�����
    public float waitTime = 2f; // ÿ��Ѳ�ߵ�ͣ����ʱ��
    private int currentPointIndex = 0; // ��ǰѲ�ߵ������
    private Rigidbody2D rb; // 2D�������
    public float jumpForce = 10f; // ��Ծ��
    private bool isWaiting = true; // �Ƿ��ڵȴ�
    private bool isJumping = false; // �Ƿ�����Ծ
    private SpriteRenderer spriteRenderer; // SpriteRenderer���

    // Start is called before the first frame update
    void Start()
    {
        npcAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (patrolPoints.Length > 0)
        {
            MoveToNextPoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting && !isJumping)
        {
            Vector2 direction = patrolPoints[currentPointIndex].position - transform.position;
            rb.velocity = direction.normalized * 2.5f; // �����ƶ��ٶ�

            float distance = Vector2.Distance(patrolPoints[currentPointIndex].position, transform.position);
            if (distance<0.5)
            {
                MoveToNextPoint();
            }
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }

        }
    }



    void MoveToNextPoint()
    {
        if (currentPointIndex < patrolPoints.Length - 1)
        {
            currentPointIndex++;

        }
        else
        {
            currentPointIndex = 0; // ѭ���ص���һ����
        }

        StartCoroutine(WaitAtPoint());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // ��������ϰ���
        {
            Jump(); // ��Ծ
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // ������ϵ���
            isJumping = true;
            Invoke("EndJump", 0.5f); // 0.5��������Ծ
        }
    }

    void EndJump()
    {
        isJumping = false;
    }

    IEnumerator WaitAtPoint()
    {
        float Waittimer = Random.Range(waitTime/2, waitTime*2);
        isWaiting = true;
        rb.velocity = Vector2.zero; // ֹͣ�ƶ�
        yield return new WaitForSeconds(Waittimer); // �ȴ�һ��ʱ��
        isWaiting = false;
        yield return new WaitForSeconds(Waittimer);
        MoveToNextPoint(); // �ƶ�����һ����
    }
}