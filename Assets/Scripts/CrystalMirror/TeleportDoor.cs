// 传送前克隆对象
// 传送时更新可隆体
// 传送后交换对象与克隆体的位置
// TODO 创建多个传送任务
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    public TeleportDoor pairedDoor; // 配对的传送门
    private bool inTeleport; // 是否在传送中
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

    #region 传送回调
    private void BeforTeleport(Collider2D collision)
    {
        // 给出口设置忽略碰撞
        pairedDoor.ignoreID.Add(collision.GetInstanceID());

        // 开始传送
        inTeleport = true;

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
        GameObject clonedObject = CloneObject(
            collision.gameObject,
            pairedDoor.transform.position,
            pairedDoor.transform.rotation
        );
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

        // 同步协程
        StartCoroutine(SynchronizeCoroutine(collision.gameObject, clonedObject));
    }

    private void AfterTeleport(Collider2D collision)
    {
        // 结束传送
        inTeleport = false;

        // 移除忽略的碰撞
        ignoreID.Remove(collision.GetInstanceID());
    }
    #endregion

    #region 功能主体
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
                break; // 找到匹配的标签后即可退出匹配
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 当被传送对象离开出口的触发器时，恢复与出口传送门的碰撞
        if (ignoreID.Contains(collision.GetInstanceID()))
        {
            AfterTeleport(collision);
        }
    }
    #endregion

    #region 辅助方法
    private GameObject CloneObject(GameObject objectToClone, Vector3 position, Quaternion rotation)
    {
        // 创建克隆对象的实例
        GameObject clonedObject = Instantiate(objectToClone, position, rotation);

        // 检查组件
        // Rigidbody2D rb = clonedObject.GetComponent<Rigidbody2D>();
        // if (rb != null)
        // {
        //     rb.isKinematic = true;
        // }
        // Collider2D collider = clonedObject.GetComponent<Collider2D>();
        // if (collider != null)
        // {
        //     collider.enabled = false;
        // }

        // 如果有其他不需要的组件，可以继续添加类似的检查和删除
        // 例如：如果需要删除某个自定义组件
        // MyCustomComponent customComponent = clonedObject.GetComponent<MyCustomComponent>();
        // if (customComponent != null)
        // {
        //     Destroy(customComponent);
        // }

        clonedObject.SetActive(false); // 克隆对象默认隐藏

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

                // 确保克隆体可见
                if (clonedGameObject.activeSelf == false)
                {
                    clonedGameObject.SetActive(true);
                }

                // 延迟一帧
                yield return new WaitForFixedUpdate();

                // 反向计算并更新本体的位置、速度、旋转
                // Vector3 clonedToCollider = clonedGameObject.transform.position - exitPosition;
                // Vector3 newRelativePosition2 =
                //     Quaternion.Euler(0, 0, -angleDifference) * clonedToCollider;
                // Vector3 targetPosition2 = entryPosition + newRelativePosition2;
                // rb.position = targetPosition2;
                // rb.velocity =
                //     Quaternion.Euler(0, 0, -angleDifference) * clonedRb.velocity * SpeedMultiplier;
                // float newAngle2 = currentAngle - angleDifference;
                // gameObject.transform.rotation = Quaternion.Euler(0, 0, newAngle2);
            }
            yield return null;
        }

        // 交换本体与克隆体的位置、旋转
        gameObject.transform.GetPositionAndRotation(out Vector3 position, out Quaternion rotation);
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

        // 销毁克隆体
        Destroy(clonedGameObject);

        // 确保Renderer组件存在
        if (gameObject.TryGetComponent<Renderer>(out var renderer))
        {
            // 获取材质
            Material material = renderer.material;
            material.SetVector("_DoorPos", new Vector3(0, -int.MaxValue, 0));
            material.SetVector("_DoorNormal", new Vector3(0, 1, 0));
        }
    }
    #endregion
}
