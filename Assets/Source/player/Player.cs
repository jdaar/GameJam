using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackDirection
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    public void Attack(AttackDirection direction)
    {
        Debug.Log("Player attacked in direction: " + direction);
    }

    private void OnHit()
    {
        Debug.Log("Player RecieveDamage");
        this._health -= 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainEvent.OnPlayerHit += this.OnHit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
