using TMPro;
using UnityEngine;

public class AbilitiesUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerBoost playerBoost;
    [SerializeField] private PlayerShield playerShield;

    [SerializeField] private GameObject boostLogo;
    [SerializeField] private GameObject shieldLogo;

    [Header("Cooldown Text")]
    [SerializeField] private TMP_Text boostTimerText;
    [SerializeField] private TMP_Text shieldTimerText;

    private void Update()
    {
        if (playerBoost != null)
        {
            boostLogo.SetActive(playerBoost.IsBoostActive);
            boostTimerText.text = playerBoost.BoostTimeRemaining.ToString("0.0");
        }

        if (playerShield != null)
        {
            shieldLogo.SetActive(playerShield.IsShieldActive);
            shieldTimerText.text = playerShield.ShieldTimeRemaining.ToString("0.0");
        }
    }
}