using UnityEngine;
using UnityEngine.EventSystems;

public class MobileAimTouch : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler,
    IPointerExitHandler
{
    [Header("Player")]
    [SerializeField] private int playerNumber = 1;

    private int activePointerId = -1;

    // ==================================================
    // UNITY
    // ==================================================

    private void OnEnable()
    {
        if (MobileInputController.Instance != null)
        {
            MobileInputController.Instance.OnInputCancelled +=
                HandleInputCancelled;
        }
    }

    private void OnDisable()
    {
        if (MobileInputController.Instance != null)
        {
            MobileInputController.Instance.OnInputCancelled -=
                HandleInputCancelled;
        }

        EndAim();
    }

    // ==================================================
    // POINTER DOWN
    // ==================================================

    public void OnPointerDown(
        PointerEventData eventData)
    {
        // This control already owns a touch.
        if (activePointerId != -1)
            return;

        activePointerId =
            eventData.pointerId;

        UpdateAim(eventData);
    }

    // ==================================================
    // DRAG
    // ==================================================

    public void OnDrag(
        PointerEventData eventData)
    {
        // Only the original touch can control this aim pad.
        if (eventData.pointerId != activePointerId)
            return;

        UpdateAim(eventData);
    }

    // ==================================================
    // POINTER UP
    // ==================================================

    public void OnPointerUp(
        PointerEventData eventData)
    {
        if (eventData.pointerId != activePointerId)
            return;

        EndAim();
    }

    // ==================================================
    // POINTER EXIT
    // ==================================================

    public void OnPointerExit(
        PointerEventData eventData)
    {
        // Intentionally empty.

        // Leaving the UI rectangle does NOT release
        // ownership of the touch.
    }

    // ==================================================
    // AIM
    // ==================================================

    private void UpdateAim(
        PointerEventData eventData)
    {
        MobileInputController input =
            MobileInputController.Instance;

        if (input == null)
            return;

        if (playerNumber == 1)
        {
            input.SetPlayer1Aim(
                eventData.position
            );
        }
        else
        {
            input.SetPlayer2Aim(
                eventData.position
            );
        }
    }

    // ==================================================
    // END AIM
    // ==================================================

    private void EndAim()
    {
        if (MobileInputController.Instance != null)
        {
            if (playerNumber == 1)
            {
                MobileInputController.Instance
                    .EndPlayer1Aim();
            }
            else
            {
                MobileInputController.Instance
                    .EndPlayer2Aim();
            }
        }

        activePointerId = -1;
    }

    // ==================================================
    // QUANTUM FLUX
    // ==================================================

    private void HandleInputCancelled()
    {
        // Important:
        // Release the pointer ownership itself,
        // not just the input value.

        activePointerId = -1;
    }

    // ==================================================
    // EXTERNAL CANCEL
    // ==================================================

    public void CancelTouch()
    {
        EndAim();
    }
}