using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStar : MonoBehaviour
{
    public float speed = 5f;
    public float angle = 45f; // 角度，单位是度
    public float rotationSpeed = 100f; // 旋转速度

    private GameObject blackStars;
    private GameObject effect;


    void Start()
    {
        blackStars = transform.Find("BlackStar").gameObject;
        effect= transform.Find("ParticleWakeline").gameObject;
        // 将角度转换为弧度
        float radians = angle * Mathf.Deg2Rad;

        // 计算水平和垂直速度分量
        float horizontalSpeed = speed * Mathf.Cos(radians);
        float verticalSpeed = speed * Mathf.Sin(radians);

        // 设置物体的初始速度
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    void Update()
    {
        // 根据旋转速度更新物体的旋转角度
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
            // 否则，停止物体的移动
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rotationSpeed = 0;
            Destroy(effect,0.05f);
        }
    }
}
