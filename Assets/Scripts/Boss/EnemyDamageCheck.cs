using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCheck :EnemyHurt
{
    public int hp;
    public override void EnemyDead()
    {
        hp--;
        Debug.Log("BossHp--");
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
