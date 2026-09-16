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
    public float ShieldCooldownRemaining => Mathf.Max(cooldownTimer, 0f);

    private void Update()
    {
        if (shieldTimer > 0f)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer <= 0f)
            {
                StopShield();
            }
        }

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (roleManager == null)
            return;

        int gunnerPlayer = GetCurrentGunner();

        if (gunnerPlayer == -1)
            return;

        if (GetShieldInput(gunnerPlayer) && cooldownTimer <= 0f)
        {
            ActivateShield();
        }

        UpdateShieldVisual();
    }

    private int GetCurrentGunner()
    {
        if (roleManager.Player1Role == RoleManager.Role.Gunner)
            return 1;

        if (roleManager.Player2Role == RoleManager.Role.Gunner)
            return 2;

        return -1;
    }

    private bool GetShieldInput(int gunnerPlayer)
    {
        // Mobile input
        if (MobileInputController.Instance != null)
        {
            if (MobileInputController.Instance.GetShieldInput(gunnerPlayer))
                return true;
        }

        // Keyboard/mouse development input
        return Input.GetMouseButtonDown(1);
    }

    private void ActivateShield()
    {
        shieldTimer = shieldDuration;
        cooldownTimer = shieldCooldown;

        Debug.Log("SHIELD ACTIVATED!");
    }

    private void StopShield()
    {
        shieldTimer = 0f;

        Debug.Log("SHIELD ENDED!");
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