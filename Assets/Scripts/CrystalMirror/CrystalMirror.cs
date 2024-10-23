using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMirror : MonoBehaviour
{
    public string[] acceptedTags; // 可接受的对象标签
    public float speedMultiplier = 1.0f; // 速度变化倍数

    void Start()
    {
        // 添加事件监听器
        EventCenter.Instance.AddEventListener(
            "Teleport_Light_Bullet",
            () =>
            {
                MusicMgr.Instance.PlaySound("OnHitMirror", false);
            }
        );
    }
}
