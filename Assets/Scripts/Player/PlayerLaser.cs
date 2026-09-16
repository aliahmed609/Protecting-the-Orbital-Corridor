using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] private float speed = 12f;

    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
        transform.up = direction;
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(1);
            Destroy(gameObject);
            return;
        }

        Debris debris = other.GetComponent<Debris>();

        if (debris != null)
        {
            debris.TakeDamage(1);
            Destroy(gameObject);
        }

        Breach breach = other.GetComponent<Breach>();
        if (breach != null)
        {
            breach.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}