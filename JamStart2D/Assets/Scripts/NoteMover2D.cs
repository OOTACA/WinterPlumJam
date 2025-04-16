using UnityEngine;

public class NoteMover2D : MonoBehaviour
{
    public Vector2 targetPosition = Vector2.zero;
    public float speed = 5f;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
