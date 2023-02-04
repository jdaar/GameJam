using UnityEngine;

public class MeleeEnemyBehaviour : BaseEnemyBehaviour
{
    public override Vector3 Move(Vector3 actualPosition, Vector3 targetPosition) {
        return new Vector3(0, 0, 0);
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
