using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDefinition
{
    public Sprite sprite = null;

    [Range(0f, 1f)]
    public float speed = 0.1f;

    [Range(0, 1)]
    public int behaviour = 0;

    public RuntimeAnimatorController animationController = null;

    public string punchAnimationName = "Punch";
    public string deathAnimationName = "Death";

    [Range(0f, 5f)]
    public float deathThreshold = 2.4f;

    [Range(0f, 5f)]
    public float punchThreshold = 1.8f;
}

[CreateAssetMenu]
public class EnemyStorer : ScriptableObject
{
    public List<EnemyDefinition> enemyDefinitions = new List<EnemyDefinition>();
}