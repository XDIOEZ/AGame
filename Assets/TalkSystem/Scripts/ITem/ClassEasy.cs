using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassEasy : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否是子弹
        if (collision.gameObject.CompareTag("Player"))
        {
            // 禁用子弹的碰撞器
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 检查是否是子弹
        if (collision.gameObject.CompareTag("Player"))
        {
            // 重新启用子弹的碰撞器
            collision.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
}
