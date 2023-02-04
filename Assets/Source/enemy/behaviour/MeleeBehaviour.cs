using UnityEngine;

public class MeleeBehaviour : BaseBehaviour
{
    public override void Attack() {
        Debug.Log("MeleeBehaviour Attack");
    }

    public override void ReceiveDamage() {
        Debug.Log("MeleeBehaviour RecieveDamage");
    }
}
