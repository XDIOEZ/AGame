using UnityEngine;

[CreateAssetMenu(fileName = "New Constant Speed Move Strategy", menuName = "Ghost/PTP/Utils/ConstantSpeedMoveStrategy")]
public class ConstantSpeedMove : IMoveStrategy
{
    public override Vector3 Move(Transform movedObject, Vector3 targetPosition, float speed)
    {
        return Vector3.MoveTowards(movedObject.position, targetPosition, speed * Time.deltaTime);
    }
}
