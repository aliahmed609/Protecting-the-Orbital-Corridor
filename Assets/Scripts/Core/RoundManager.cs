using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public enum GamePhase
    {
        Patrol,
        Alert,
        Critical
    }

    [Header("Round")]
    [SerializeField] private float roundDuration = 60f;

    [Header("Phase Timings")]
    [SerializeField] private float alertTime = 20f;
    [SerializeField] private float criticalTime = 40f;

    [Header("References")]
    [SerializeField] private PlayerBoost playerBoost;
    [SerializeField] private PlayerShield playerShield;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private MobileInputController mobileInputController;

    [Header("Flux Warning")]
    [SerializeField] private float warningTime = 3f;

    [Header("Role Swap UI")]
    [SerializeField] private RawImage roleSwappedImage;
    [SerializeField] private float roleSwappedDisplayDuration = 1f;

    private int alertWarningStep;
    private int criticalWarningStep;

    public System.Action<string> OnFluxWarning;
    public System.Action OnRoundWon;
    public System.Action OnRoundLost;

    private float currentTime;
    private GamePhase currentPhase;

    private bool roundActive;
    private bool roundEnded;

    public float CurrentTime => currentTime;
    public GamePhase CurrentPhase => currentPhase;
    public bool IsRoundActive => roundActive;
    public bool IsRoundEnded => roundEnded;

    public System.Action<GamePhase> OnPhaseChanged;

    private void Start()
    {
        if (roleSwappedImage != null)
        {
            roleSwappedImage.gameObject.SetActive(false);
        }

        StartRound();
    }

    private void Update()
    {
        if (!roundActive)
            return;

        currentTime += Time.deltaTime;

        CheckFluxWarning();
        CheckPhaseTransition();
        CheckRoundEnd();
    }

    private void StartRound()
    {
        alertWarningStep = 0;
        criticalWarningStep = 0;

        currentTime = 0f;
        currentPhase = GamePhase.Patrol;

        roundActive = true;
        roundEnded = false;

        Debug.Log("Round Started");
    }

    private void CheckPhaseTransition()
    {
        if (currentPhase == GamePhase.Patrol &&
            currentTime >= alertTime)
        {
            EnterPhase(GamePhase.Alert);
        }
        else if (currentPhase == GamePhase.Alert &&
                 currentTime >= criticalTime)
        {
            EnterPhase(GamePhase.Critical);
        }
    }

    private void EnterPhase(GamePhase newPhase)
    {
        // ==================================================
        // ROLE SWAP UI
        // ==================================================

        ShowRoleSwappedImage();

        // ==================================================
        // CANCEL ACTIVE INPUT
        // ==================================================

        if (mobileInputController != null)
        {
            mobileInputController.CancelAllInput();
        }

        // ==================================================
        // CANCEL ACTIVE ABILITIES
        // ==================================================

        if (playerBoost != null)
        {
            playerBoost.CancelBoost();
        }

        if (playerShield != null)
        {
            playerShield.CancelShield();
        }

        if (playerWeapon != null)
        {
            playerWeapon.CancelFiring();
        }

        // ==================================================
        // CHANGE PHASE
        // ==================================================

        currentPhase = newPhase;

        Debug.Log(
            "QUANTUM FLUX! New Phase: " +
            currentPhase
        );

        // RoleManager receives this event and swaps
        // Player 1 / Player 2 responsibilities.
        OnPhaseChanged?.Invoke(newPhase);
    }

    // ==================================================
    // ROLE SWAP IMAGE
    // ==================================================

    private void ShowRoleSwappedImage()
    {
        if (roleSwappedImage == null)
            return;

        StopCoroutine(nameof(HideRoleSwappedImage));

        roleSwappedImage.gameObject.SetActive(true);

        StartCoroutine(
            HideRoleSwappedImage()
        );
    }

    private IEnumerator HideRoleSwappedImage()
    {
        yield return new WaitForSeconds(
            roleSwappedDisplayDuration
        );

        if (roleSwappedImage != null)
        {
            roleSwappedImage.gameObject.SetActive(false);
        }
    }

    // ==================================================
    // ROUND END
    // ==================================================

    private void CheckRoundEnd()
    {
        if (currentTime >= roundDuration)
        {
            WinRound();
        }
    }

    public void WinRound()
    {
        if (roundEnded)
            return;

        roundEnded = true;
        roundActive = false;

        Debug.Log("ROUND WON!");

        OnRoundWon?.Invoke();
    }

    public void LoseRound()
    {
        if (roundEnded)
            return;

        roundEnded = true;
        roundActive = false;

        Debug.Log("ROUND LOST!");

        OnRoundLost?.Invoke();
    }

    // ==================================================
    // FLUX WARNING
    // ==================================================

    private void CheckFluxWarning()
    {
        if (currentPhase == GamePhase.Patrol)
        {
            float remaining =
                alertTime - currentTime;

            if (remaining <= warningTime &&
                remaining > 2f &&
                alertWarningStep < 1)
            {
                alertWarningStep = 1;

                OnFluxWarning?.Invoke(
                    "QUANTUM FLUX IN 3..."
                );
            }
            else if (remaining <= 2f &&
                     remaining > 1f &&
                     alertWarningStep < 2)
            {
                alertWarningStep = 2;

                OnFluxWarning?.Invoke(
                    "QUANTUM FLUX IN 2..."
                );
            }
            else if (remaining <= 1f &&
                     remaining > 0f &&
                     alertWarningStep < 3)
            {
                alertWarningStep = 3;

                OnFluxWarning?.Invoke(
                    "QUANTUM FLUX IN 1..."
                );
            }
        }

        if (currentPhase == GamePhase.Alert)
        {
            float remaining =
                criticalTime - currentTime;

            if (remaining <= warningTime &&
                remaining > 2f &&
                criticalWarningStep < 1)
            {
                criticalWarningStep = 1;

                OnFluxWarning?.Invoke(
                    "QUANTUM FLUX IN 3..."
                );
            }
            else if (remaining <= 2f &&
                     remaining > 1f &&
                     criticalWarningStep < 2)
            {
                criticalWarningStep = 2;

                OnFluxWarning?.Invoke(
                    "QUANTUM FLUX IN 2..."
                );
            }
            else if (remaining <= 1f &&
                     remaining > 0f &&
                     criticalWarningStep < 3)
            {
                criticalWarningStep = 3;

                OnFluxWarning?.Invoke(
                    "QUANTUM FLUX IN 1..."
                );
            }
        }
    }
}