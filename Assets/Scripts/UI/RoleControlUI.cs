using UnityEngine;

public class RoleControlUI : MonoBehaviour
{
    [Header("Role Manager")]
    [SerializeField] private RoleManager roleManager;

    [Header("Player Panels")]
    [SerializeField] private PlayerPanelUI leftPanel;
    [SerializeField] private PlayerPanelUI rightPanel;

    private void Start()
    {
        if (roleManager == null)
        {
            Debug.LogError("RoleControlUI: RoleManager is not assigned.");
            return;
        }

        if (leftPanel == null || rightPanel == null)
        {
            Debug.LogError("RoleControlUI: Both player panels must be assigned.");
            return;
        }

        roleManager.OnRolesChanged += HandleRolesChanged;

        // Apply the current roles immediately.
        UpdatePanels(
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
        UpdatePanels(player1Role, player2Role);
    }

    private void UpdatePanels(
        RoleManager.Role player1Role,
        RoleManager.Role player2Role)
    {
        // IMPORTANT:
        // The panel positions don't determine the player.
        //
        // Player 1 / Player 2 can occupy either panel.
        //
        // We currently start with:
        // Player 1 = Right
        // Player 2 = Left

        leftPanel.Setup(2, player2Role);
        rightPanel.Setup(1, player1Role);
    }
}