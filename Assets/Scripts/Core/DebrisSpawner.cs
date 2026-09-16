using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    [SerializeField] private GameObject debrisPrefab;
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnXMin = -8f;
    [SerializeField] private float spawnXMax = 8f;
    [SerializeField] private float spawnCheckRadius = 0.8f;
    [SerializeField] private LayerMask obstacleLayer;

    private float nextSpawnTime;

    private void Update()
    {
        if (!roundManager.IsRoundActive)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnDebris();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnDebris()
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

        GameObject debris = Instantiate(
            debrisPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Debris debrisScript = debris.GetComponent<Debris>();

        if (debrisScript != null)
        {
            debrisScript.SetScoreManager(scoreManager);
        }
    }
}