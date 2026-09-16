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

    private void Update()
    {
        if (boostTimer > 0f)
            boostTimer -= Time.deltaTime;

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (roleManager.Player1Role != RoleManager.Role.Pilot)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0f)
        {
            ActivateBoost();
        }
    }

    private void ActivateBoost()
    {
        boostTimer = boostDuration;
        cooldownTimer = boostCooldown;

        shipController.SetSpeedMultiplier(boostMultiplier);

        Invoke(nameof(StopBoost), boostDuration);

        Debug.Log("BOOST ACTIVATED!");
    }

    private void StopBoost()
    {
        shipController.SetSpeedMultiplier(1f);

        Debug.Log("BOOST ENDED!");
    }
    public void CancelBoost()
    {
        boostTimer = 0f;
        CancelInvoke(nameof(StopBoost));

        shipController.SetSpeedMultiplier(1f);

        Debug.Log("Boost Cancelled!");
    }
}