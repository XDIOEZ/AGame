using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystalreflection : MonoBehaviour
{
    public float bounceForce = 10f; // 反弹力的大小

    private void OnTriggerStay2D(Collider2D other)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否是主角
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log("反弹");
                // 在相反方向上应用反弹力
                Vector2 velocity = rb.velocity;
                if (velocity.x > 0 && velocity.x >= Mathf.Abs(velocity.y))
                {
                    // 如果主角向右移动且水平速度大于垂直速度，则向左反弹
                    rb.AddForce(-transform.right * bounceForce, ForceMode2D.Impulse);
                }
                else if (velocity.x < 0 && velocity.x <= Mathf.Abs(velocity.y))
                {
                    // 如果主角向左移动且水平速度小于垂直速度，则向右反弹
                    rb.AddForce(transform.right * bounceForce, ForceMode2D.Impulse);
                }
                else if (velocity.y > 0 && velocity.y >= Mathf.Abs(velocity.x))
                {
                    // 如果主角向上移动且垂直速度大于水平速度，则向下反弹
                    rb.AddForce(-transform.up * bounceForce, ForceMode2D.Impulse);
                }
                else if (velocity.y < 0 && velocity.y <= Mathf.Abs(velocity.x))
                {
                    // 如果主角向下移动且垂直速度小于水平速度，则向上反弹
                    rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
