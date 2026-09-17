using UnityEngine;

public class MobileRoleControls : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoleManager roleManager;

    [Header("Player 1 - Left Side")]
    [SerializeField] private GameObject player1PilotControls;
    [SerializeField] private GameObject player1GunnerControls;

    [Header("Player 2 - Right Side")]
    [SerializeField] private GameObject player2PilotControls;
    [SerializeField] private GameObject player2GunnerControls;

    private void Start()
    {
        if (roleManager == null)
        {
            Debug.LogError(
                "MobileRoleControls: RoleManager is not assigned!"
            );

            return;
        }

        roleManager.OnRolesChanged += HandleRolesChanged;

        // Apply the current roles immediately.
        UpdateControls(
            roleManager.Player1Role,
            roleManager.Player2Role
        );
    }

    private void OnDestroy()
    {
        if (roleManager != null)
        {
            roleManager.OnRolesChanged -= HandleRolesChanged;
        }
    }

    private void HandleRolesChanged(
        RoleManager.Role player1Role,
        RoleManager.Role player2Role)
    {
        UpdateControls(
            player1Role,
            player2Role
        );
    }

    private void UpdateControls(
        RoleManager.Role player1Role,
        RoleManager.Role player2Role)
    {
        // ==================================================
        // PLAYER 1
        // ==================================================

        if (player1PilotControls != null)
        {
            player1PilotControls.SetActive(
                player1Role == RoleManager.Role.Pilot
            );
        }

        if (player1GunnerControls != null)
        {
            player1GunnerControls.SetActive(
                player1Role == RoleManager.Role.Gunner
            );
        }

        // ==================================================
        // PLAYER 2
        // ==================================================

        if (player2PilotControls != null)
        {
            player2PilotControls.SetActive(
                player2Role == RoleManager.Role.Pilot
            );
        }

        if (player2GunnerControls != null)
        {
            player2GunnerControls.SetActive(
                player2Role == RoleManager.Role.Gunner
            );
        }
    }
}