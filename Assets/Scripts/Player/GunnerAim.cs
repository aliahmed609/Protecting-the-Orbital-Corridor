using UnityEngine;

public class GunnerAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoleManager roleManager;
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        if (roleManager == null || mainCamera == null)
            return;

        // Check which player is currently the Gunner.
        int gunnerPlayer = GetCurrentGunner();

        if (gunnerPlayer == -1)
            return;

        // First try mobile aim.
        if (MobileInputController.Instance != null &&
            MobileInputController.Instance.GetAimInput(
                gunnerPlayer,
                out Vector2 mobilePosition))
        {
            UpdateAimPosition(mobilePosition);
            return;
        }

        // Keyboard/mouse development control.
        UpdateMouseAim();
    }

    private int GetCurrentGunner()
    {
        if (roleManager.Player1Role == RoleManager.Role.Gunner)
            return 1;

        if (roleManager.Player2Role == RoleManager.Role.Gunner)
            return 2;

        return -1;
    }

    private void UpdateMouseAim()
    {
        Vector3 mousePosition = Input.mousePosition;

        UpdateAimPosition(mousePosition);
    }

    private void UpdateAimPosition(Vector2 screenPosition)
    {
        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(
                new Vector3(
                    screenPosition.x,
                    screenPosition.y,
                    -mainCamera.transform.position.z
                )
            );

        worldPosition.z = 0f;

        transform.position = worldPosition;
    }
}