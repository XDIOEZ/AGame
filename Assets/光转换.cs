using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 光转换 : MonoBehaviour
{
    Transform yuan; // 圆形光源子物体
    Transform shu;  // 竖状光源子物体

    [Header("检测参数")]
    public Vector2 weizhi;  // 检测范围偏移
    public Vector2 boxSize; // 方形检测范围的大小
    public LayerMask jianceduixiang; // 用于检测玩家所在的图层

    private bool jiechu;          // 当前检测到的状态
    private bool previousJiechu;  // 上一帧的检测状态

    void Start()
    {
        // 查找玩家对象，并获取其子物体的光源引用
        PlayerLight wanjia = FindObjectOfType<PlayerLight>();
        if (wanjia != null)
        {
            yuan = wanjia.transform.Find("圆型光圈");
            shu = wanjia.transform.Find("竖状光条");

            if (yuan == null || shu == null)
            {
                Debug.LogError("未找到玩家的子物体光源，请检查命名。");
            }
        }
        else
        {
            Debug.LogError("请确保场景中存在 PlayerLight 组件的实例。");
        }

        // 初始化前一帧状态为false，避免误触发
        previousJiechu = false;
    }

    void Update()
    {
        CheckCollision();

        // 玩家进入范围时，切换子物体
        if (jiechu && !previousJiechu)
        {
            ToggleLight();
        }
        // 玩家离开范围时，再次切换子物体
        else if (!jiechu && previousJiechu)
        {
            ToggleLight();
        }

        // 更新前一帧状态
        previousJiechu = jiechu;
    }

    private void CheckCollision()
    {
        // 检测玩家是否在方形范围内
        jiechu = Physics2D.OverlapBox((Vector2)transform.position + weizhi, boxSize, 0, jianceduixiang);
    }

    private void ToggleLight()
    {
        if (yuan != null && shu != null)
        {
            // 切换两个子物体的启用状态
            bool yuanActive = yuan.gameObject.activeSelf;
            yuan.gameObject.SetActive(!yuanActive);  // 启用或禁用圆形光源
            shu.gameObject.SetActive(yuanActive);    // 启用或禁用竖状光源
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 可视化方形检测范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + weizhi, boxSize);
    }
}
