using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeRange : MonoBehaviour
{
    [Header("可见范围设置")]
    public float[] radii = new float[1]; // 初始化为一个默认值数组

    [Header("Gizmos 颜色设置")]
    public Color gizmoColor = Color.red; // 暴露 Gizmos 颜色设置

    // 在场景视图中绘制可见圆
    private void OnDrawGizmos()
    {
        // 获取当前对象的坐标作为圆心
        Vector2 center = transform.position;

        // 检查 radii 数组是否已初始化并具有元素
        if (radii != null && radii.Length > 0)
        {
            // 绘制每个圆
            for (int i = 0; i < radii.Length; i++)
            {
                Gizmos.color = gizmoColor; // 使用暴露的颜色
                Gizmos.DrawWireSphere(center, radii[i]); // 绘制圆的边界
            }
        }
        else
        {
            Debug.LogWarning("radii 数组未初始化或为空。");
        }
    }
}
