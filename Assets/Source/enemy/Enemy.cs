using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private BaseEnemyBehaviour _behaviour;

    [SerializeField] private float _speed = 0.01f;
    private float _journeyLength;
    private float _movementStartTime;

    private bool _canMove = true;
    private AttackDirection _directionRelativeToPlayer;

    private Transform _transform;
    private Transform _target;

    private Animator _animator;

    private static string DEATH_SOUND_PATH = "0_Enemy_Death";

    public void SetBehaviour<T> () where T: BaseEnemyBehaviour, new()
    {
        this._behaviour = new T();
    }

    public void OnPlayerHit(AttackDirection direction) {
        if (Vector3.Distance(_transform.position, _target.position) < 2.5 && _canMove && direction == _directionRelativeToPlayer) {
            StartCoroutine(AnimateAndMove("Wasabi_Get_Punch"));
        }
        
    }

    private IEnumerator AnimateAndMove(string animationName)
    {
        _animator.Play(animationName);
        AudioService.Instance.Play(Enemy.DEATH_SOUND_PATH);
        _canMove = false;
        yield return new WaitForSeconds(1);
        _transform.position = EnemyController.GetRandomSpawnPosition();
        Quaternion flippedQuaternion = Quaternion.Euler(0, 180, 0);
        if (_transform.position.x < 0)
        {
            _transform.rotation = flippedQuaternion;
            _directionRelativeToPlayer = AttackDirection.LEFT;
        } else {
            _transform.rotation = Quaternion.identity;
            _directionRelativeToPlayer = AttackDirection.RIGHT;
        }

        OnPositionChange();
        _canMove = true;
    }

    void Start()
    {
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._target = GameObject.FindObjectsOfType<Player>()[0].GetComponent<Transform>();
        this._movementStartTime = Time.time;
        this._journeyLength = Vector3.Distance(this._transform.position, this._target.position);

        if (_transform.position.x < 0)
        {
            _directionRelativeToPlayer = AttackDirection.LEFT;
        } else {
            _directionRelativeToPlayer = AttackDirection.RIGHT;
        }

        MainEvent.OnPlayerHit += this.OnPlayerHit;
    }

    void OnPositionChange()
    {
        this._movementStartTime = Time.time;
        this._journeyLength = Vector3.Distance(this._transform.position, this._target.position);
        this._target = GameObject.FindObjectsOfType<Player>()[0].GetComponent<Transform>();
    }

    void Update()
    {
        if (this._behaviour == null || this._target == null)
            return;

        if (Vector3.Distance(_transform.position, _target.position) < 1.8 && _canMove) {
            this._behaviour.Attack();
            StartCoroutine(AnimateAndMove("Wasabi_Punch"));
            OnPositionChange();
        }

        if (_canMove) {
            float distanceCovered = ((Time.time - _movementStartTime) * _speed) / _journeyLength;
            transform.position = this._behaviour.Move(this._transform.position, this._target.position, distanceCovered);
        }
    }
}
