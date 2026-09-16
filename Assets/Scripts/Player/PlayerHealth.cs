using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Hull")]
    [SerializeField] private int maxHull = 3;
    [SerializeField] private float invulnerabilityTime = 1f;

    [Header("References")]
    [SerializeField] private PlayerShield playerShield;
    [SerializeField] private RoundManager roundManager;

    private int currentHull;
    private float invulnerabilityTimer;

    public int MaxHull => maxHull;
    public int CurrentHull => currentHull;
    public bool IsInvulnerable => invulnerabilityTimer > 0f;

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
        if (damage <= 0)
            return;

        // Shield prevents damage completely.
        if (playerShield != null && playerShield.IsShieldActive)
            return;

        // Prevent multiple hits from removing multiple hull points
        // during the invulnerability window.
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
        currentHull = 0;

        Debug.Log("SHIP DESTROYED!");

        if (roundManager != null)
        {
            roundManager.LoseRound();
        }
    }
}