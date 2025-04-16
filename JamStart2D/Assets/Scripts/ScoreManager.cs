using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    public TMP_Text scoreText;

    [Header("Scoring")]
    public int pointsPerTap = 100;
    public int pointsPerHold = 200;

    private int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
{
    UpdateUI(); // ← Esto muestra "SCORE: 0" desde el inicio
}

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: \n\n{score}";

        }
    }

    public int GetScore() => score;
}
