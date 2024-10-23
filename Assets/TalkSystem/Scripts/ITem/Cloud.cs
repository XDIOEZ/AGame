using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float contactTime = 1f; // 触碰时间
    public float disappearTime = 3f; // 消失时间
    public float fadeSpeed = 1f; // 淡化速度

    private bool isContacted = false;
    private float contactTimer = 0f;

    private void Update()
    {
        if (isContacted)
        {
            Debug.Log(contactTimer);
            contactTimer += Time.deltaTime;
            if (contactTimer >= contactTime)
            {
                StartCoroutine(FadeAndDestroy());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否是主角
        if (collision.collider.CompareTag("Player"))
        {
            isContacted = true;
            contactTimer = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isContacted = false;
        }
    }

    private IEnumerator FadeAndDestroy()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float timer = 0f;
        while (timer < disappearTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, endColor.a, timer / disappearTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
