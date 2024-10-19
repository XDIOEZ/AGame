using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPTP : EnemyHurt
{
    [Header("节点配置")]
    [Tooltip("所有路径点的父节点")]
    public Transform Path; // Path节点

    [Tooltip("移动对象的父节点")]
    public Transform Moved; // Moved节点

    [Header("数据配置")]
    [Min(0)]
    [Tooltip("默认静默时间")]
    public float defaultIdleTime = 0f; // 默认静默时间

    [Min(0)]
    [Tooltip("最大额外静默时间")]
    public float maxIdleTimeRandom = 0f; // 最大额外静默时间

    [Tooltip("移动速度")]
    public float moveSpeed = 2f; // 移动速度

    [Tooltip("移动策略")]
    public IMoveStrategy moveStrategy; // 移动策略

    [Header("声音配置")]
    [Tooltip("移动声音")]
    public string moveAudio; // 移动声音

    [Tooltip("静默声音")]
    public string idleAudio; // 静默声音

    [Tooltip("死亡声音")]
    public string deathAudio = "OnDead"; // 死亡声音
    private List<Transform> points; // 存储Path下所有的Point
    private int currentPointIndex = 0; // 当前目标点的索引

    void Start()
    {
        InitializePoints();
        if (points.Count > 0)
        {
            currentPointIndex = FindNearestPointIndex(); // 找到离Moved最近的点
            StartCoroutine(MoveToPoints());
        }
    }

    void InitializePoints()
    {
        points = new List<Transform>();
        foreach (Transform child in Path)
        {
            points.Add(child); // 将Path下的所有Point添加到列表中
        }
    }

    int FindNearestPointIndex()
    {
        float nearestDistance = Mathf.Infinity;
        int nearestIndex = 0;

        for (int i = 0; i < points.Count; i++)
        {
            float distance = Vector3.Distance(Moved.position, points[i].position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestIndex = i; // 更新最近点的索引
            }
        }

        return nearestIndex; // 返回离Moved最近的点的索引
    }

    IEnumerator MoveToPoints()
    {
        while (true)
        {
            // 移动到当前目标点
            yield return StartCoroutine(MoveToPoint(points[currentPointIndex]));

            // 更新目标点索引
            currentPointIndex = (currentPointIndex + 1) % points.Count; // 循环
        }
    }

    IEnumerator MoveToPoint(Transform targetPoint)
    {
        while (Vector3.Distance(Moved.position, targetPoint.position) > 0.01f)
        {
            Moved.position = moveStrategy.Move(Moved, targetPoint.position, moveSpeed);
            yield return null; // 等待下一帧
        }
        yield return new WaitForSeconds(defaultIdleTime + Random.Range(0f, maxIdleTimeRandom)); // 等待随机静默时间
        Moved.position = targetPoint.position; // 确保最终位置到达目标点
    }

    public override void EnemyDead()
    {
        MusicMgr.Instance.PlaySound(deathAudio, false);
    }
}
