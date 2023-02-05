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

    private string _punchAnimationName;
    private string _deathAnimationName;

    private float _deathThreshold = 2.4f;
    private float _punchThreshold = 1.8f;

    private Animator _animator;

    private static string DEATH_SOUND_PATH = "0_Enemy_Death";

    public void SetBehaviour (BaseEnemyBehaviour behaviour)
    {
        this._behaviour = behaviour;
    }

    public void SetSprite(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    public void SetAnimatorController(RuntimeAnimatorController animatorController)
    {
        this._animator = gameObject.GetComponent<Animator>();
        this._animator.runtimeAnimatorController = animatorController;
    }

    public void SetAnimationNames(string punchAnimationName, string deathAnimationName)
    {
        this._punchAnimationName = punchAnimationName;
        this._deathAnimationName = deathAnimationName;   
    }

    public void SetThreshold(float punchThreshold, float deathThreshold)
    {
        this._punchThreshold = punchThreshold;
        this._deathThreshold = deathThreshold;   
    }

    public void SetSpeed(float speed)
    {
        this._speed = speed;
    }

    public void OnPlayerHit(AttackDirection direction) {
        if (_transform == null || _target == null) {
            return;
        }
        if (Vector3.Distance(_transform.position, _target.position) < this._deathThreshold && _canMove && direction == _directionRelativeToPlayer) {
            StartCoroutine(AnimateAndMove(this._deathAnimationName));
            MainEvent.OnEnemyDeath.Invoke();
        }
    }

    public void OnPlayerDeath() {
        _canMove = false;
    }

    private IEnumerator AnimateAndMove(string animationName)
    {
        _animator.Play(animationName);
        AudioService.Instance.Play(Enemy.DEATH_SOUND_PATH);
        _canMove = false;

        yield return new WaitForSeconds(2.5f);

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
        MainEvent.OnPlayerDeath += this.OnPlayerDeath;
    }

    void OnPositionChange()
    {
        this._movementStartTime = Time.time;
        this._journeyLength = Vector3.Distance(this._transform.position, this._target.position);
        this._target = GameObject.FindObjectsOfType<Player>()[0].GetComponent<Transform>();
    }

    void Update()
    {
        if (this._behaviour == null || this._target == null || this._transform == null)
            return;

        if (Vector3.Distance(_transform.position, _target.position) < this._punchThreshold && _canMove) {
            this._behaviour.Attack();
            StartCoroutine(AnimateAndMove(this._punchAnimationName));
            OnPositionChange();
        }

        if (_canMove) {
            float distanceCovered = ((Time.time - _movementStartTime) * _speed) / _journeyLength;
            transform.position = this._behaviour.Move(this._transform.position, this._target.position, distanceCovered);
        }
    }
}
