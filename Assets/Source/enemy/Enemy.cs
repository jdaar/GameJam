using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private BaseEnemyBehaviour _behaviour;

    [SerializeField] private float _speed = 0.0000001f;
    private float _journeyLength;
    private float _movementStartTime;

    private bool _canMove = true;

    private Transform _transform;
    private Transform _target;

    private Animator _animator;

    private static string PUNCH_SOUND_PATH = "0_Enemy_Death";

    public void SetBehaviour<T> () where T: BaseEnemyBehaviour, new()
    {
        this._behaviour = new T();
    }

    public void OnPlayerHit() {
        if (Vector3.Distance(_transform.position, _target.position) < 2.5 && _canMove) {
            StartCoroutine(AnimateAndMove("Wasabi_Get_Punch"));
        }
        
        AudioClip punchAudioClip = Resources.Load<AudioClip>(Enemy.PUNCH_SOUND_PATH);

        AudioService.Instance.Play(Enemy.PUNCH_SOUND_PATH);
    }

    private IEnumerator AnimateAndMove(string animationName)
    {
        _animator.Play(animationName);
        _canMove = false;
        yield return new WaitForSeconds(1);
        _transform.position = EnemyController.GetRandomSpawnPosition();
        Quaternion flippedQuaternion = Quaternion.Euler(0, 180, 0);
        if (_transform.position.x < 0)
        {
            _transform.rotation = flippedQuaternion;
        } else {
            _transform.rotation = Quaternion.identity;
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

        if (Vector3.Distance(_transform.position, _target.position) < 1.25) {
            this._behaviour.Attack();
            StartCoroutine(AnimateAndMove("Wasabi_Punch"));
            _transform.position = EnemyController.GetRandomSpawnPosition();
            OnPositionChange();
        }

        if (_canMove) {
            float distanceCovered = ((Time.time - _movementStartTime) * _speed) / _journeyLength;
            transform.position = this._behaviour.Move(this._transform.position, this._target.position, distanceCovered);
        }
    }
}
