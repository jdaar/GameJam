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
    private Transform _transform;

    public void Attack(AttackDirection direction)
    {
        Animator _animator = gameObject.GetComponent<Animator>();
        if (direction == AttackDirection.RIGHT) {
            _transform.localScale = new Vector3(1, 1, 1);
        }
        if (direction == AttackDirection.LEFT) {
            _transform.localScale = new Vector3(-1, 1, 1);
        }
        _animator.Play("Ruth_Punch");


        MainEvent.OnPlayerHit?.Invoke();
    }

    private void OnHit()
    {
        Debug.Log("Player RecieveDamage");
        this._health -= 10;
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
