using UnityEngine;

public class PlayerLaser : MonoBehaviour, IPoolable
{
    [Header("Movement")]
    [SerializeField] private float speed = 12f;

    [Header("Cleanup")]
    [SerializeField] private float minX = -12f;
    [SerializeField] private float maxX = 12f;
    [SerializeField] private float minY = -7f;
    [SerializeField] private float maxY = 10f;

    private Vector2 direction;

    // --------------------------------------------------
    // DIRECTION
    // --------------------------------------------------

    public void SetDirection(Vector2 newDirection)
    {
        direction =
            newDirection.normalized;

        transform.up = direction;
    }

    // --------------------------------------------------
    // UPDATE
    // --------------------------------------------------

    private void Update()
    {
        transform.position +=
            (Vector3)(
                direction *
                speed *
                Time.deltaTime
            );

        CheckOffScreen();
    }

    // --------------------------------------------------
    // OFF SCREEN
    // --------------------------------------------------

    private void CheckOffScreen()
    {
        Vector3 position =
            transform.position;

        if (position.x < minX ||
            position.x > maxX ||
            position.y < minY ||
            position.y > maxY)
        {
            ReturnToPool();
        }
    }

    // --------------------------------------------------
    // HIT
    // --------------------------------------------------

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Laser hit: " + other.name);

        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            Debug.Log("Hit Enemy");
            enemy.TakeDamage(1);
            ReturnToPool();
            return;
        }

        Debris debris = other.GetComponentInParent<Debris>();

        if (debris != null)
        {
            Debug.Log("Hit Debris");
            debris.TakeDamage(1);
            ReturnToPool();
            return;
        }

        Breach breach = other.GetComponentInParent<Breach>();

        if (breach != null)
        {
            Debug.Log("Hit Breach");
            breach.TakeDamage(1);
            ReturnToPool();
            return;
        }
    }
    // --------------------------------------------------
    // RETURN TO POOL
    // --------------------------------------------------

    private void ReturnToPool()
    {
        PoolReference poolReference =
            GetComponent<PoolReference>();

        if (poolReference == null)
        {
            poolReference =
                GetComponentInParent<PoolReference>();
        }

        if (poolReference != null)
        {
            poolReference.ReturnToPool();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // --------------------------------------------------
    // POOL LIFECYCLE
    // --------------------------------------------------

    public void OnSpawned()
    {
        direction = Vector2.up;
    }

    public void OnDespawned()
    {
        direction = Vector2.zero;
    }
}