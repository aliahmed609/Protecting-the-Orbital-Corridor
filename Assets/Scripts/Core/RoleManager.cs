using System;
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

    // Fired whenever the Player 1 / Player 2 roles change.
    public event Action<Role, Role> OnRolesChanged;

    private void Start()
    {
        if (roundManager != null)
        {
            roundManager.OnPhaseChanged += HandlePhaseChanged;
        }

        // Make sure every listener gets the starting roles.
        OnRolesChanged?.Invoke(player1Role, player2Role);
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

        // Tell UI and gameplay systems about the new assignments.
        OnRolesChanged?.Invoke(player1Role, player2Role);
    }
}