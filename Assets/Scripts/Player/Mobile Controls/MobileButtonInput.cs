using UnityEngine;
using UnityEngine.EventSystems;

public class MobileButtonInput : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerExitHandler
{
    public enum InputAction
    {
        Left,
        Right,
        Boost,
        Fire,
        Shield
    }

    [Header("Player")]
    [SerializeField] private int playerNumber = 1;

    [Header("Action")]
    [SerializeField] private InputAction action;

    private int activePointerId = -1;

    public void OnPointerDown(PointerEventData eventData)
    {
        // This button is already owned by another touch.
        if (activePointerId != -1)
            return;

        activePointerId = eventData.pointerId;

        SetInput(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId != activePointerId)
            return;

        SetInput(false);

        activePointerId = -1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Do NOT release the button here.
        //
        // The touch remains assigned to the button
        // until the finger is released or cancelled.
    }

    public void CancelTouch()
    {
        if (activePointerId == -1)
            return;

        SetInput(false);

        activePointerId = -1;
    }

    private void SetInput(bool pressed)
    {
        if (MobileInputController.Instance == null)
            return;

        if (playerNumber == 1)
        {
            switch (action)
            {
                case InputAction.Left:
                    MobileInputController.Instance.SetPlayer1Left(pressed);
                    break;

                case InputAction.Right:
                    MobileInputController.Instance.SetPlayer1Right(pressed);
                    break;

                case InputAction.Boost:
                    MobileInputController.Instance.SetPlayer1Boost(pressed);
                    break;

                case InputAction.Fire:
                    MobileInputController.Instance.SetPlayer1Fire(pressed);
                    break;

                case InputAction.Shield:
                    MobileInputController.Instance.SetPlayer1Shield(pressed);
                    break;
            }
        }
        else
        {
            switch (action)
            {
                case InputAction.Left:
                    MobileInputController.Instance.SetPlayer2Left(pressed);
                    break;

                case InputAction.Right:
                    MobileInputController.Instance.SetPlayer2Right(pressed);
                    break;

                case InputAction.Boost:
                    MobileInputController.Instance.SetPlayer2Boost(pressed);
                    break;

                case InputAction.Fire:
                    MobileInputController.Instance.SetPlayer2Fire(pressed);
                    break;

                case InputAction.Shield:
                    MobileInputController.Instance.SetPlayer2Shield(pressed);
                    break;
            }
        }
    }

    private void OnDisable()
    {
        CancelTouch();
    }
}