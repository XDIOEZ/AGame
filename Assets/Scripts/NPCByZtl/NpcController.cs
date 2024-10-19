using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : SingletonAutoMono<NpcController>
{
    private Animator npcAnimator;

    public Transform[] patrolPoints; // 巡逻点数组
    public float waitTime = 2f; // 每个巡逻点停留的时间
    private int currentPointIndex = 0; // 当前巡逻点的索引
    private Rigidbody2D rb; // 2D刚体组件
    public float jumpForce = 10f; // 跳跃力
    private bool isWaiting = false; // 是否在等待
    private bool isJumping = false; // 是否在跳跃
    private SpriteRenderer spriteRenderer; // SpriteRenderer组件

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
            rb.velocity = direction.normalized * 2.5f; // 设置移动速度
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
            currentPointIndex = 0; // 循环回到第一个点
        }

        StartCoroutine(WaitAtPoint());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // 如果碰到障碍物
        {
            Jump(); // 跳跃
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // 添加向上的力
            isJumping = true;
            Invoke("EndJump", 0.5f); // 0.5秒后结束跳跃
        }
    }

    void EndJump()
    {
        isJumping = false;
    }

    IEnumerator WaitAtPoint()
    {
        isWaiting = false;
        // 根据移动方向设置镜像反转
        rb.velocity = Vector2.zero; // 停止移动
        yield return new WaitForSeconds(waitTime); // 等待一段时间
        if (spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        MoveToNextPoint(); // 移动到下一个点
    }
}