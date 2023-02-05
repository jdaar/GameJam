using UnityEngine;

public abstract class BaseEnemyBehaviour
{
    public abstract Vector3 Move(Vector3 actualPosition, Vector3 targetPosition, float distanceCovered);
    public abstract void Attack();
}
