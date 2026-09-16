using UnityEngine;

public class BottomZone : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Breach breach = other.GetComponent<Breach>();

        if (breach != null)
        {
            roundManager.LoseRound();
            Destroy(breach.gameObject);
        }
    }
}