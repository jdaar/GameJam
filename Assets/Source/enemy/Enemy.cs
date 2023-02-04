using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private BaseEnemyBehaviour _behaviour;

    public void SetBehaviour<T> () where T: BaseEnemyBehaviour, new()
    {
        this._behaviour = new T();
    }

    void Start()
    {
    }

    void Update()
    {
        Transform transform = gameObject.GetComponent<Transform>();

        if (this._behaviour == null)
            return;

        transform.position = this._behaviour.Move(transform.position, new Vector3(1, 1, 1));
    }
}
