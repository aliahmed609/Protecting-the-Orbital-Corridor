using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float bottomLimit = -7f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int scoreValue = 10;

    [Header("Shooting")]
    [SerializeField] private float fireInterval = 2f;
    [SerializeField] private float projectileSpeed = 5f;

    [Header("References")]
    [SerializeField] private ObjectPool projectilePool;

    private int currentHealth;

    private float baseMoveSpeed;
    private float nextFireTime;

    private ScoreManager scoreManager;
    private RoundManager roundManager;

    private bool isReturning;

    // ==================================================
    // UNITY
    // ==================================================

    private void Awake()
    {
        baseMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (isReturning)
            return;

        if (roundManager != null &&
            !roundManager.IsRoundActive)
        {
            return;
        }

        Move();
        Shoot();
        CheckOffScreen();
    }

    // ==================================================
    // MOVEMENT
    // ==================================================

    private void Move()
    {
        transform.position +=
            Vector3.down *
            moveSpeed *
            Time.deltaTime;
    }

    private void CheckOffScreen()
    {
        if (transform.position.y <= bottomLimit)
        {
            ReturnToPool();
        }
    }

    // ==================================================
    // SHOOTING
    // ==================================================

    private void Shoot()
    {
        if (projectilePool == null)
            return;

        if (Time.time < nextFireTime)
            return;

        GameObject projectile =
            projectilePool.Get();

        if (projectile == null)
        {
            Debug.LogError(
                "Enemy: Projectile Pool returned NULL!"
            );

            return;
        }

        projectile.transform.position =
            transform.position;

        projectile.transform.rotation =
            Quaternion.identity;

        EnemyProjectile projectileScript =
            projectile.GetComponent<EnemyProjectile>();

        if (projectileScript != null)
        {
            float finalSpeed =
                projectileSpeed;

            if (roundManager != null &&
                roundManager.CurrentPhase ==
                RoundManager.GamePhase.Critical)
            {
                finalSpeed *= 1.5f;
            }

            projectileScript.SetSpeed(
                finalSpeed
            );
        }
        else
        {
            Debug.LogError(
                "Enemy Projectile prefab has no EnemyProjectile component!"
            );
        }

        nextFireTime =
            Time.time + fireInterval;
    }

    // ==================================================
    // SETTERS
    // ==================================================

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void SetScoreManager(
        ScoreManager manager)
    {
        scoreManager = manager;
    }

    public void SetRoundManager(
        RoundManager manager)
    {
        roundManager = manager;
    }

    public void SetProjectilePool(
        ObjectPool pool)
    {
        projectilePool = pool;
    }

    // ==================================================
    // DAMAGE
    // ==================================================

    public void TakeDamage(int damage)
    {
        if (isReturning)
            return;

        if (damage <= 0)
            return;

        if (currentHealth <= 0)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isReturning)
            return;

        if (scoreManager != null)
        {
            scoreManager.AddScore(
                scoreValue
            );
        }

        ReturnToPool();
    }

    // ==================================================
    // PLAYER COLLISION
    // ==================================================

    private void OnCollisionEnter2D(
        Collision2D collision)
    {
        if (isReturning)
            return;

        PlayerHealth player =
            collision.gameObject
                .GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(1);

            ReturnToPool();
        }
    }

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

        PoolReference reference =
            GetComponent<PoolReference>();

        if (reference != null)
        {
            reference.ReturnToPool();
        }
        else
        {
            Debug.LogError(
                "Enemy has no PoolReference!"
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

        currentHealth =
            maxHealth;

        moveSpeed =
            baseMoveSpeed;

        nextFireTime =
            Time.time + fireInterval;
    }

    public void OnDespawned()
    {
        isReturning = true;

        currentHealth = 0;

        moveSpeed =
            baseMoveSpeed;

        nextFireTime = 0f;
    }
}