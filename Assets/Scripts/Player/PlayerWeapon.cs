using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private RoleManager roleManager;
    [SerializeField] private Transform reticle;
    [SerializeField] private GameObject laserPrefab;

    [SerializeField] private float fireCooldown = 0.25f;

    private float nextFireTime;

    private void Update()
    {
        if (roleManager.Player2Role != RoleManager.Role.Gunner)
            return;

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            laserPrefab,
            transform.position,
            Quaternion.identity
        );

        Vector2 direction = reticle.position - transform.position;

        laser.GetComponent<PlayerLaser>().SetDirection(direction);
    }
    public void CancelFiring()
    {
        nextFireTime = Time.time + fireCooldown;
    }
}