using UnityEngine;

public class GunnerAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoleManager roleManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform reticle;

    [Header("Aim")]
    [SerializeField] private float worldSensitivity = 1f;

    private void Update()
    {
        if (roleManager == null ||
            mainCamera == null ||
            reticle == null)
        {
            return;
        }

        int gunnerPlayer = GetCurrentGunner();

        if (gunnerPlayer == -1)
        {
            HideReticle();
            return;
        }

        // ==================================================
        // MOBILE AIM PAD
        // ==================================================

        if (MobileInputController.Instance == null)
        {
            HideReticle();
            return;
        }

        bool aimActive =
            MobileInputController.Instance.GetAimInput(
                gunnerPlayer,
                out _
            );

        // ==================================================
        // AIM PAD ACTIVE
        // ==================================================

        if (aimActive)
        {
            reticle.gameObject.SetActive(true);

            Vector2 aimDelta =
                MobileInputController.Instance.GetAimDelta(
                    gunnerPlayer
                );

            if (aimDelta != Vector2.zero)
            {
                MoveReticle(aimDelta);
            }

            return;
        }

        // ==================================================
        // AIM PAD NOT ACTIVE
        // ==================================================

        HideReticle();
    }

    // ==================================================
    // CURRENT GUNNER
    // ==================================================

    private int GetCurrentGunner()
    {
        if (roleManager.Player1Role == RoleManager.Role.Gunner)
            return 1;

        if (roleManager.Player2Role == RoleManager.Role.Gunner)
            return 2;

        return -1;
    }

    // ==================================================
    // MOVE RETICLE
    // ==================================================

    private void MoveReticle(Vector2 screenDelta)
    {
        float distance =
            Mathf.Abs(
                transform.position.z -
                mainCamera.transform.position.z
            );

        Vector3 worldBefore =
            mainCamera.ScreenToWorldPoint(
                new Vector3(
                    0f,
                    0f,
                    distance
                )
            );

        Vector3 worldAfter =
            mainCamera.ScreenToWorldPoint(
                new Vector3(
                    screenDelta.x,
                    screenDelta.y,
                    distance
                )
            );

        Vector3 worldDelta =
            (worldAfter - worldBefore) *
            worldSensitivity;

        worldDelta.z = 0f;

        reticle.position += worldDelta;
    }

    // ==================================================
    // HIDE RETICLE
    // ==================================================

    private void HideReticle()
    {
        if (reticle != null)
        {
            reticle.gameObject.SetActive(false);
        }
    }

    // ==================================================
    // RESET
    // ==================================================

    public void ResetAim()
    {
        HideReticle();
    }
}