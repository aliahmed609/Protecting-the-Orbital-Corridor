using UnityEngine;

public class EnemyProjectile : MonoBehaviour, IPoolable
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;

    [Header("Cleanup")]
    [SerializeField] private float bottomLimit = -7f;

    private float currentSpeed;
    private bool isReturning;

    // ==================================================
    // UPDATE
    // ==================================================

    private void Update()
    {
        if (isReturning)
            return;

        transform.position +=
            Vector3.down *
            currentSpeed *
            Time.deltaTime;

        if (transform.position.y <= bottomLimit)
        {
            ReturnToPool();
        }
    }

    // ==================================================
    // SETTERS
    // ==================================================

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    // ==================================================
    // PLAYER COLLISION
    // ==================================================

    private void OnTriggerEnter2D(
        Collider2D other)
    {
        if (isReturning)
            return;

        PlayerHealth player =
            other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(1);

            ReturnToPool();
        }
    }

    // ==================================================
    // RETURN TO POOL
    // ==================================================

    private void ReturnToPool()
    {
        if (isReturning)
            return;

        isReturning = true;

        PoolReference poolReference =
            GetComponent<PoolReference>();

        if (poolReference != null)
        {
            poolReference.ReturnToPool();
        }
        else
        {
            Debug.LogError(
                "EnemyProjectile has no PoolReference!"
            );

            gameObject.SetActive(false);
        }
    }

    // ==================================================
    // POOL LIFECYCLE
    // ==================================================

    public void OnSpawned()
    {
        isReturning = false;

        currentSpeed = speed;
    }

    public void OnDespawned()
    {
        isReturning = true;

        currentSpeed = 0f;
    }
}