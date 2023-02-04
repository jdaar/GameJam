using UnityEngine;

public class MeleeBehaviour : BaseBehaviour
{
    public override void Attack() {
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("MeleeBehaviour Attack (space pressed)");
    }

    public override void OnHit() {
        Debug.Log("MeleeBehaviour RecieveDamage");
    }

    public override void OnSpawn()
    {
        throw new System.NotImplementedException();
    }
}
