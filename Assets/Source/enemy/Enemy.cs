using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private BaseEnemyBehaviour _behaviour;

    [SerializeField] private float _speed = 0.0000001f;
    private float _journeyLength;
    private float _movementStartTime;

    private bool _attacked = false;

    private Transform _transform;
    private Transform _target;

    public void SetBehaviour<T> () where T: BaseEnemyBehaviour, new()
    {
        this._behaviour = new T();
    }

    void Start()
    {
        this._transform = gameObject.GetComponent<Transform>();
        this._target = GameObject.Find("Player").GetComponent<Transform>();
        this._movementStartTime = Time.time;
        this._journeyLength = Vector3.Distance(this._transform.position, this._target.position);
    }

    void Update()
    {
        if (this._behaviour == null || this._target == null)
            return;

        if (Vector3.Distance(_transform.position, _target.position) < 1.25 && !_attacked) {
            this._behaviour.Attack();
            this._attacked = true;
        }

        float distanceCovered = ((Time.time - _movementStartTime) * _speed) / _journeyLength;
        transform.position = this._behaviour.Move(this._transform.position, this._target.position, distanceCovered);
    }
}
