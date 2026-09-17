using TMPro;
using UnityEngine;

public class AbilitiesUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoleManager roleManager;
    [SerializeField] private PlayerBoost playerBoost;
    [SerializeField] private PlayerShield playerShield;

    [Header("Player 1 UI")]
    [SerializeField] private GameObject player1BoostLogo;
    [SerializeField] private GameObject player1ShieldLogo;
    [SerializeField] private TMP_Text player1BoostTimer;
    [SerializeField] private TMP_Text player1ShieldTimer;

    [Header("Player 2 UI")]
    [SerializeField] private GameObject player2BoostLogo;
    [SerializeField] private GameObject player2ShieldLogo;
    [SerializeField] private TMP_Text player2BoostTimer;
    [SerializeField] private TMP_Text player2ShieldTimer;

    private void Update()
    {
        if (roleManager == null)
            return;

        UpdateAbilityUI();
    }

    private void UpdateAbilityUI()
    {
        bool player1IsPilot =
            roleManager.Player1Role == RoleManager.Role.Pilot;

        bool player2IsPilot =
            roleManager.Player2Role == RoleManager.Role.Pilot;

        bool boostActive =
            playerBoost != null &&
            playerBoost.IsBoostActive;

        bool shieldActive =
            playerShield != null &&
            playerShield.IsShieldActive;

        // --------------------------------------------------
        // PLAYER 1
        // --------------------------------------------------

        bool player1BoostActive =
            player1IsPilot && boostActive;

        bool player1ShieldActive =
            !player1IsPilot && shieldActive;

        if (player1BoostLogo != null)
            player1BoostLogo.SetActive(player1BoostActive);

        if (player1ShieldLogo != null)
            player1ShieldLogo.SetActive(player1ShieldActive);

        if (player1BoostTimer != null)
        {
            player1BoostTimer.gameObject.SetActive(player1BoostActive);

            if (player1BoostActive)
            {
                player1BoostTimer.text =
                    playerBoost.BoostTimeRemaining.ToString("0.0");
            }
        }

        if (player1ShieldTimer != null)
        {
            player1ShieldTimer.gameObject.SetActive(player1ShieldActive);

            if (player1ShieldActive)
            {
                player1ShieldTimer.text =
                    playerShield.ShieldTimeRemaining.ToString("0.0");
            }
        }

        // --------------------------------------------------
        // PLAYER 2
        // --------------------------------------------------

        bool player2BoostActive =
            player2IsPilot && boostActive;

        bool player2ShieldActive =
            !player2IsPilot && shieldActive;

        if (player2BoostLogo != null)
            player2BoostLogo.SetActive(player2BoostActive);

        if (player2ShieldLogo != null)
            player2ShieldLogo.SetActive(player2ShieldActive);

        if (player2BoostTimer != null)
        {
            player2BoostTimer.gameObject.SetActive(player2BoostActive);

            if (player2BoostActive)
            {
                player2BoostTimer.text =
                    playerBoost.BoostTimeRemaining.ToString("0.0");
            }
        }

        if (player2ShieldTimer != null)
        {
            player2ShieldTimer.gameObject.SetActive(player2ShieldActive);

            if (player2ShieldActive)
            {
                player2ShieldTimer.text =
                    playerShield.ShieldTimeRemaining.ToString("0.0");
            }
        }
    }
}