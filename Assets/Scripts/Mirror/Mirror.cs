using UnityEngine;

[System.Serializable]
public struct HitInfo
{
    public Vector2 ReflectionNormal;
    public Vector2 CollisionPoint;

    public HitInfo(Vector2 reflectionNormal, Vector2 collisionPoint)
    {
        ReflectionNormal = reflectionNormal;
        CollisionPoint = collisionPoint;
    }
}

[RequireComponent(typeof(BoxCollider2D))]
public class Mirror : MonoBehaviour
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
        // 获取反射镜的边界
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

        // 计算碰撞点相对于反射镜的局部坐标
        Vector2 localPoint = transform.InverseTransformPoint(collisionPoint);

        // 计算法线
        Vector2 normal = Vector2.zero;

        // 检查碰撞点在哪一侧，并设置相应的法线
        if (Mathf.Abs(localPoint.x - bounds.min.x) < 0.01f) // 左边界
        {
            normal = Vector2.left;
        }
        else if (Mathf.Abs(localPoint.x - bounds.max.x) < 0.01f) // 右边界
        {
            normal = Vector2.right;
        }
        else if (Mathf.Abs(localPoint.y - bounds.min.y) < 0.01f) // 下边界
        {
            normal = Vector2.down;
        }
        else if (Mathf.Abs(localPoint.y - bounds.max.y) < 0.01f) // 上边界
        {
            normal = Vector2.up;
        }

        // 将法线转换到世界坐标系
        return transform.TransformDirection(normal);
    }
}
