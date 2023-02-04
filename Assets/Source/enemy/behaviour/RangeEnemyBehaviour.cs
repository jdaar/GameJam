using UnityEngine;

public class RangeEnemyBehaviour : BaseEnemyBehaviour
{
    public override Vector3 Move(Vector3 actualPosition, Vector3 targetPosition) {
        Debug.LogWarning("RangeBehaviour Move");
        return targetPosition;
    }
    public override void Attack() {
    }

    public override void OnHit() {
        Debug.Log("MeleeBehaviour RecieveDamage");
    }

    public override void OnSpawn()
    {
        throw new System.NotImplementedException();
    }
}
