using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 检测玩家受伤的类，通过碰撞检测来减少玩家的生命值。
/// </summary>
public class PlayerDamagerChecker : MonoBehaviour
{
    [SerializeField]
    private PlayerData_Temp playerData; // 引用玩家数据组件

    // Start is called before the first frame update        
    void Start()
    {
        // 获取父对象的 PlayerData_Temp 组件
        playerData = GetComponentInParent<PlayerData_Temp>();

        if (playerData == null)
        {
            Debug.LogError("无法在父对象中找到 PlayerData_Temp 组件！");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 使用触发器检测其他物体，例如伤害源
        if (other.CompareTag("DamageSource")) // 假设伤害源的 Tag 为 "DamageSource"
        {
            float damageAmount = 10f; // 示例伤害值
            TakeDamage(damageAmount);  // 调用处理伤害的方法
            Debug.Log($"玩家受伤: -{damageAmount} 生命值: {playerData.health}"); // 输出调试信息
        }
    }

    // 处理伤害逻辑
    private void TakeDamage(float damageAmount)
    {
        if (playerData != null)
        {
            playerData.ChangeHealth((int)damageAmount); // 调用 PlayerData_Temp 的 TakeDamage 方法
            
            // 检查是否死亡
            if (playerData.health <= 0)
            {
                Die();
            }
        }
    }

    // 玩家死亡处理
    private void Die()
    {
        Debug.Log("玩家死亡，处理死亡逻辑。");
        // 在这里可以添加玩家死亡后的逻辑，例如播放死亡动画、重置场景等
    }

    // Update is called once per frame
    void Update()
    {
        // 如果需要，可以在这里处理其他逻辑
        //按下-键减少生命值
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            TakeDamage(10f);
        }
    }
}
