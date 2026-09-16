using UnityEngine;
using TMPro;
using System.Collections;

public class FluxUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private GameObject fluxBanner;
    [SerializeField] private TMP_Text fluxText;

    [Header("Display")]
    [SerializeField] private float transitionMessageDuration = 2f;

    private Coroutine bannerCoroutine;

    private void Start()
    {
        if (roundManager == null)
            return;

        roundManager.OnFluxWarning += ShowFluxWarning;
        roundManager.OnPhaseChanged += ShowRoleReversal;
    }

    private void OnDestroy()
    {
        if (roundManager == null)
            return;

        roundManager.OnFluxWarning -= ShowFluxWarning;
        roundManager.OnPhaseChanged -= ShowRoleReversal;
    }

    private void ShowFluxWarning(string message)
    {
        ShowMessage(message, 1f);
    }

    private void ShowRoleReversal(RoundManager.GamePhase phase)
    {
        ShowMessage(
            "QUANTUM FLUX ROLES REVERSED",
            transitionMessageDuration
        );
    }

    private void ShowMessage(string message, float duration)
    {
        if (fluxBanner == null || fluxText == null)
            return;

        if (bannerCoroutine != null)
            StopCoroutine(bannerCoroutine);

        bannerCoroutine = StartCoroutine(
            DisplayMessage(message, duration)
        );
    }

    private IEnumerator DisplayMessage(
        string message,
        float duration)
    {
        fluxBanner.SetActive(true);

        fluxText.text = message;

        yield return new WaitForSeconds(duration);

        fluxBanner.SetActive(false);

        bannerCoroutine = null;
    }
}