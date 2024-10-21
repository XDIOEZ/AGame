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
        direction = transform.right; // 假设射线初始方向是向右
        distanceTraveled = 0f;
    }

    void Update()
    {
        // 更新射线的位置
        //transform.position += (Vector3)direction * speed * Time.deltaTime;
        distanceTraveled += speed * Time.deltaTime;
        Debug.Log(distanceTraveled);
        // 如果射线的距离超过了最大距离，则销毁它
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }

        // 检测射线是否与玩家碰撞


        // 更新射线的长度和颜色
        float t = Mathf.Clamp01(distanceTraveled / maxDistance);
        // spriteRenderer.color = Color.Lerp(startColor, endColor, t);
        transform.localScale = new Vector3(t, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("001");
            // 对玩家造成伤害
            // hit.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
           // Destroy(collision);
        }
    }
}