using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class TeleportDoor : MonoBehaviour
{
    public TeleportDoor pairedDoor; // 配对的传送门
    #region 动态属性
    [HideInInspector]
    public List<int> ignoreID = new List<int>(); // 用于忽略碰撞的对象ID组

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

    private void BeforTeleport(Collider2D collision)
    {
        Renderer renderer = collision.GetComponent<Renderer>();
        // 确保Renderer组件存在
        if (renderer != null)
        {
            // 获取材质
            Material material = renderer.material;
            material.SetVector("_DoorPos", pairedDoor.transform.position);
            material.SetVector(
                "_DoorNormal",
                (pairedDoor.gameObject.name == "LeftDoor")
                    ? -pairedDoor.transform.right
                    : pairedDoor.transform.right
            );
        }
    }

    private void AfterTeleport(Collider2D collision)
    {
        Renderer renderer = collision.GetComponent<Renderer>();
        // 确保Renderer组件存在
        if (renderer != null)
        {
            // 获取材质
            Material material = renderer.material;
            material.SetVector("_DoorPos", new Vector3(0, -int.MaxValue, 0));
            material.SetVector("_DoorNormal", new Vector3(0, 1, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查是否需要忽略该对象
        if (ignoreID.Count > 0 && ignoreID.Contains(collision.GetInstanceID()))
            return;

        // 检查对象的标签是否在接受的标签数组中
        foreach (string tag in AcceptedTags)
        {
            // 如果对象标签匹配，则执行传送
            if (collision.CompareTag(tag))
            {
                BeforTeleport(collision);
                Debug.Log("传送 " + collision.name + " -> " + pairedDoor.name);
                Teleport(collision);
                break; // 找到匹配的标签后可以退出循环
            }
        }
    }

    private void Teleport(Collider2D collider)
    {
        // 获取对象的Rigidbody2D组件
        Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null && pairedDoor != null)
        {
            // 计算当前门的坐标到被传送对象的距离向量
            Vector3 entryPosition = transform.position;
            Vector3 exitPosition = pairedDoor.transform.position;

            // 计算被传送对象相对于入口的向量
            Vector3 entryToCollider = collider.transform.position - entryPosition;

            // 计算被传送对象的当前旋转角度
            float currentAngle = collider.transform.rotation.eulerAngles.z;

            // 计算被传送对象的入口旋转
            float entryRotation = transform.rotation.eulerAngles.z;
            float exitRotation = pairedDoor.transform.rotation.eulerAngles.z;

            // 计算两个角度之间的最小差值
            float angleDifference = Mathf.DeltaAngle(entryRotation, exitRotation);

            // 旋转这个向量到出口的方向
            Vector3 newRelativePosition = Quaternion.Euler(0, 0, angleDifference) * entryToCollider;

            // 计算目标位置
            Vector3 targetPosition = exitPosition + newRelativePosition;

            // 更新对象的位置
            rb.position = targetPosition;

            // 更新对象的速度
            rb.velocity = Quaternion.Euler(0, 0, angleDifference) * rb.velocity * SpeedMultiplier;

            // 更新对象的旋转
            float newAngle = currentAngle + angleDifference;
            collider.transform.rotation = Quaternion.Euler(0, 0, newAngle);

            // 设置忽略碰撞
            pairedDoor.ignoreID.Add(collider.GetInstanceID());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 当被传送对象离开出口的触发器时，恢复与出口传送门的碰撞
        if (ignoreID.Contains(collision.GetInstanceID()))
        {
            ignoreID.Remove(collision.GetInstanceID());
            Debug.Log(collision.name + " 已离开传送出口 " + pairedDoor.name);
            AfterTeleport(collision);
        }
    }
}
