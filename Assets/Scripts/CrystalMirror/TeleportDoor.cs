// 传送前克隆对象
// 传送时更新可隆体
// 传送后交换对象与克隆体的位置
// TODO 创建多个传送任务
// TODO 克隆体反作用于本体
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    #region 属性
    public TeleportDoor pairedDoor; // 配对的传送门
    private bool inTeleport; // 是否在传送中

    [HideInInspector]
    public List<int> teleportingID = new List<int>(); // 正在传送的主体对象ID

    // 接受的标签
    private string[] AcceptedTags
    {
        get => GetComponentInParent<CrystalMirror>().acceptedTags;
        set => GetComponentInParent<CrystalMirror>().acceptedTags = value;
    }

    // 速度变化倍数
    private float SpeedMultiplier
    {
        get => GetComponentInParent<CrystalMirror>().speedMultiplier;
        set => GetComponentInParent<CrystalMirror>().speedMultiplier = value;
    }
    #endregion

    #region 功能主体
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查对象的标签是否在接受的标签数组中
        foreach (string tag in AcceptedTags)
        {
            // 如果对象标签匹配，则执行传送
            if (collision.CompareTag(tag))
            {
                BeforTeleport(collision);
                break; // 找到匹配的标签后即可退出匹配
            }
        }
    }

    private void BeforTeleport(Collider2D collision)
    {
        // 设置本体参数
        if (collision.TryGetComponent<Renderer>(out var renderer))
        {
            Material material = renderer.material;
            material.SetVector("_DoorPos", transform.position);
            material.SetVector(
                "_DoorNormal",
                (gameObject.name == "LeftDoor") ? -transform.right : transform.right
            );
        }

        // 设置克隆对象参数
        GameObject clonedObject = CloneObject(collision.gameObject);
        if (clonedObject.TryGetComponent<Renderer>(out var clonedRenderer))
        {
            Material clonedMaterial = clonedRenderer.material;
            clonedMaterial.SetVector("_DoorPos", pairedDoor.transform.position);
            clonedMaterial.SetVector(
                "_DoorNormal",
                (pairedDoor.gameObject.name == "LeftDoor")
                    ? -pairedDoor.transform.right
                    : pairedDoor.transform.right
            );
        }

        // 给出口设置忽略碰撞
        Physics2D.IgnoreCollision(
            clonedObject.GetComponent<Collider2D>(),
            pairedDoor.GetComponent<Collider2D>(),
            true
        );

        // 开始传送
        inTeleport = true;

        // 记录正在传送的主体对象ID
        teleportingID.Add(collision.GetInstanceID());

        // 同步协程
        StartCoroutine(SynchronizeCoroutine(collision.gameObject, clonedObject));
    }

    private void AfterTeleport(Collider2D collision)
    {
        // 移除正在传送的主体对象ID(传送结束)
        teleportingID.Remove(collision.GetInstanceID());

        if (teleportingID.Count <= 0)
            inTeleport = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 如果该对象正在传送，则结束
        if (teleportingID.Contains(collision.GetInstanceID()))
        {
            AfterTeleport(collision);
        }
    }
    #endregion

    #region 辅助方法
    private GameObject CloneObject(GameObject objectToClone)
    {
        // 创建克隆对象的实例
        GameObject clonedObject = Instantiate(objectToClone);

        // 检查组件
        if (clonedObject.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.gravityScale = 0; // 克隆体不受重力
            rb.isKinematic = true; // 克隆体不受物理影响
        }
        Collider2D collider = clonedObject.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false; // 克隆体不受碰撞
        }

        return clonedObject;
    }

    private IEnumerator SynchronizeCoroutine(GameObject gameObject, GameObject clonedGameObject)
    {
        while (inTeleport)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D clonedRb = clonedGameObject.GetComponent<Rigidbody2D>();
            if (rb != null && pairedDoor != null && clonedRb != null)
            {
                // 计算当前门的坐标到被传送对象的距离向量
                Vector3 entryPosition = transform.position;
                Vector3 exitPosition = pairedDoor.transform.position;

                // 计算被传送对象相对于入口的向量
                Vector3 entryToCollider = gameObject.transform.position - entryPosition;

                // 计算被传送对象的当前旋转角度
                float currentAngle = gameObject.transform.rotation.eulerAngles.z;

                // 计算被传送对象的入口旋转
                float entryRotation = transform.rotation.eulerAngles.z;
                float exitRotation = pairedDoor.transform.rotation.eulerAngles.z;

                // 计算两个角度之间的最小差值
                float angleDifference = Mathf.DeltaAngle(entryRotation, exitRotation);

                // 旋转这个向量到出口的方向
                Vector3 newRelativePosition =
                    Quaternion.Euler(0, 0, angleDifference) * entryToCollider;

                // 计算目标位置
                Vector3 targetPosition = exitPosition + newRelativePosition;

                // 更新克隆体的位置
                clonedRb.position = targetPosition;

                // 更新克隆体的速度
                clonedRb.velocity =
                    Quaternion.Euler(0, 0, angleDifference) * rb.velocity * SpeedMultiplier;

                // 更新克隆体的旋转
                float newAngle = currentAngle + angleDifference;
                clonedGameObject.transform.rotation = Quaternion.Euler(0, 0, newAngle);
            }
            yield return null;
        }

        // 如果本体不在入口的可见方向上则交换位置
        if (gameObject.TryGetComponent<Renderer>(out var renderer))
        {
            Material material = renderer.material;
            Vector3 doorNormal = material.GetVector("_DoorNormal");

            if (Vector3.Dot(doorNormal, gameObject.transform.position - transform.position) < 0)
            {
                // 交换本体与克隆体的位置、旋转
                gameObject.transform.GetPositionAndRotation(
                    out Vector3 position,
                    out Quaternion rotation
                );
                gameObject.transform.SetPositionAndRotation(
                    clonedGameObject.transform.position,
                    clonedGameObject.transform.rotation
                );
                clonedGameObject.transform.SetPositionAndRotation(position, rotation);

                // 交换本体与克隆体的速度
                Rigidbody2D rb1 = gameObject.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = clonedGameObject.GetComponent<Rigidbody2D>();
                if (rb1 != null && rb2 != null)
                {
                    Vector3 velocity1 = rb1.velocity;
                    Vector3 velocity2 = rb2.velocity;
                    rb1.velocity = velocity2;
                    rb2.velocity = velocity1;
                }
            }

            // 销毁克隆体
            Destroy(clonedGameObject);

            // 调整可见范围到完全可见
            material.SetVector("_DoorPos", new Vector3(0, -int.MaxValue, 0));
            material.SetVector("_DoorNormal", new Vector3(0, 1, 0));
        }
    }
    #endregion
}
