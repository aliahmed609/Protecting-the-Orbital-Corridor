using UnityEngine;

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
    [SerializeField] private PlayerBoost playerBoost;
    [SerializeField] private PlayerShield playerShield;
    [SerializeField] private PlayerWeapon playerWeapon;

    [Header("Flux Warning")]
    [SerializeField] private float warningTime = 3f;

    private bool alertWarningShown;
    private bool criticalWarningShown;

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
        alertWarningShown = false;
        criticalWarningShown = false;

        currentTime = 0f;
        currentPhase = GamePhase.Patrol;

        roundActive = true;
        roundEnded = false;

        Debug.Log("Round Started");
    }

    private void CheckPhaseTransition()
    {
        if (currentPhase == GamePhase.Patrol && currentTime >= alertTime)
        {
            EnterPhase(GamePhase.Alert);
        }
        else if (currentPhase == GamePhase.Alert && currentTime >= criticalTime)
        {
            EnterPhase(GamePhase.Critical);
        }
    }

    private void EnterPhase(GamePhase newPhase)
    {
        currentPhase = newPhase;

        if (playerBoost != null)
            playerBoost.CancelBoost();

        if (playerShield != null)
            playerShield.CancelShield();

        if (playerWeapon != null)
            playerWeapon.CancelFiring();

        Debug.Log("QUANTUM FLUX! New Phase: " + currentPhase);

        OnPhaseChanged?.Invoke(newPhase);
    }

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
    private void CheckFluxWarning()
    {
        if (!alertWarningShown &&
            currentPhase == GamePhase.Patrol &&
            currentTime >= alertTime - warningTime)
        {
            alertWarningShown = true;

            Debug.Log("QUANTUM FLUX IN 3...");
            OnFluxWarning?.Invoke("QUANTUM FLUX IN 3...");
        }

        if (!criticalWarningShown &&
            currentPhase == GamePhase.Alert &&
            currentTime >= criticalTime - warningTime)
        {
            criticalWarningShown = true;

            Debug.Log("QUANTUM FLUX IN 3...");
            OnFluxWarning?.Invoke("QUANTUM FLUX IN 3...");
        }
    }
}