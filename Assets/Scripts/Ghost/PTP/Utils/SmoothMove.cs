using UnityEngine;

[CreateAssetMenu(
    fileName = "New SmoothMove Strategy",
    menuName = "Ghost/PTP/Utils/SmoothMoveStrategy"
)]
public class SmoothMove : IMoveStrategy
{
    private Vector3 velocity = Vector3.zero; // 用于存储移动速度

    public override Vector3 Move(Transform movedObject, Vector3 targetPosition, float speed)
    {
        float distance = Vector3.Distance(movedObject.position, targetPosition);

        // 使用 SmoothDamp 计算下一帧的目标位置
        Vector3 targetVelocity = Vector3.SmoothDamp(
            movedObject.position,
            targetPosition,
            ref velocity,
            distance / speed
        );

        // 返回新的位置
        return targetVelocity;
    }
}
