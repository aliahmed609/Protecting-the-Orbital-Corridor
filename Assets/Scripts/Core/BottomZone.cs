using UnityEngine;

public class BottomZone : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Breach breach =
            other.GetComponent<Breach>();

        if (breach == null)
            return;

        // Reaching the bottom immediately loses the round.
        if (roundManager != null)
        {
            roundManager.LoseRound();
        }

        // Breach is pooled instead of destroyed.
        breach.ReturnToPoolFromBottom();
    }
}
