using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Light_Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float destoryTime = 2.0f;
    float destoryTimer;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EventCenter.Instance.AddEventListener<HitInfo>(
            $"{gameObject.GetInstanceID()}_OnHitMirror",
            OnHitMirror
        );
        destoryTimer = destoryTime;
    }

    private void Update()
    {
        if (destoryTimer > 0)
        {
            destoryTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Toggle"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("DarkWall"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy") && collision != null)
        {
            collision.GetComponent<EnemyHurt>().EnemyDestroy();
        }
    }

    public void Lunch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    private void OnHitMirror(HitInfo hitInfo)
    {
        if (rb != null)
        {
            // 获取光线子弹的当前速度
            Vector2 incomingDirection = rb.velocity;

            // 计算反射后的方向
            Vector2 reflectedDirection = Vector2.Reflect(
                incomingDirection,
                hitInfo.ReflectionNormal
            );

            // 反射方向近似到四向
            if (Mathf.Abs(reflectedDirection.x) < 0.1f)
            {
                reflectedDirection.x = 0;
            }
            if (Mathf.Abs(reflectedDirection.y) < 0.1f)
            {
                reflectedDirection.y = 0;
            }

            // 更新光线子弹的速度为反射方向
            rb.velocity = reflectedDirection;

            // 更新子弹朝向为反射方向
            transform.right = reflectedDirection;

            // 更新子弹位置到撞击点
            transform.position = hitInfo.CollisionPoint;
        }
    }
}
