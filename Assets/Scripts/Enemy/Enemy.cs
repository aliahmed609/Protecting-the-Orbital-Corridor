using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Health")]
    [SerializeField] private int health = 1;
    [SerializeField] private int scoreValue = 10;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireInterval = 2f;
    [SerializeField] private float projectileSpeed = 5f;

    private ScoreManager scoreManager;
    private RoundManager roundManager;

    private float nextFireTime;

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void Shoot()
    {
        if (projectilePrefab == null)
            return;

        if (Time.time < nextFireTime)
            return;

        GameObject projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        EnemyProjectile projectileScript =
            projectile.GetComponent<EnemyProjectile>();

        if (projectileScript != null)
        {
            float speed = projectileSpeed;

            if (roundManager != null &&
                roundManager.CurrentPhase == RoundManager.GamePhase.Critical)
            {
                speed *= 1.5f;
            }

            projectileScript.SetSpeed(speed);
        }

        nextFireTime = Time.time + fireInterval;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void SetScoreManager(ScoreManager manager)
    {
        scoreManager = manager;
    }

    public void SetRoundManager(RoundManager manager)
    {
        roundManager = manager;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            scoreManager.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}