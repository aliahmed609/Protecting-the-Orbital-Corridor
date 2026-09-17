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

    [Header("References")]
    [SerializeField] private GameObject reticle;
    [SerializeField] private Transform player;

    [Header("Reticle")]
    [SerializeField] private float reticleDistance = 3f;

    [Header("Aim")]
    [SerializeField] private float aimSensitivity = 0.02f;

    private int activePointerId = -1;

    private void Awake()
    {
        if (reticle != null)
            reticle.SetActive(false);
    }

    private void Start()
    {
        if (MobileInputController.Instance != null)
        {
            MobileInputController.Instance.RegisterAimTouch(
                playerNumber,
                this
            );
        }
    }

    // --------------------------------------------------
    // TOUCH START
    // --------------------------------------------------

    public void OnPointerDown(PointerEventData eventData)
    {
        if (activePointerId != -1)
            return;

        activePointerId = eventData.pointerId;

        Debug.Log(
            "AIM START - Player " +
            playerNumber
        );

        ShowReticle();

        // Start aiming
        SendAimDelta(Vector2.zero);

        // Start firing automatically
        SetAimFire(true);
    }

    // --------------------------------------------------
    // DRAG
    // --------------------------------------------------

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != activePointerId)
            return;

        Vector2 delta =
            eventData.delta * aimSensitivity;

        SendAimDelta(delta);
    }

    // --------------------------------------------------
    // TOUCH END
    // --------------------------------------------------

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId != activePointerId)
            return;

        Debug.Log(
            "AIM END - Player " +
            playerNumber
        );

        EndAim();
    }

    // --------------------------------------------------
    // POINTER EXIT
    // --------------------------------------------------

    public void OnPointerExit(PointerEventData eventData)
    {
        // Intentionally empty.
        //
        // Leaving the visual boundary while dragging
        // should NOT immediately cancel the AimPad.
    }

    // --------------------------------------------------
    // RETICLE
    // --------------------------------------------------

    private void ShowReticle()
    {
        if (reticle == null)
            return;

        reticle.SetActive(true);

        if (player != null)
        {
            Vector3 position =
                player.position +
                Vector3.up * reticleDistance;

            position.z = 0f;

            reticle.transform.position = position;
        }
    }

    // --------------------------------------------------
    // AIM
    // --------------------------------------------------

    private void SendAimDelta(Vector2 delta)
    {
        if (MobileInputController.Instance == null)
            return;

        if (playerNumber == 1)
        {
            MobileInputController.Instance.SetPlayer1Aim(delta);
        }
        else
        {
            MobileInputController.Instance.SetPlayer2Aim(delta);
        }
    }

    // --------------------------------------------------
    // AIM FIRE
    // --------------------------------------------------

    private void SetAimFire(bool pressed)
    {
        if (MobileInputController.Instance == null)
            return;

        if (playerNumber == 1)
        {
            MobileInputController.Instance.SetPlayer1AimFire(
                pressed
            );
        }
        else
        {
            MobileInputController.Instance.SetPlayer2AimFire(
                pressed
            );
        }
    }

    // --------------------------------------------------
    // END
    // --------------------------------------------------

    private void EndAim()
    {
        if (MobileInputController.Instance != null)
        {
            if (playerNumber == 1)
            {
                MobileInputController.Instance.EndPlayer1Aim();
            }
            else
            {
                MobileInputController.Instance.EndPlayer2Aim();
            }
        }

        if (reticle != null)
            reticle.SetActive(false);

        activePointerId = -1;
    }

    // --------------------------------------------------
    // CANCEL
    // --------------------------------------------------

    public void CancelTouch()
    {
        if (activePointerId == -1)
            return;

        EndAim();
    }

    private void OnDisable()
    {
        if (activePointerId != -1)
        {
            EndAim();
        }
    }
}