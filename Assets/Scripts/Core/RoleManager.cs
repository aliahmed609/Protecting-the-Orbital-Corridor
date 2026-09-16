using UnityEngine;

public class RoleManager : MonoBehaviour
{
    public enum Role
    {
        Pilot,
        Gunner
    }

    [Header("Starting Roles")]
    [SerializeField] private Role player1Role = Role.Pilot;
    [SerializeField] private Role player2Role = Role.Gunner;

    [Header("References")]
    [SerializeField] private RoundManager roundManager;

    public Role Player1Role => player1Role;
    public Role Player2Role => player2Role;

    private void Start()
    {
        if (roundManager != null)
        {
            roundManager.OnPhaseChanged += HandlePhaseChanged;
        }
    }

    private void OnDestroy()
    {
        if (roundManager != null)
        {
            roundManager.OnPhaseChanged -= HandlePhaseChanged;
        }
    }

    private void HandlePhaseChanged(RoundManager.GamePhase phase)
    {
        switch (phase)
        {
            case RoundManager.GamePhase.Patrol:

                player1Role = Role.Pilot;
                player2Role = Role.Gunner;

                break;

            case RoundManager.GamePhase.Alert:

                player1Role = Role.Gunner;
                player2Role = Role.Pilot;

                break;

            case RoundManager.GamePhase.Critical:

                player1Role = Role.Pilot;
                player2Role = Role.Gunner;

                break;
        }

        Debug.Log("QUANTUM FLUX - Roles Updated!");
        Debug.Log("Player 1 = " + player1Role);
        Debug.Log("Player 2 = " + player2Role);
    }
}