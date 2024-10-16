using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Light_Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    //private void Start()
    //{
    //    EventCenter.Instance.AddEventListener($"{this.gameObject.name}_OnHitMirror", ()=>OnHitMirror());
    //}

   

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DarkWall"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }


    public void Lunch(Vector2 direction,float force)
    {
        rb.AddForce(direction*force);
    }
    private void OnHitMirror(Vector2 reflectionNormal)
    {
        Debug.Log($"撞到镜子，法线向量:{reflectionNormal}");
        // 这里可以做一些反射相关的处理
    }

}
