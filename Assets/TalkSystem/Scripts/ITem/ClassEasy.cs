using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassEasy : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����Ƿ����ӵ�
        if (collision.gameObject.CompareTag("Player"))
        {
            // �����ӵ�����ײ��
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ����Ƿ����ӵ�
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���������ӵ�����ײ��
            collision.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
}
