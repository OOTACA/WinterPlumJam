using UnityEngine;
using System.Collections.Generic;

public class NoteHoldVisualBuilder : MonoBehaviour
{
    [Header("Visual References")]
    public SpriteRenderer headRenderer;
    public SpriteRenderer bodyPrefab;
    public SpriteRenderer tailRenderer;

    public Transform bodyContainer;

    [Header("Options")]
    public bool showHead = true;

   public void Build(Vector2 direction, float distance)
    {
        direction.Normalize();

        // Mostrar o esconder la cabeza
        if (headRenderer != null)
            headRenderer.enabled = showHead;

        // Activar la cola
        if (tailRenderer != null)
        {
            tailRenderer.enabled = true;
            tailRenderer.transform.localPosition = direction * distance;
        }

        // Activar un solo cuerpo (no clones)
        if (bodyPrefab != null)
        {
            bodyPrefab.enabled = true;
            bodyPrefab.transform.localPosition = direction * distance * 0.5f;

            // Escalarlo visualmente hasta cubrir la distancia
            Vector3 scale = bodyPrefab.transform.localScale;

            if (Mathf.Abs(direction.x) > 0) // horizontal
                scale.x = distance;
            else if (Mathf.Abs(direction.y) > 0) // vertical
                scale.y = distance;

            bodyPrefab.transform.localScale = scale;
        }

        // Pasar referencias al checker
        NoteHoldChecker checker = GetComponent<NoteHoldChecker>();
        if (checker != null)
        {
            checker.headRenderer = headRenderer;
            checker.tailRenderer = tailRenderer;
            checker.bodyRenderers = new List<SpriteRenderer> { bodyPrefab };
        }
    }

}
