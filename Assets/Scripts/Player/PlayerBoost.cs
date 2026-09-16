using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    [Header("Boost")]
    [SerializeField] private float boostDuration = 1f;
    [SerializeField] private float boostMultiplier = 1.75f;
    [SerializeField] private float boostCooldown = 4f;

    [Header("References")]
    [SerializeField] private RoleManager roleManager;
    [SerializeField] private PlayerShipController shipController;

    private float boostTimer;
    private float cooldownTimer;

    public bool IsBoostActive => boostTimer > 0f;
    public float BoostTimeRemaining => Mathf.Max(boostTimer, 0f);
    public float BoostCooldownRemaining => Mathf.Max(cooldownTimer, 0f);

    private void Update()
    {
        if (boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0f)
            {
                StopBoost();
            }
        }

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (roleManager == null)
            return;

        int pilotPlayer = GetCurrentPilot();

        if (pilotPlayer == -1)
            return;

        if (GetBoostInput(pilotPlayer) && cooldownTimer <= 0f)
        {
            ActivateBoost();
        }
    }

    private int GetCurrentPilot()
    {
        if (roleManager.Player1Role == RoleManager.Role.Pilot)
            return 1;

        if (roleManager.Player2Role == RoleManager.Role.Pilot)
            return 2;

        return -1;
    }

    private bool GetBoostInput(int pilotPlayer)
    {
        // Mobile input
        if (MobileInputController.Instance != null)
        {
            if (MobileInputController.Instance.GetBoostInput(pilotPlayer))
                return true;
        }

        // Keyboard development input
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void ActivateBoost()
    {
        boostTimer = boostDuration;
        cooldownTimer = boostCooldown;

        if (shipController != null)
        {
            shipController.SetSpeedMultiplier(boostMultiplier);
        }

        Debug.Log("BOOST ACTIVATED!");
    }

    private void StopBoost()
    {
        boostTimer = 0f;

        if (shipController != null)
        {
            shipController.SetSpeedMultiplier(1f);
        }

        Debug.Log("BOOST ENDED!");
    }

    public void CancelBoost()
    {
        boostTimer = 0f;

        StopBoost();

        Debug.Log("Boost Cancelled!");
    }
}