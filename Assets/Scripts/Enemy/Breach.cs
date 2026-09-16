using UnityEngine;

public class Breach : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int health = 3;
    [SerializeField] private int scoreValue = 25;

    private ScoreManager scoreManager;

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    public void SetScoreManager(ScoreManager manager)
    {
        scoreManager = manager;
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