using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float leftLimit = -8f;
    [SerializeField] private float rightLimit = 8f;

    [Header("References")]
    [SerializeField] private RoleManager roleManager;

    private float speedMultiplier = 1f;

    private void Update()
    {
        if (roleManager == null)
            return;

        // Find the player who is currently the Pilot.
        int pilotPlayer = GetCurrentPilot();

        if (pilotPlayer == -1)
            return;

        float input = GetMovementInput(pilotPlayer);

        float movement =
            input *
            moveSpeed *
            speedMultiplier *
            Time.deltaTime;

        transform.position += Vector3.right * movement;

        float x = Mathf.Clamp(
            transform.position.x,
            leftLimit,
            rightLimit
        );

        transform.position = new Vector3(
            x,
            transform.position.y,
            transform.position.z
        );
    }

    private int GetCurrentPilot()
    {
        if (roleManager.Player1Role == RoleManager.Role.Pilot)
            return 1;

        if (roleManager.Player2Role == RoleManager.Role.Pilot)
            return 2;

        return -1;
    }

    private float GetMovementInput(int pilotPlayer)
    {
        float keyboardInput = Input.GetAxisRaw("Horizontal");

        float mobileInput = 0f;

        if (MobileInputController.Instance != null)
        {
            mobileInput =
                MobileInputController.Instance.GetMovementInput(
                    pilotPlayer
                );
        }

        // Mobile input takes priority when it is being used.
        if (Mathf.Abs(mobileInput) > 0f)
            return mobileInput;

        return keyboardInput;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}