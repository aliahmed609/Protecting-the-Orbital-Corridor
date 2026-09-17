using UnityEngine;

public class MobileInputController : MonoBehaviour
{
    public static MobileInputController Instance { get; private set; }

    // Player 1
    private bool player1Left;
    private bool player1Right;
    private bool player1Boost;
    private bool player1Fire;
    private bool player1Shield;

    // Player 2
    private bool player2Left;
    private bool player2Right;
    private bool player2Boost;
    private bool player2Fire;
    private bool player2Shield;

    // Aim
    private Vector2 player1AimDelta;
    private Vector2 player2AimDelta;

    private bool player1AimActive;
    private bool player2AimActive;

    // AimPad automatically firing
    private bool player1AimFire;
    private bool player2AimFire;

    private MobileAimTouch player1AimTouch;
    private MobileAimTouch player2AimTouch;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // --------------------------------------------------
    // AIM TOUCH REGISTRATION
    // --------------------------------------------------

    public void RegisterAimTouch(int playerNumber, MobileAimTouch aimTouch)
    {
        if (playerNumber == 1)
            player1AimTouch = aimTouch;
        else if (playerNumber == 2)
            player2AimTouch = aimTouch;
    }

    // --------------------------------------------------
    // PLAYER 1
    // --------------------------------------------------

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

    public void SetPlayer1Aim(Vector2 delta)
    {
        player1AimDelta = delta;
        player1AimActive = true;
    }

    public void EndPlayer1Aim()
    {
        player1AimActive = false;
        player1AimDelta = Vector2.zero;
        player1AimFire = false;
    }

    public void SetPlayer1AimFire(bool pressed)
    {
        player1AimFire = pressed;
    }

    // --------------------------------------------------
    // PLAYER 2
    // --------------------------------------------------

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

    public void SetPlayer2Aim(Vector2 delta)
    {
        player2AimDelta = delta;
        player2AimActive = true;
    }

    public void EndPlayer2Aim()
    {
        player2AimActive = false;
        player2AimDelta = Vector2.zero;
        player2AimFire = false;
    }

    public void SetPlayer2AimFire(bool pressed)
    {
        player2AimFire = pressed;
    }

    // --------------------------------------------------
    // MOVEMENT
    // --------------------------------------------------

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

    // --------------------------------------------------
    // ABILITIES
    // --------------------------------------------------

    public bool GetBoostInput(int playerNumber)
    {
        return playerNumber == 1
            ? player1Boost
            : player2Boost;
    }

    public bool GetFireInput(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return player1Fire || player1AimFire;
        }

        return player2Fire || player2AimFire;
    }

    public bool GetShieldInput(int playerNumber)
    {
        return playerNumber == 1
            ? player1Shield
            : player2Shield;
    }

    // --------------------------------------------------
    // AIM
    // --------------------------------------------------

    public bool GetAimInput(
        int playerNumber,
        out Vector2 position)
    {
        if (playerNumber == 1)
        {
            position = player1AimDelta;
            return player1AimActive;
        }

        position = player2AimDelta;
        return player2AimActive;
    }

    public Vector2 GetAimDelta(int playerNumber)
    {
        if (playerNumber == 1)
        {
            Vector2 delta = player1AimDelta;
            player1AimDelta = Vector2.zero;
            return delta;
        }

        Vector2 player2Delta = player2AimDelta;
        player2AimDelta = Vector2.zero;
        return player2Delta;
    }

    // --------------------------------------------------
    // RESET
    // --------------------------------------------------

    public void CancelAllInput()
    {
        player1Left = false;
        player1Right = false;
        player1Boost = false;
        player1Fire = false;
        player1Shield = false;

        player2Left = false;
        player2Right = false;
        player2Boost = false;
        player2Fire = false;
        player2Shield = false;

        player1AimActive = false;
        player2AimActive = false;

        player1AimFire = false;
        player2AimFire = false;

        player1AimDelta = Vector2.zero;
        player2AimDelta = Vector2.zero;

        if (player1AimTouch != null)
            player1AimTouch.CancelTouch();

        if (player2AimTouch != null)
            player2AimTouch.CancelTouch();
    }
}