using UnityEngine;

public static class SpawnUtility
{
    public static bool IsPositionClear(
        Vector2 position,
        float radius,
        LayerMask obstacleLayer)
    {
        return Physics2D.OverlapCircle(
            position,
            radius,
            obstacleLayer
        ) == null;
    }
}