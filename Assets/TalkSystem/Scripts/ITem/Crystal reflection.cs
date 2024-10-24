using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystalreflection : MonoBehaviour
{
    public float pushForce = 10f; // 推力大小
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
            Player = obj as PlayerMove; // 将 obj 转换为 PlayerMove
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
            // 检查是否是主角
            if (collision.collider.CompareTag("Player"))
            {
                // 获取碰撞的接触点
                ContactPoint2D contact = collision.contacts[0];

                // 计算推力方向
                Vector2 pushDirection = contact.point - (Vector2)transform.position;

                // 根据推力方向来决定移动方向
                if (Mathf.Abs(pushDirection.x) > Mathf.Abs(pushDirection.y))
                {
                    // 如果推力方向在水平方向，则只应用水平方向的推力
                    pushDirection.y = 0f;
                }
                else
                {
                    // 如果推力方向在垂直方向，则只应用垂直方向的推力
                    pushDirection.x = 0f;
                }

                // 应用推力并开始协程
                rb.AddForce(pushDirection.normalized * pushForce, ForceMode2D.Impulse);
                StartCoroutine(MoveForDuration(moveDuration));
            }
        }
    }

    private IEnumerator MoveForDuration(float duration)
    {
        isMoving = true;
        rb.isKinematic = false; // 禁用物理引擎的影响

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // 停止移动
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // 启用物理引擎的影响

        isMoving = false;
    }
}
