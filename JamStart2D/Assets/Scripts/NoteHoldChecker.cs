using UnityEngine;

public class NoteHoldChecker : MonoBehaviour
{
    [Header("Settings")]
    public Vector2 requiredDirection; // Si es Vector2.zero = nota normal (tap)
    public GameObject hitParticlesPrefab;

    [Header("Visual")]
    public SpriteRenderer spriteRenderer;
    public Color successColor = Color.green;
    public Color idleColor = Color.white;

    private bool isInsideZone = false;
    private bool tapNoteRegistered = false;
    private bool alreadyCompleted = false;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = idleColor;
    }

    void Update()
    {
        if (alreadyCompleted) return;

        // Si se presionó TapNote
        if (!tapNoteRegistered && PlayerInputSystem.tapNotePressed)
        {
            tapNoteRegistered = true;
            Debug.Log($"[Note {name}] Se registró el TAP");

            if (requiredDirection == Vector2.positiveInfinity)
            {
                if (isInsideZone && PlayerInputSystem.tapNotePressed)
                {
                    CompleteNote();
                    return;
                }
            }
        }

        // Para notas con dirección requerida
        if (isInsideZone && tapNoteRegistered && requiredDirection != Vector2.zero)
        {
            Vector2 dir = PlayerInputSystem.currentDirection;

            if (dir != Vector2.zero)
            {
                float dot = Vector2.Dot(dir.normalized, requiredDirection.normalized);
                Debug.Log($"[Note {name}] Dirección actual: {dir}, DOT = {dot}");

                if (dot > 0.9f)
                {
                    CompleteNote();
                    return;
                }
            }
        }

        spriteRenderer.color = idleColor;
    }

    void CompleteNote()
    {
        if (alreadyCompleted) return;
        alreadyCompleted = true;

        spriteRenderer.color = successColor;

        if (hitParticlesPrefab != null)
        {
            GameObject particles = Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);
            particles.transform.localScale = Vector3.one;
        }

        Debug.Log($"[Note {name}] ✅ COMPLETADA");
        Destroy(gameObject, 0.05f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ActivationZone"))
        {
            Debug.Log($"[Note {name}] Entró al trigger central");
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
