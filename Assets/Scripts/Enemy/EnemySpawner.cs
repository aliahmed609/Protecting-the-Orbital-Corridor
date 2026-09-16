using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ObjectPool enemyPool;
    [SerializeField] private ObjectPool enemyProjectilePool;

    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnXMin = -8f;
    [SerializeField] private float spawnXMax = 8f;

    [SerializeField] private float spawnCheckRadius = 0.8f;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Difficulty")]
    [SerializeField] private float alertSpeedMultiplier = 1.25f;
    [SerializeField] private float criticalSpawnMultiplier = 0.70f;

    [Header("Movement")]
    [SerializeField] private float baseEnemySpeed = 2f;

    private float nextSpawnTime;

    private void Update()
    {
        if (roundManager == null)
            return;

        if (!roundManager.IsRoundActive)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            nextSpawnTime =
                Time.time + GetSpawnInterval();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPool == null)
        {
            Debug.LogError(
                "EnemySpawner: Enemy Pool is not assigned!"
            );

            return;
        }

        float randomX =
            Random.Range(
                spawnXMin,
                spawnXMax
            );

        Vector2 spawnPosition =
            new Vector2(
                randomX,
                transform.position.y
            );

        if (!SpawnUtility.IsPositionClear(
            spawnPosition,
            spawnCheckRadius,
            obstacleLayer))
        {
            return;
        }

        GameObject enemyObject =
            enemyPool.Get();

        if (enemyObject == null)
        {
            Debug.LogError(
                "EnemySpawner: Enemy Pool returned NULL!"
            );

            return;
        }

        enemyObject.transform.position =
            spawnPosition;

        enemyObject.transform.rotation =
            Quaternion.identity;

        Enemy enemy =
            enemyObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.SetSpeed(
                GetEnemySpeed()
            );

            enemy.SetScoreManager(
                scoreManager
            );

            enemy.SetRoundManager(
                roundManager
            );
            enemy.SetProjectilePool(
    enemyProjectilePool
);
        }
    }

    private float GetSpawnInterval()
    {
        if (roundManager.CurrentPhase ==
            RoundManager.GamePhase.Critical)
        {
            return spawnInterval *
                   criticalSpawnMultiplier;
        }

        return spawnInterval;
    }

    private float GetEnemySpeed()
    {
        if (roundManager.CurrentPhase ==
                RoundManager.GamePhase.Alert ||
            roundManager.CurrentPhase ==
                RoundManager.GamePhase.Critical)
        {
            return baseEnemySpeed *
                   alertSpeedMultiplier;
        }

        return baseEnemySpeed;
    }
}