using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MobileCameraScaler : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private float referenceAspect = 16f / 9f;

    [Header("Camera")]
    [SerializeField] private float referenceOrthographicSize = 5f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        ApplyCameraSettings();
    }

    private void ApplyCameraSettings()
    {
        if (cam == null)
            return;

        cam.orthographic = true;

        float currentAspect =
            (float)Screen.width / Screen.height;

        if (currentAspect >= referenceAspect)
        {
            // Wider screens.
            // Keep the same vertical gameplay view.
            cam.orthographicSize =
                referenceOrthographicSize;
        }
        else
        {
            // Narrower screens.
            // Show a little more vertically instead
            // of cropping the game.
            cam.orthographicSize =
                referenceOrthographicSize *
                (referenceAspect / currentAspect);
        }
    }
}