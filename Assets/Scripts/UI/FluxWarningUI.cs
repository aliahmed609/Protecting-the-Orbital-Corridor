using TMPro;
using UnityEngine;

public class FluxWarningUI : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;

    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TMP_Text warningText;

    private void Start()
    {
        if (warningPanel != null)
            warningPanel.SetActive(false);

        if (roundManager != null)
        {
            roundManager.OnFluxWarning += ShowWarning;
        }
    }

    private void OnDestroy()
    {
        if (roundManager != null)
        {
            roundManager.OnFluxWarning -= ShowWarning;
        }
    }

    private void ShowWarning(string message)
    {
        if (warningText == null || warningPanel == null)
            return;

        warningText.text = message;
        warningPanel.SetActive(true);

        CancelInvoke(nameof(HideWarning));
        Invoke(nameof(HideWarning), 2f);
    }

    private void HideWarning()
    {
        warningPanel.SetActive(false);
    }
}