using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float horizontalPadding = 0.5f;

    [Header("References")]
    [SerializeField] private RoleManager roleManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PCInputController pcInputController;

    private float speedMultiplier = 1f;

    private void Update()
    {
        if (roleManager == null)
            return;

        int pilotPlayer = GetCurrentPilot();

        if (pilotPlayer == -1)
            return;

        float input = GetMovementInput(pilotPlayer);

        float movement =
            input *
            moveSpeed *
            speedMultiplier *
            Time.deltaTime;

        transform.position +=
            Vector3.right * movement;

        ClampToCameraBounds();
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
        // --------------------------------------------------
        // MOBILE INPUT
        // --------------------------------------------------

        if (MobileInputController.Instance != null)
        {
            float mobileInput =
                MobileInputController.Instance.GetMovementInput(
                    pilotPlayer
                );

            if (Mathf.Abs(mobileInput) > 0f)
                return mobileInput;
        }

        // --------------------------------------------------
        // PC INPUT
        // --------------------------------------------------

        if (pcInputController != null)
            return pcInputController.GetMovementInput();

        // Fallback.
        return Input.GetAxisRaw("Horizontal");
    }

    private void ClampToCameraBounds()
    {
        if (mainCamera == null)
            return;

        float cameraHalfWidth =
            mainCamera.orthographicSize *
            mainCamera.aspect;

        float leftLimit =
            mainCamera.transform.position.x -
            cameraHalfWidth +
            horizontalPadding;

        float rightLimit =
            mainCamera.transform.position.x +
            cameraHalfWidth -
            horizontalPadding;

        Vector3 position = transform.position;

        position.x =
            Mathf.Clamp(
                position.x,
                leftLimit,
                rightLimit
            );

        transform.position = position;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}