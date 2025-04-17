using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int currentScore = 0;
    public int streak = 0;
    public int highScore = 0;

    public Text scoreText;
    public Text highScoreText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }



    public void AddScore(int points)
    {
        streak++;
        currentScore += points * streak;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateUI();
    }



    public void ResetStreak()
    {
        streak = 0;
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}