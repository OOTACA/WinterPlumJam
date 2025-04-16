using UnityEngine;

public class NoteScaler : MonoBehaviour
{
    public float stretchFactor = 1f; // escala base de cuánto se estira

    public void StretchInDirection(Vector2 direction, float durationSeconds)
    {
        if (direction == Vector2.zero)
        {
            Debug.LogWarning("[NoteScaler] Dirección inválida, no se puede estirar.");
            return;
        }

        // Normalizar la dirección para que no escale en diagonal de más
        Vector2 normDir = direction.normalized;
        float distance = durationSeconds * stretchFactor;

        // Convertimos dirección a escala en X e Y
        Vector3 scale = transform.localScale;

        // Solo afecta el eje correspondiente (diagonal también sirve)
        scale.x += Mathf.Abs(normDir.x) * distance;
        scale.y += Mathf.Abs(normDir.y) * distance;

        transform.localScale = scale;

        // Opcional: alinear visualmente el origen del estiramiento
        transform.position -= new Vector3(normDir.x, normDir.y, 0) * distance * 0.5f;
    }
}
