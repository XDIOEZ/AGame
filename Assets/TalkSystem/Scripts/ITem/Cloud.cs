using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float contactTime = 1f; // ����ʱ��
    public float disappearTime = 3f; // ��ʧʱ��
    public float fadeSpeed = 1f; // �����ٶ�

    private bool isContacted = false;
    private float contactTimer = 0f;
    private bool effeckey=true;

    private ParticleSystem EffectOne;
    private ParticleSystem EffectTwo;


    private void Start()
    {
        EffectOne = GameObject.Find("CloudDieEffect01").gameObject.GetComponentInParent<ParticleSystem>();
        EffectTwo = GameObject.Find("CloudDieEffect02").gameObject.GetComponentInParent<ParticleSystem>();
    }


    private void Update()
    {
        if (isContacted)
        {
            contactTimer += Time.deltaTime;
            if (contactTimer >= contactTime)
            {
                StartCoroutine(FadeAndDestroy());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����Ƿ�������
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

            if (effeckey)
            {
                EffectOne.Play();
                EffectTwo.Play();
                effeckey=false;
            }
            yield return null;
        }

        Destroy(gameObject);
    }
}