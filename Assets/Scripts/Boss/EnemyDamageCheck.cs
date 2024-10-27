using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCheck : MonoBehaviour
{
    public int hp = 100; // �����ʼ����ֵΪ100
    public int maxHp = 100; // �������ֵ
    public Color healthBarColor = Color.white; // ��������ɫ

    public void EnemyDead()
    {
        hp--;
        Debug.Log("BossHp--");
    }

    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light_Bullet"))
        {
            Debug.Log("�ӵ����е���: EnemyHurt.cs: OnTriggerEnter2D: Bullet");
            Destroy(other.gameObject);
            EnemyDead();
        }
    }

    void Update()
    {
        if (hp <= 0)
        {
            EventCenter.Instance.EventTrigger("BossDead");
            Destroy(gameObject);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    void OnGUI()
    {
        // �����������Ŀ��
        float healthBarWidth = (hp / (float)maxHp) * Screen.width; // ��������ֵ������
        float healthBarHeight = 20; // �������߶�
        float healthBarX = 0; // X ���꣨�����ң�
        float healthBarY = Screen.height - healthBarHeight; // Y ���꣨�ײ���

        // ������������������ѡ��
        GUI.color = new Color(0, 0, 0, 0.5f); // ������ɫ����ɫ����͸����
        GUI.DrawTexture(new Rect(healthBarX, healthBarY, Screen.width, healthBarHeight), Texture2D.whiteTexture);

        // ����������
        GUI.color = healthBarColor; // ������������ɫ
        GUI.DrawTexture(new Rect(healthBarX, healthBarY, healthBarWidth, healthBarHeight), Texture2D.whiteTexture);

        // ������ɫ
        GUI.color = Color.white;
    }
}
