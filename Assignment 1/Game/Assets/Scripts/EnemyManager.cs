using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<Enemy> _enemies;
    [SerializeField] private RangedEnemy _rangedEnemyPrefab;
    [SerializeField] private SelfHealingEnemy _healingEnemyPrefab;

    private void Start()
    {
        _enemies ??= new List<Enemy>();
        CreateEnemies(InstantiateEnemies(_rangedEnemyPrefab, count: 5));
        CreateEnemies(InstantiateEnemies(_healingEnemyPrefab, count: 5));
    }

    private Enemy[] InstantiateEnemies(Enemy enemyPrefab, int count = 0)
    {
        Enemy[] enemies = new Enemy[count];
        for (int i = 0; i < count; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity);
        }
        return enemies;
    }

    public void CreateEnemies(params Enemy[] enemies)
    {
        foreach (Enemy enemy in enemies) { 
            _enemies.Add(enemy);
            enemy.transform.position = GetSpawnPosition();
            enemy.gameObject.SetActive(true);
            enemy.enemyRigidBody.gravityScale = 0f;
            enemy.enemyRigidBody.isKinematic = true;
            enemy.enemySprite.sortingOrder = 97;
        }
    }
    private Vector3 GetSpawnPosition() {
        return new Vector3(Random.Range(70, 110), Random.Range(-280, -90), 0);
    }
}
