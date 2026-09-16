using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private RoundManager roundManager;

    [Header("Hull")]
    [SerializeField] private GameObject[] hullBars;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;

    private void Update()
    {
        UpdateHull();
        UpdateScore();
        UpdateTimer();
    }

    private void UpdateHull()
    {
        int hull = playerHealth.CurrentHull;

        for (int i = 0; i < hullBars.Length; i++)
        {
            hullBars[i].SetActive(i < hull);
        }
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + scoreManager.Score;
    }

    private void UpdateTimer()
    {
        float timeLeft = Mathf.Max(
            0f,
            60f - roundManager.CurrentTime
        );

        timerText.text = timeLeft.ToString("00");
    }
}