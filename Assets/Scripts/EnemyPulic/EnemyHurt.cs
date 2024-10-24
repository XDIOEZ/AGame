using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHurt : MonoBehaviour
{
    public void EnemyDestroy()
    {
        EnemyDead();
    }

    public abstract void EnemyDead(); // 处理死亡逻辑，包含声音播放等

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家受伤: EnemyHurt.cs: OnTriggerEnter2D: Player");
        }
    }
}
