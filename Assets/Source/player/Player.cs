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
    public void Attack(AttackDirection direction)
    {
        Debug.Log("Player attacked in direction: " + direction);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
