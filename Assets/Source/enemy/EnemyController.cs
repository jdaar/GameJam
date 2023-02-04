using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyController: MonoBehaviour
{
    private List<Enemy> _enemies;
    [SerializeField] private int _enemyCount = 10;

    private static string ENEMY_PREFAB_PATH = "prefabs/Enemy"; 

    void Start()
    {
        this._enemies = new List<Enemy>();
        this.PopulateEnemies();
    }

    private void PopulateEnemies()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            _enemies.Add(EnemyController.GenerateEnemyWithRandomBehaviour());
        }
    }

    private static Enemy GenerateEnemyWithRandomBehaviour()
    {
        Enemy enemyPrefab = Resources.Load<Enemy>(EnemyController.ENEMY_PREFAB_PATH);

        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not found at path: " + ENEMY_PREFAB_PATH);
            return null;
        }

        Random random = new Random();

        int randomScalar = random.Next(1, 3);
        bool direction = random.Next(0, 2) == 1;

        Vector3 randomPosition = new Vector3(
            direction ? -10 * randomScalar : 10 * randomScalar,
            0,
            0);
        Debug.Log(randomPosition);

        Enemy newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

        int randomBehaviour = random.Next(0, 2);

        switch (randomBehaviour)
        {
            case 0:
                newEnemy.SetBehaviour<MeleeEnemyBehaviour>();
                break;
            case 1:
                newEnemy.SetBehaviour<RangeEnemyBehaviour>();
                break;
            default:
                Debug.LogError("Invalid behaviour index: " + randomBehaviour);
                break;
        }

        return newEnemy;
    }
}
