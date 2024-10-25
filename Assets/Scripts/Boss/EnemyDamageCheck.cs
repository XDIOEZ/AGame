using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCheck :MonoBehaviour
{
    public int hp;
    public  void EnemyDead()
    {
        hp--;
        Debug.Log("BossHp--");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light_Bullet"))
        {
            Debug.Log("×Óµ¯»÷ÖÐµÐÈË: EnemyHurt.cs: OnTriggerEnter2D: Bullet");
            Destroy(other.gameObject);
            EnemyDead();
        }
    }

        // Update is called once per frame
        void Update()
    {

        if (hp<=0)
        {
            EventCenter.Instance.EventTrigger("BossDead");
            Destroy(gameObject);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
