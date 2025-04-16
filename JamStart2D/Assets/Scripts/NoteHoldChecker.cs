using UnityEngine;

public class NoteHoldChecker : MonoBehaviour
{
    [Header("Settings")]
    public Vector2 requiredDirection;
    public GameObject hitParticlesPrefab;
    public float particleCooldown = 0.4f; // tiempo mínimo entre partículas

    [Header("Visual")]
    public SpriteRenderer spriteRenderer;
    public Color successColor = Color.green;
    public Color idleColor = Color.white;
    public Color holdingColor = Color.yellow;

    private bool isInsideZone = false;
    private bool tapNoteRegistered = false;
    private bool alreadyCompleted = false;
    private float lastParticleTime = -999f;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = idleColor;
    }

    void Update()
    {
        if (alreadyCompleted) return;

        // Registro de TAP inicial
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
                    spriteRenderer.color = holdingColor;

                    // Cooldown: ¿podemos mostrar partículas?
                    if (Time.time - lastParticleTime >= particleCooldown)
                    {
                        EmitParticle();
                        lastParticleTime = Time.time;
                    }

                    return;
                }
            }
        }

        spriteRenderer.color = idleColor;
    }

    void EmitParticle()
    {
        if (hitParticlesPrefab != null)
        {
            GameObject particles = Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);
            particles.transform.localScale = Vector3.one;
            Debug.Log($"[Note {name}] ✨ Emitiendo partículas (CD ok)");
        }
    }

    void CompleteNote()
    {
        if (alreadyCompleted) return;
        alreadyCompleted = true;

        spriteRenderer.color = successColor;
        EmitParticle();
        Debug.Log($"[Note {name}] ✅ COMPLETADA");
        Destroy(gameObject, 0.05f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
