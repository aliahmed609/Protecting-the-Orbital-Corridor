using UnityEngine;

public class Breach : MonoBehaviour, IPoolable
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float bottomLimit = -7f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int scoreValue = 25;

    private int currentHealth;

    private float baseMoveSpeed;

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
            if (roundManager != null)
            {
                roundManager.LoseRound();
            }

            ReturnToPool();
        }
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
            "Breach HP = " +
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
                "Breach has no PoolReference!"
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