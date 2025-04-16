using UnityEngine;

public class NoteTapChecker : MonoBehaviour
{
    public GameObject hitParticlesPrefab;
    public SpriteRenderer spriteRenderer;
    public Color successColor = Color.green;
    public Color idleColor = Color.white;

    private bool isInsideZone = false;
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

        if (isInsideZone && PlayerInputSystem.tapNotePressed)
        {
            CompleteNote();
        }
    }

    void CompleteNote()
    {
        alreadyCompleted = true;

        spriteRenderer.color = successColor;

        if (hitParticlesPrefab != null)
        {
            Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);
        }

        Debug.Log($"[Note {name}] ✅ TAP completado");
        ComboManager.Instance?.AddCombo();
        ScoreManager.Instance?.AddScore(ScoreManager.Instance.pointsPerTap);

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
