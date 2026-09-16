using UnityEngine;

public class GunnerAim : MonoBehaviour
{
    [SerializeField] private RoleManager roleManager;
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        if (roleManager.Player2Role != RoleManager.Role.Gunner)
            return;

        Vector3 mousePosition = Input.mousePosition;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        worldPosition.z = 0;

        transform.position = worldPosition;
    }
}