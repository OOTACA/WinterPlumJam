using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoodManager : MonoBehaviour
{
    public Image faceImage; // Usa esto si estás usando UI
    // public SpriteRenderer faceRenderer; // Usa esto si es un objeto en la escena

    public Sprite[] sprites; // Asigna tus 3 sprites en el Inspector

    public float minChangeTime = 5f;
    public float maxChangeTime = 10f;

    void Start()
    {
        StartCoroutine(ChangeSpriteRoutine());
    }

    IEnumerator ChangeSpriteRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minChangeTime, maxChangeTime));

            int index = Random.Range(0, sprites.Length);

            if (sprites.Length > 0)
            {
                faceImage.sprite = sprites[index];
                // faceRenderer.sprite = sprites[index]; // Descomenta si usas SpriteRenderer
            }
        }
    }
}