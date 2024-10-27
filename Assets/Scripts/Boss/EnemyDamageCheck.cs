using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCheck : MonoBehaviour
{
    public int hp = 100; // 假设初始生命值为100
    public int maxHp = 100; // 最大生命值
    public Color healthBarColor = Color.white; // 生命条颜色

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
            Debug.Log("子弹击中敌人: EnemyHurt.cs: OnTriggerEnter2D: Bullet");
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
        // 计算生命条的宽度
        float healthBarWidth = (hp / (float)maxHp) * Screen.width; // 根据生命值计算宽度
        float healthBarHeight = 20; // 生命条高度
        float healthBarX = 0; // X 坐标（从左到右）
        float healthBarY = Screen.height - healthBarHeight; // Y 坐标（底部）

        // 绘制生命条背景（可选）
        GUI.color = new Color(0, 0, 0, 0.5f); // 背景颜色（黑色，半透明）
        GUI.DrawTexture(new Rect(healthBarX, healthBarY, Screen.width, healthBarHeight), Texture2D.whiteTexture);

        // 绘制生命条
        GUI.color = healthBarColor; // 设置生命条颜色
        GUI.DrawTexture(new Rect(healthBarX, healthBarY, healthBarWidth, healthBarHeight), Texture2D.whiteTexture);

        // 重置颜色
        GUI.color = Color.white;
    }
}
