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
    private Animator _animator;

    private static string PUNCH_SOUND_PATH = "5_Player_Punch";

    public void Attack(AttackDirection direction)
    {
        if (direction == AttackDirection.RIGHT) {
            _transform.rotation = Quaternion.identity;
        }
        if (direction == AttackDirection.LEFT) {
            Quaternion flippedQuaternion = Quaternion.Euler(0, 180, 0);
            _transform.rotation = flippedQuaternion;
        }
        _animator.Play("Ruth_Punch");

        AudioService.Instance.Play(Player.PUNCH_SOUND_PATH);

        MainEvent.OnPlayerHit?.Invoke();
    }

    private void OnPlayerTakeDamage() {
        _animator.Play("Ruth_Get_Punch");
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
        _animator = gameObject.GetComponent<Animator>();
        _transform = gameObject.GetComponent<Transform>();
        MainEvent.OnPlayerTakeDamage += this.OnPlayerTakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
