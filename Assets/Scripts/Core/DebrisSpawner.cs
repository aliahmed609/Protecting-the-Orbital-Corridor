using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ObjectPool debrisPool;

    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnXMin = -8f;
    [SerializeField] private float spawnXMax = 8f;
    [SerializeField] private float spawnCheckRadius = 0.8f;
    [SerializeField] private LayerMask obstacleLayer;

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }
    private void Update()
    {
        if (roundManager == null)
            return;

        if (!roundManager.IsRoundActive)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnDebris();

            nextSpawnTime =
                Time.time + spawnInterval;
        }
    }

    // ==================================================
    // SPAWN
    // ==================================================

    private void SpawnDebris()
    {
        if (debrisPool == null)
        {
            Debug.LogError(
                "DebrisSpawner: Debris Pool is not assigned!"
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

        GameObject debrisObject =
            debrisPool.Get();

        if (debrisObject == null)
        {
            Debug.LogError(
                "DebrisSpawner: Debris Pool returned NULL!"
            );

            return;
        }

        debrisObject.transform.position =
            spawnPosition;

        debrisObject.transform.rotation =
            Quaternion.identity;

        Debris debris =
            debrisObject.GetComponent<Debris>();

        if (debris == null)
        {
            Debug.LogError(
                "DebrisSpawner: Debris prefab has no Debris component!"
            );

            return;
        }

        debris.SetScoreManager(
            scoreManager
        );
    }
}