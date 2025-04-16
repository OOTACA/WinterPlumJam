using UnityEngine;
using SonicBloom.Koreo.Players; 

public class GameEndOnMusicFinish : MonoBehaviour
{
    public SimpleMusicPlayer musicPlayer;
    private bool gameEnded = false;

    void Update()
    {
        if (!gameEnded && !musicPlayer.IsPlaying)

        {
            gameEnded = true;
            Debug.Log("🎵 ¡La canción terminó!");
            EndGame();
        }
    }

    void EndGame()
    {
        // Pausa el juego (opcional)
        Time.timeScale = 0f;

        // Aquí podés mostrar UI, guardar datos, o cambiar de escena
        Debug.Log("🛑 Fin del juego. Mostrar resultados.");
    }
}
