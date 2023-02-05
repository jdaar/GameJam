using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using Random = System.Random;

public class EnemyController: MonoBehaviour
{
    private List<Enemy> _enemies;
    private static float _enemyPositionY = -3.5f;

    [SerializeField] private int _enemyCount = 10;

    private static string ENEMY_PREFAB_PATH = "prefabs/Enemy"; 

    [SerializeField] private EnemyStorer _enemyStorer;

    void Start()
    {
        if (this._enemyStorer == null)
        {
            Debug.LogError("Enemy storer not found");
            return;
        }
        this._enemies = new List<Enemy>();
        this.PopulateEnemies();
    }

    private void PopulateEnemies()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            _enemies.Add(this.GenerateEnemyWithRandomBehaviour());
        }
    }

    public static Vector3 GetRandomSpawnPosition() {
        Random random = new Random();
        int randomScalar = random.Next(1, 3);
        bool direction = random.Next(0, 2) == 1;

        Vector3 randomPosition = new Vector3(
            direction ? -15 * randomScalar : 15 * randomScalar,
            _enemyPositionY,
            0
        );

        return randomPosition;
    } 

    private Enemy GenerateEnemyWithRandomBehaviour()
    {
        Enemy enemyPrefab = Resources.Load<Enemy>(EnemyController.ENEMY_PREFAB_PATH);

        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not found at path: " + ENEMY_PREFAB_PATH);
            return null;
        }

        if (this._enemyStorer.enemyDefinitions == null || this._enemyStorer.enemyDefinitions.Count == 0)
        {
            Debug.LogError("Enemy definitions not found");
            return null;
        }

        Random random = new Random();

        EnemyDefinition enemyDefinition = this._enemyStorer.enemyDefinitions[random.Next(0, this._enemyStorer.enemyDefinitions.Count)];

        Vector3 randomPosition = EnemyController.GetRandomSpawnPosition();
        Quaternion flippedQuaternion = Quaternion.Euler(0, 180, 0);
        Enemy newEnemy;
        if (randomPosition.x < 0)
        {
            newEnemy = Instantiate(enemyPrefab, randomPosition, flippedQuaternion);
        } else {
            newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }

        switch (enemyDefinition.behaviour)
        {
            case 0:
                newEnemy.SetBehaviour(new MeleeEnemyBehaviour());
                break;
            case 1:
                newEnemy.SetBehaviour(new RangeEnemyBehaviour());
                break;
            default:
                Debug.LogError("Invalid behaviour index: " + enemyDefinition.behaviour);
                break;
        }

        newEnemy.SetSprite(enemyDefinition.sprite);
        newEnemy.SetAnimatorController(enemyDefinition.animationController);
        newEnemy.SetAnimationNames(enemyDefinition.punchAnimationName, enemyDefinition.deathAnimationName);
        newEnemy.SetSpeed(enemyDefinition.speed);
        newEnemy.SetThreshold(enemyDefinition.punchThreshold, enemyDefinition.deathThreshold);
        return newEnemy;
    }
}
