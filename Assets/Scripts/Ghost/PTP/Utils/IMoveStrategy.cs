using UnityEngine;

public abstract class IMoveStrategy : ScriptableObject
{
    public abstract Vector3 Move(Transform movedObject, Vector3 targetPosition, float speed);
}
