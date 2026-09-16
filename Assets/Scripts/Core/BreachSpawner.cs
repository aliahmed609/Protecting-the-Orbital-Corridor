using UnityEngine;

public class BreachSpawner : MonoBehaviour
{
    [SerializeField] private GameObject breachPrefab;
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private float spawnXMin = -8f;
    [SerializeField] private float spawnXMax = 8f;

    [Header("Critical")]
    [SerializeField] private float criticalSpawnMultiplier = 0.70f;

    [SerializeField] private float spawnCheckRadius = 0.8f;
    [SerializeField] private LayerMask obstacleLayer;

    private float nextSpawnTime;

    private void Update()
    {
        if (!roundManager.IsRoundActive)
            return;

        if (roundManager.CurrentPhase == RoundManager.GamePhase.Patrol)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnBreach();
            nextSpawnTime = Time.time + GetSpawnInterval();
        }
    }

    private void SpawnBreach()
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

        GameObject breach = Instantiate(
            breachPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Breach breachScript = breach.GetComponent<Breach>();

        if (breachScript != null)
        {
            breachScript.SetScoreManager(scoreManager);
        }
    }

    private float GetSpawnInterval()
    {
        if (roundManager.CurrentPhase == RoundManager.GamePhase.Critical)
        {
            return spawnInterval * criticalSpawnMultiplier;
        }

        return spawnInterval;
    }
}