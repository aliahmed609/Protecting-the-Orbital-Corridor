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

        bool fireInput = GetFireInput(gunnerPlayer);

        if (fireInput && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    private int GetCurrentGunner()
    {
        if (roleManager.Player1Role == RoleManager.Role.Gunner)
            return 1;

        if (roleManager.Player2Role == RoleManager.Role.Gunner)
            return 2;

        return -1;
    }

    private bool GetFireInput(int gunnerPlayer)
    {
        if (MobileInputController.Instance != null)
        {
            if (MobileInputController.Instance.GetFireInput(gunnerPlayer))
                return true;
        }

        // Development keyboard/mouse input.
        return Input.GetMouseButton(0);
    }

    private void Fire()
    {
        if (laserPool == null || reticle == null)
            return;

        GameObject laser = laserPool.Get();

        if (laser == null)
            return;

        laser.transform.position = transform.position;
        laser.transform.rotation = Quaternion.identity;

        Vector2 direction = reticle.position - transform.position;

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