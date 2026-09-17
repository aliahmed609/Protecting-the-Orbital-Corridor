using UnityEngine;
using UnityEngine.EventSystems;

public class AimPadTest : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("========== AIM PAD DOWN ==========");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("========== AIM PAD DRAG ==========");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("========== AIM PAD UP ==========");
    }
}