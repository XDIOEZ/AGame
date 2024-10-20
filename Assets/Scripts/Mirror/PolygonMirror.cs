using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonMirror : MonoBehaviour
{
    [Header("声音配置")]
    [Tooltip("击中反射镜的声音")]
    public string hitSound = "OnHitMirror";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的物体是否是光线子弹
        if (collision.CompareTag("Light_Bullet"))
        {
            // 播放击中反射镜的声音
            MusicMgr.Instance.PlaySound(hitSound, false);
            Vector2 collisionPoint = collision.ClosestPoint(transform.position);
            Vector2 reflectionNormal = CalculateReflectionNormal(collisionPoint);

            // 构造击中反射镜的事件数据
            HitInfo hitInfo = new HitInfo(reflectionNormal, collisionPoint);

            // 触发事件并传递法线
            EventCenter.Instance.EventTrigger(
                $"{collision.gameObject.GetInstanceID()}_OnHitMirror",
                hitInfo
            );
        }
    }

    private Vector2 CalculateReflectionNormal(Vector2 collisionPoint)
    {
        // 获取反射镜的多边形碰撞体
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        Vector2[] points = polygonCollider.points;
        Vector2 localPoint = transform.InverseTransformPoint(collisionPoint);

        // 计算法线
        Vector2 normal = Vector2.zero;
        for (int i = 0; i < points.Length; i++)
        {
            Vector2 p1 = points[i];
            Vector2 p2 = points[(i + 1) % points.Length];

            // 计算边的法线
            Vector2 edge = p2 - p1;
            Vector2 edgeNormal = new Vector2(-edge.y, edge.x).normalized;

            // 检查碰撞点是否在边的附近
            if (IsPointNearLine(localPoint, p1, p2, 0.01f))
            {
                normal = edgeNormal;
                break;
            }
        }

        // 将法线转换到世界坐标系
        return transform.TransformDirection(normal);
    }

    private bool IsPointNearLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd, float threshold)
    {
        Vector2 line = lineEnd - lineStart;
        float lineLength = line.magnitude;
        if (lineLength < 0.01f)
            return false;

        // 计算点到线的垂直距离
        float distance =
            Mathf.Abs((line.x * (lineStart.y - point.y)) - (line.y * (lineStart.x - point.x)))
            / lineLength;
        return distance < threshold;
    }
}
