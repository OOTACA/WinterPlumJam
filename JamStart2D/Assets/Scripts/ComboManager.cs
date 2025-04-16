using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;

    [Header("UI")]
    public TMP_Text comboText;

    private int currentCombo = 0;
    private int maxCombo = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddCombo()
    {
        currentCombo++;
        maxCombo = Mathf.Max(maxCombo, currentCombo);
        UpdateUI();
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (comboText != null)
        {
            comboText.text = currentCombo > 0 ? $"COMBO: \n\n{currentCombo}" : "";
        }
    }

    public int GetCurrentCombo() => currentCombo;
    public int GetMaxCombo() => maxCombo;
}
