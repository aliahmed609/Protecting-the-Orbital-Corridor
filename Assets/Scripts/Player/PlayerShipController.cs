using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float leftLimit = -8f;
    [SerializeField] private float rightLimit = 8f;
    private float speedMultiplier = 1f;

    [SerializeField] private RoleManager roleManager;

    private void Update()
    {
        if (roleManager.Player1Role != RoleManager.Role.Pilot)
            return;

        float input = Input.GetAxisRaw("Horizontal");

        float movement = input * moveSpeed * speedMultiplier * Time.deltaTime;

        transform.position += Vector3.right * movement;

        float x = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);

        transform.position = new Vector3(
            x,
            transform.position.y,
            transform.position.z
        );
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}