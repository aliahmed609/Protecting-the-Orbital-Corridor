using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private float spawnCheckRadius = 0.8f;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnXMin = -8f;
    [SerializeField] private float spawnXMax = 8f;

    [Header("Difficulty")]
    [SerializeField] private float alertSpeedMultiplier = 1.25f;
    [SerializeField] private float criticalSpawnMultiplier = 0.70f;

    [SerializeField] private float baseEnemySpeed = 2f;

    private float nextSpawnTime;

    private void Update()
    {
        if (!roundManager.IsRoundActive)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + GetSpawnInterval();
        }
    }

    private void SpawnEnemy()
    {
        float randomX = Random.Range(spawnXMin, spawnXMax);

        Vector2 spawnPosition = new Vector2(
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

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Enemy enemyScript = enemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.SetSpeed(GetEnemySpeed());
            enemyScript.SetScoreManager(scoreManager);
            enemyScript.SetRoundManager(roundManager);
        }
    }

    private float GetSpawnInterval()
    {
        if (roundManager.CurrentPhase == RoundManager.GamePhase.Critical)
            return spawnInterval * criticalSpawnMultiplier;

        return spawnInterval;
    }

    private float GetEnemySpeed()
    {
        if (roundManager.CurrentPhase == RoundManager.GamePhase.Alert ||
            roundManager.CurrentPhase == RoundManager.GamePhase.Critical)
        {
            return baseEnemySpeed * alertSpeedMultiplier;
        }

        return baseEnemySpeed;
    }
}