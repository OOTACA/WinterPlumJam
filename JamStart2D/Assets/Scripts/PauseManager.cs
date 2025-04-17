using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseUI;
    public AudioSource musicSource;
    public MonoBehaviour noteSpawnerScript;
    private bool isPaused = false;
    private float storedAudioTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Guardamos el tiempo actual del audio y pausamos
            storedAudioTime = musicSource.time;
            musicSource.Pause();
            Time.timeScale = 0f;
            if (noteSpawnerScript != null) noteSpawnerScript.enabled = false;
            pauseUI.SetActive(true);
        }
        else
        {
            // Restauramos el tiempo y reanudamos
            Time.timeScale = 1f;
            musicSource.time = storedAudioTime;
            musicSource.Play();
            if (noteSpawnerScript != null) noteSpawnerScript.enabled = true;
            pauseUI.SetActive(false);
        }
    }
}