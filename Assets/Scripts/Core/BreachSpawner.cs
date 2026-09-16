using UnityEngine;

public class BreachSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ObjectPool breachPool;

    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private float spawnXMin = -8f;
    [SerializeField] private float spawnXMax = 8f;

    [Header("Critical")]
    [SerializeField] private float criticalSpawnMultiplier = 0.70f;

    [Header("Spawn Check")]
    [SerializeField] private float spawnCheckRadius = 0.8f;
    [SerializeField] private LayerMask obstacleLayer;

    private float nextSpawnTime;

    // ==================================================
    // UNITY
    // ==================================================

    private void Start()
    {
        // Do not spawn on the first frame.
        nextSpawnTime =
            Time.time + spawnInterval;
    }

    private void Update()
    {
        if (roundManager == null)
            return;

        if (!roundManager.IsRoundActive)
            return;

        // Breaches do not spawn during Patrol.
        if (roundManager.CurrentPhase ==
            RoundManager.GamePhase.Patrol)
        {
            return;
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnBreach();

            nextSpawnTime =
                Time.time + GetSpawnInterval();
        }
    }

    // ==================================================
    // SPAWN
    // ==================================================

    private void SpawnBreach()
    {
        if (breachPool == null)
        {
            Debug.LogError(
                "BreachSpawner: Breach Pool is not assigned!"
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

        // Check the spawn position BEFORE
        // taking an object from the pool.
        if (!SpawnUtility.IsPositionClear(
            spawnPosition,
            spawnCheckRadius,
            obstacleLayer))
        {
            return;
        }

        GameObject breachObject =
            breachPool.Get();

        if (breachObject == null)
        {
            Debug.LogError(
                "BreachSpawner: Breach Pool returned NULL!"
            );

            return;
        }

        breachObject.transform.position =
            spawnPosition;

        breachObject.transform.rotation =
            Quaternion.identity;

        Breach breach =
            breachObject.GetComponent<Breach>();

        if (breach == null)
        {
            Debug.LogError(
                "BreachSpawner: Breach prefab has no Breach component!"
            );

            return;
        }

        breach.SetScoreManager(
            scoreManager
        );

        breach.SetRoundManager(
            roundManager
        );
    }

    // ==================================================
    // DIFFICULTY
    // ==================================================

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
}