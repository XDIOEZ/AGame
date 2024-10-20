using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Light_Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    float destoryTime;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EventCenter.Instance.AddEventListener<Vector2>(
            $"{this.gameObject.name}_OnHitMirror",
            OnHitMirror
        );
    }

    private void Update() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DarkWall"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void Lunch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    private void OnHitMirror(Vector2 reflectionNormal)
    {
        if (rb != null)
        {
            // 获取光线子弹的当前速度
            Vector2 incomingDirection = rb.velocity;

            // 计算反射后的方向
            Vector2 reflectedDirection = Vector2.Reflect(incomingDirection, reflectionNormal);

            // 反射方向近似到四向
            if (Mathf.Abs(reflectedDirection.x) < 0.1f)
            {
                reflectedDirection.x = 0;
            }
            if (Mathf.Abs(reflectedDirection.y) < 0.1f)
            {
                reflectedDirection.y = 0;
            }

            // 更新光线子弹的速度
            rb.velocity = reflectedDirection;
        }
    }
}
