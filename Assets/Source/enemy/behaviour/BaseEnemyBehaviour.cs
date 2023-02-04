using UnityEngine;

public abstract class BaseEnemyBehaviour
{
    public abstract Vector3 Move(Vector3 actualPosition, Vector3 targetPosition);
    public abstract void Attack();
    public abstract void OnHit();
    public abstract void OnSpawn();
}
