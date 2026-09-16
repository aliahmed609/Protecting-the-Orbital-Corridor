using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHull = 3;
    [SerializeField] private float invulnerabilityTime = 1f;
    [SerializeField] private PlayerShield playerShield;
    [SerializeField] private RoundManager roundManager;

    private int currentHull;
    public int MaxHull => maxHull;
    private float invulnerabilityTimer;

    public int CurrentHull => currentHull;

    private void Start()
    {
        currentHull = maxHull;
    }

    private void Update()
    {
        if (invulnerabilityTimer > 0f)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerShield != null && playerShield.IsShieldActive)
            return;

        if (invulnerabilityTimer > 0f)
            return;

        currentHull -= damage;
        invulnerabilityTimer = invulnerabilityTime;

        Debug.Log("Ship Hull: " + currentHull);

        if (currentHull <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("SHIP DESTROYED!");

        roundManager.LoseRound();
    }
}