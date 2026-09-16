using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class RoundResultUI : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Image resultImage;

    private void Start()
    {
        resultPanel.SetActive(false);

        roundManager.OnRoundWon += ShowWin;
        roundManager.OnRoundLost += ShowLose;
    }

    private void OnDestroy()
    {
        roundManager.OnRoundWon -= ShowWin;
        roundManager.OnRoundLost -= ShowLose;
    }

    private void ShowWin()
    {
        Debug.Log("RoundResultUI: SHOW WIN");

        resultText.text = "ROUND WON!";
        resultText.color = Color.red;
        resultImage.color = Color.green;
        resultPanel.SetActive(true);
    }
    private void ShowLose()
    {
        Debug.Log("RoundResultUI: SHOW LOSE");

        resultText.text = "ROUND LOST!";
        resultText.color = Color.green;
        resultImage.color = Color.red;
        resultPanel.SetActive(true);
    }
}