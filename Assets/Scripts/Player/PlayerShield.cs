using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [Header("Shield")]
    [SerializeField] private float shieldDuration = 1.5f;
    [SerializeField] private float shieldCooldown = 5f;

    [Header("References")]
    [SerializeField] private RoleManager roleManager;
    [SerializeField] private GameObject shieldVisual;

    private float shieldTimer;
    private float cooldownTimer;

    public bool IsShieldActive => shieldTimer > 0f;
    public float ShieldTimeRemaining => Mathf.Max(shieldTimer, 0f);

    private void Update()
    {
        if (shieldTimer > 0f)
            shieldTimer -= Time.deltaTime;

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (roleManager.Player2Role != RoleManager.Role.Gunner)
            return;

        if (Input.GetMouseButtonDown(1) && cooldownTimer <= 0f)
        {
            ActivateShield();
        }

        UpdateShieldVisual();
    }

    private void ActivateShield()
    {
        shieldTimer = shieldDuration;
        cooldownTimer = shieldCooldown;

        Debug.Log("Shield Activated!");
    }

    private void UpdateShieldVisual()
    {
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(IsShieldActive);
        }
    }
    public void CancelShield()
    {
        shieldTimer = 0f;

        UpdateShieldVisual();

        Debug.Log("Shield Cancelled!");
    }
}