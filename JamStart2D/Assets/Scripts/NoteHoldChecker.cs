using UnityEngine;
using System.Collections.Generic;

public class NoteHoldChecker : MonoBehaviour
{
    [Header("Settings")]
    public Vector2 requiredDirection;
    public GameObject hitParticlesPrefab;
    public float particleCooldown = 0.4f;

    [Header("Visuals (linked externally)")]
    public SpriteRenderer headRenderer;
    public SpriteRenderer tailRenderer;
    public List<SpriteRenderer> bodyRenderers = new List<SpriteRenderer>();

    public Color successColor = Color.green;
    public Color idleColor = Color.white;
    public Color holdingColor = Color.yellow;

    private bool isInsideZone = false;
    private bool tapNoteRegistered = false;
    private bool alreadyCompleted = false;
    private float lastParticleTime = -999f;
    private bool comboBroken = false;

    private float enterZoneTime = -1f;
    public float maxTapDelay = 1f; // segundos para aceptar TAP


    void Start()
    {
        SetAllRenderersColor(idleColor);
    }

    void Update()
    {
        if (alreadyCompleted) return;

        if (!tapNoteRegistered && PlayerInputSystem.tapNotePressed)
        {
            tapNoteRegistered = true;
        }

        if (isInsideZone && tapNoteRegistered)
        {
            Vector2 dir = PlayerInputSystem.currentDirection;

            if (dir != Vector2.zero)
            {
                float dot = Vector2.Dot(dir.normalized, requiredDirection.normalized);

                if (dot > 0.9f)
                {
                    SetAllRenderersColor(holdingColor);

                    if (Time.time - lastParticleTime >= particleCooldown)
                    {
                        EmitParticle();
                        lastParticleTime = Time.time;
                    }

                    return;
                }
            }
        }

        SetAllRenderersColor(idleColor);

        if (!comboBroken && tapNoteRegistered && isInsideZone)
        {
            Vector2 dir = PlayerInputSystem.currentDirection;
            float dot = Vector2.Dot(dir.normalized, requiredDirection.normalized);

            if (dir == Vector2.zero || dot < 0.8f)
            {
                ComboManager.Instance?.ResetCombo();
                comboBroken = true;
            }
        }
    }

    void EmitParticle()
    {
        if (hitParticlesPrefab != null)
        {
            GameObject particles = Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);
            particles.transform.localScale = Vector3.one;
        }
    }

    void CompleteNote()
    {
        if (alreadyCompleted) return;
        alreadyCompleted = true;

        SetAllRenderersColor(successColor);
        EmitParticle();
        ComboManager.Instance?.AddCombo();

        ScoreManager.Instance?.AddScore(ScoreManager.Instance.pointsPerHold);
        Destroy(gameObject, 0.05f);
    }

    void SetAllRenderersColor(Color color)
    {
        if (headRenderer) headRenderer.color = color;
        if (tailRenderer) tailRenderer.color = color;
        foreach (var body in bodyRenderers)
        {
            if (body != null) body.color = color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        enterZoneTime = Time.time;

        if (other.CompareTag("ActivationZone"))
        {
            isInsideZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActivationZone"))
        {
            isInsideZone = false;
        }
    }
}
