using UnityEngine;

public class RangeEnemyBehaviour : BaseEnemyBehaviour
{
    public override Vector3 Move(Vector3 initialPosition, Vector3 targetPosition, float distanceCovered) {
        return Vector3.Lerp(initialPosition, targetPosition, distanceCovered);
    }
    public override void Attack() {
        MainEvent.OnPlayerHit?.Invoke();
    }

    public override void OnHit() {
        Debug.Log("MeleeBehaviour RecieveDamage");
    }

    public override void OnSpawn()
    {
        throw new System.NotImplementedException();
    }
}
