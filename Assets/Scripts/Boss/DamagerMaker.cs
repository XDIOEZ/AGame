using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerMaker : MonoBehaviour
{
    public int damage = 1; // 伤害值
    [SerializeField]
    private PolygonCollider2D polygonCollider; // 多边形碰撞器引用
    private bool CanDamaged = true; // 标志位，追踪是否已经对玩家造成过伤害
    private bool wasColliderEnabled = false; // 记录上一次碰撞器的启用状态

    // Start在游戏开始时调用
    void Start()
    {
        // 获取当前物体上的PolygonCollider2D组件
        polygonCollider = GetComponent<PolygonCollider2D>();

        if (polygonCollider == null)
        {
            Debug.LogError("没有找到PolygonCollider2D组件。");
        }

        // 初始化碰撞器的启用状态
        wasColliderEnabled = polygonCollider.enabled;
    }

    // Update在每帧调用
    void Update()
    {
        // 检查多边形碰撞器的启用状态是否发生变化
        if (polygonCollider != null && polygonCollider.enabled != wasColliderEnabled)
        {
            // 如果碰撞器从激活状态变为失活状态
            if (!polygonCollider.enabled)
            {
                // 重置CanDamaged为true
                CanDamaged = true;
            }

            // 更新记录的碰撞器状态
            wasColliderEnabled = polygonCollider.enabled;
        }

        // 检查多边形碰撞器是否启用且CanDamaged为true
        if (polygonCollider != null && polygonCollider.enabled && CanDamaged)
        {
            // 创建一个接触过滤器，只检测"Player"层的物体
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Player"));

            // 用于存储检测到的碰撞体的列表
            List<Collider2D> results = new List<Collider2D>();

            // 检查多边形碰撞器内的碰撞情况
            int hitCount = polygonCollider.OverlapCollider(filter, results);

            // 如果检测到与玩家的碰撞
            if (hitCount > 0)
            {
                foreach (var collider in results)
                {
                    // 从碰撞的物体上获取PlayerData_Temp组件
                    PlayerData_Temp playerData = collider.GetComponent<PlayerData_Temp>();
                    if (playerData != null)
                    {
                        // 调用ChangeHealth函数对玩家造成伤害
                        playerData.ChangeHealth(-damage);
                        Debug.Log("对玩家造成了伤害: " + damage);

                        // 设置CanDamaged为false，表示已经对玩家造成过伤害
                        CanDamaged = false;
                        break;
                    }
                }
            }
        }
    }
}
