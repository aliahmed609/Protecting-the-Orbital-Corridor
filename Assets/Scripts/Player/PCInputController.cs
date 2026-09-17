using UnityEngine;

public class PCInputController : MonoBehaviour
{
    public float GetMovementInput()
    {
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);

        if (left && right)
            return 0f;

        if (left)
            return -1f;

        if (right)
            return 1f;

        return 0f;
    }

    public bool GetShieldInput()
    {
        return Input.GetMouseButton(1);
    }

    public bool GetBoostInput()
    {
        return Input.GetKey(KeyCode.Space);
    }
}