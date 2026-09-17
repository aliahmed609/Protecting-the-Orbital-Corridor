using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAspectController : MonoBehaviour
{
    [Header("Reference Aspect")]
    [SerializeField] private float referenceWidth = 16f;
    [SerializeField] private float referenceHeight = 9f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        ApplyAspectRatio();
    }

    private void ApplyAspectRatio()
    {
        if (cam == null)
            return;

        float targetAspect =
            referenceWidth / referenceHeight;

        float currentAspect =
            (float)Screen.width / Screen.height;

        float scaleHeight =
            currentAspect / targetAspect;

        Rect rect = new Rect();

        // --------------------------------------------------
        // SCREEN IS WIDER THAN 16:9
        // --------------------------------------------------

        if (scaleHeight < 1f)
        {
            rect.width = 1f;
            rect.height = scaleHeight;

            rect.x = 0f;
            rect.y = (1f - scaleHeight) * 0.5f;
        }

        // --------------------------------------------------
        // SCREEN IS TALLER / NARROWER THAN 16:9
        // --------------------------------------------------

        else
        {
            float scaleWidth =
                1f / scaleHeight;

            rect.width = scaleWidth;
            rect.height = 1f;

            rect.x = (1f - scaleWidth) * 0.5f;
            rect.y = 0f;
        }

        cam.rect = rect;
    }
}