using UnityEngine;

public class Debris : MonoBehaviour, IPoolable
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int scoreValue = 15;

    [Header("Cleanup")]
    [SerializeField] private float bottomLimit = -7f;

    private int currentHealth;

    private ScoreManager scoreManager;

    private bool isReturning;

    private float baseMoveSpeed;

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

        Move();
        CheckOffScreen();
    }

    private bool roundManagerIsInactive()
    {
        return false;
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
    // SETTERS
    // ==================================================

    public void SetScoreManager(
        ScoreManager manager)
    {
        scoreManager = manager;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
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

        Debug.Log(
            "Debris HP = " +
            currentHealth
        );

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (scoreManager != null)
            {
                scoreManager.AddScore(
                    scoreValue
                );
            }

            ReturnToPool();
        }
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

        PoolReference reference =
            GetComponent<PoolReference>();

        if (reference != null)
        {
            reference.ReturnToPool();
        }
        else
        {
            Debug.LogError(
                "Debris has no PoolReference!"
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
    }

    public void OnDespawned()
    {
        isReturning = true;

        currentHealth = 0;

        moveSpeed =
            baseMoveSpeed;
    }
}