using TMPro;
using UnityEngine;

public class PlayerPanelUI : MonoBehaviour
{
    [Header("Panel Visuals")]
    [SerializeField] private GameObject redBorder;
    [SerializeField] private GameObject blueBorder;
    [SerializeField] private TMP_Text roleText;

    [Header("Role Controls")]
    [SerializeField] private GameObject pilotControls;
    [SerializeField] private GameObject gunnerControls;

    [Header("Player Colors")]
    [SerializeField] private Color player1Color = Color.blue;
    [SerializeField] private Color player2Color = Color.red;

    private int playerNumber;
    private RoleManager.Role currentRole;

    public int PlayerNumber => playerNumber;
    public RoleManager.Role CurrentRole => currentRole;

    public void Setup(int player, RoleManager.Role role)
    {
        playerNumber = player;
        currentRole = role;

        UpdatePlayerVisuals();
        UpdateRoleControls();
    }

    private void UpdatePlayerVisuals()
    {
        bool isPlayer1 = playerNumber == 1;

        // Player 1 = Blue
        if (blueBorder != null)
            blueBorder.SetActive(isPlayer1);

        // Player 2 = Red
        if (redBorder != null)
            redBorder.SetActive(!isPlayer1);

        if (roleText != null)
        {
            roleText.text =
                "PLAYER " + playerNumber + " - " + currentRole.ToString().ToUpper();

            roleText.color = isPlayer1 ? player1Color : player2Color;
        }
    }

    private void UpdateRoleControls()
    {
        bool isPilot = currentRole == RoleManager.Role.Pilot;

        if (pilotControls != null)
            pilotControls.SetActive(isPilot);

        if (gunnerControls != null)
            gunnerControls.SetActive(!isPilot);
    }
}