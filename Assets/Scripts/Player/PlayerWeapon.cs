using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoleManager roleManager;
    [SerializeField] private Transform reticle;
    [SerializeField] private ObjectPool laserPool;

    [Header("Weapon")]
    [SerializeField] private float fireCooldown = 0.25f;

    private float nextFireTime;

    private void Update()
    {
        if (roleManager == null)
            return;

        int gunnerPlayer = GetCurrentGunner();

        if (gunnerPlayer == -1)
            return;

        // Mobile Fire Button only.
        if (MobileInputController.Instance == null)
            return;

        if (!MobileInputController.Instance.GetFireInput(gunnerPlayer))
            return;

        if (Time.time < nextFireTime)
            return;

        Fire(gunnerPlayer);

        nextFireTime = Time.time + fireCooldown;
    }

    private int GetCurrentGunner()
    {
        if (roleManager.Player1Role == RoleManager.Role.Gunner)
            return 1;

        if (roleManager.Player2Role == RoleManager.Role.Gunner)
            return 2;

        return -1;
    }

    private void Fire(int gunnerPlayer)
    {
        if (laserPool == null)
            return;

        GameObject laser = laserPool.Get();

        if (laser == null)
            return;

        laser.transform.position = transform.position;

        bool aimActive =
            MobileInputController.Instance.GetAimInput(
                gunnerPlayer,
                out _
            );

        Vector2 direction;

        // AimPad + Fire → shoot toward reticle.
        if (aimActive && reticle != null)
        {
            direction =
                reticle.position -
                transform.position;
        }
        else
        {
            // Fire only → shoot forward.
            direction = transform.up;
        }

        if (direction.sqrMagnitude <= 0.001f)
            direction = transform.up;

        direction.Normalize();

        float angle =
            Mathf.Atan2(
                direction.y,
                direction.x
            ) * Mathf.Rad2Deg - 90f;

        laser.transform.rotation =
            Quaternion.Euler(
                0f,
                0f,
                angle
            );

        PlayerLaser playerLaser =
            laser.GetComponent<PlayerLaser>();

        if (playerLaser != null)
        {
            playerLaser.SetDirection(direction);
        }
    }

    public void CancelFiring()
    {
        nextFireTime = Time.time + fireCooldown;
    }
}