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
    [SerializeField] private int _health = 3;
    private Transform _transform;

    public void Attack(AttackDirection direction)
    {
        Animator _animator = gameObject.GetComponent<Animator>();
        if (direction == AttackDirection.RIGHT) {
            _transform.rotation = Quaternion.identity;
        }
        if (direction == AttackDirection.LEFT) {
            Quaternion flippedQuaternion = Quaternion.Euler(0, 180, 0);
            _transform.rotation = flippedQuaternion;
        }
        _animator.Play("Ruth_Punch");


        MainEvent.OnPlayerHit?.Invoke();
    }

    private void OnHit()
    {
        Debug.Log("Player RecieveDamage");
        if (this._health <= 0)
        {
            Debug.Log("Player is dead");
            return;
        }
        this._health -= 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        MainEvent.OnPlayerHit += this.OnHit;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
