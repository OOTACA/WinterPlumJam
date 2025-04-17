using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameEndOnMusicFinish : MonoBehaviour

{
    public AudioSource musicSource;
    public CanvasGroup fadeCanvasGroup;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button quitButton;
    public float fadeDuration = 2f;

    private bool hasEnded = false;

    void Update()
    {
        if (!hasEnded && !musicSource.isPlaying)
        {
            hasEnded = true;
            StartCoroutine(EndGameSequence());
        }

        
    }

    private IEnumerator EndGameSequence()
    {
        // Fade in
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;

        // Show text and buttons
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        // Pause game
        Time.timeScale = 0f;
    }

    // Puedes vincular estos métodos a los botones desde el Inspector
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Cambia "MainMenu" por el nombre de tu escena de menú principal
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("saliendoooo");
    }
}