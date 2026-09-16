using System;
using UnityEngine;

public class MobileInputController : MonoBehaviour
{
    public static MobileInputController Instance { get; private set; }

    // Fired when Quantum Flux or another system cancels all active touches.
    public event Action OnInputCancelled;

    // ==================================================
    // PLAYER 1
    // ==================================================

    private bool player1Left;
    private bool player1Right;
    private bool player1Boost;
    private bool player1Fire;
    private bool player1Shield;

    private Vector2 player1AimPosition;
    private bool player1AimActive;

    // ==================================================
    // PLAYER 2
    // ==================================================

    private bool player2Left;
    private bool player2Right;
    private bool player2Boost;
    private bool player2Fire;
    private bool player2Shield;

    private Vector2 player2AimPosition;
    private bool player2AimActive;

    // ==================================================
    // UNITY
    // ==================================================

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // ==================================================
    // PLAYER 1
    // ==================================================

    public void SetPlayer1Left(bool pressed)
    {
        player1Left = pressed;
    }

    public void SetPlayer1Right(bool pressed)
    {
        player1Right = pressed;
    }

    public void SetPlayer1Boost(bool pressed)
    {
        player1Boost = pressed;
    }

    public void SetPlayer1Fire(bool pressed)
    {
        player1Fire = pressed;
    }

    public void SetPlayer1Shield(bool pressed)
    {
        player1Shield = pressed;
    }

    public void SetPlayer1Aim(Vector2 position)
    {
        player1AimPosition = position;
        player1AimActive = true;
    }

    public void EndPlayer1Aim()
    {
        player1AimActive = false;
    }

    // ==================================================
    // PLAYER 2
    // ==================================================

    public void SetPlayer2Left(bool pressed)
    {
        player2Left = pressed;
    }

    public void SetPlayer2Right(bool pressed)
    {
        player2Right = pressed;
    }

    public void SetPlayer2Boost(bool pressed)
    {
        player2Boost = pressed;
    }

    public void SetPlayer2Fire(bool pressed)
    {
        player2Fire = pressed;
    }

    public void SetPlayer2Shield(bool pressed)
    {
        player2Shield = pressed;
    }

    public void SetPlayer2Aim(Vector2 position)
    {
        player2AimPosition = position;
        player2AimActive = true;
    }

    public void EndPlayer2Aim()
    {
        player2AimActive = false;
    }

    // ==================================================
    // READ INPUT
    // ==================================================

    public float GetMovementInput(int playerNumber)
    {
        if (playerNumber == 1)
        {
            if (player1Left && player1Right)
                return 0f;

            if (player1Left)
                return -1f;

            if (player1Right)
                return 1f;

            return 0f;
        }

        if (player2Left && player2Right)
            return 0f;

        if (player2Left)
            return -1f;

        if (player2Right)
            return 1f;

        return 0f;
    }

    public bool GetBoostInput(int playerNumber)
    {
        return playerNumber == 1
            ? player1Boost
            : player2Boost;
    }

    public bool GetFireInput(int playerNumber)
    {
        return playerNumber == 1
            ? player1Fire
            : player2Fire;
    }

    public bool GetShieldInput(int playerNumber)
    {
        return playerNumber == 1
            ? player1Shield
            : player2Shield;
    }

    public bool GetAimInput(
        int playerNumber,
        out Vector2 position)
    {
        if (playerNumber == 1)
        {
            position = player1AimPosition;
            return player1AimActive;
        }

        position = player2AimPosition;
        return player2AimActive;
    }

    // ==================================================
    // FLUX RESET
    // ==================================================

    public void CancelAllInput()
    {
        // Clear Player 1 input.
        player1Left = false;
        player1Right = false;
        player1Boost = false;
        player1Fire = false;
        player1Shield = false;

        // Clear Player 2 input.
        player2Left = false;
        player2Right = false;
        player2Boost = false;
        player2Fire = false;
        player2Shield = false;

        // Clear aim state.
        player1AimActive = false;
        player2AimActive = false;

        // Tell individual touch controls to release
        // their pointer ownership as well.
        OnInputCancelled?.Invoke();
    }
}