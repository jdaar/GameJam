using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private BaseBehaviour _behaviour;

    public void SetBehaviour<T> () where T: BaseBehaviour, new()
    {
        this._behaviour = new T();
    }

    void Start()
    {
    }


    void Update()
    {
        this._behaviour?.Attack();
    }
}
