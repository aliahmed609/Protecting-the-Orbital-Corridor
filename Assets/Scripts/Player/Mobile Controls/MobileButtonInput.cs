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

    public void OnPointerDown(PointerEventData eventData)
    {
        SetInput(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetInput(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetInput(false);
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
}