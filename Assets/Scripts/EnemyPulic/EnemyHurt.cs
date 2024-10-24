using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHurt : MonoBehaviour
{
    [Header("敌人触碰伤害值")]
    public int damage = 1;

    public void EnemyDestroy()
    {
        EnemyDead();
    }

    public abstract void EnemyDead(); // 处理死亡逻辑，包含声音播放等

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("子弹击中敌人: EnemyHurt.cs: OnTriggerEnter2D: Bullet");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerData_Temp playerData))
            {
# if UNITY_EDITOR
                Debug.Log("玩家受伤: EnemyHurt.cs: OnTriggerEnter2D: Player");
# else
                playerData.ChangeHealth(damage);
# endif
            }
            else
            {
                Debug.LogError(
                    "玩家数据脚本未找到(受伤失效): EnemyHurt.cs: OnTriggerEnter2D: Player"
                );
            }
        }
    }
}
