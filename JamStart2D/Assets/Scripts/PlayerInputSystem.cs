using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public SpriteRenderer playerRenderer;
    public Color tapColor = Color.green;
    public Color normalColor = Color.white;

    public static Vector2 currentDirection;      // Dirección actual sostenida (WASD o joystick)
    public static bool tapNotePressed = false;   // Si se presionó el botón de acción este frame

    private bool flashOnTap = true;

    // Dirección del jugador (WASD o stick)
    public void OnMove(InputAction.CallbackContext context)
    {
        currentDirection = context.ReadValue<Vector2>();

        if (context.performed)
        {
            Debug.Log($"[PlayerInput] Direction pressed: {currentDirection}");
        }

        if (context.canceled)
        {
            Debug.Log("[PlayerInput] Direction released");
        }
    }

    // Presionar el botón de nota (Space o Gamepad)
    public void OnTapNote(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            tapNotePressed = true;
            Debug.Log("[PlayerInput] TAP NOTE started");

            // Feedback visual temporal
            if (playerRenderer != null && flashOnTap)
            {
                playerRenderer.color = tapColor;
                CancelInvoke(nameof(ResetColor));
                Invoke(nameof(ResetColor), 0.1f);
            }
        }
    }

    void ResetColor()
    {
        if (playerRenderer != null)
        {
            playerRenderer.color = normalColor;
        }
    }

    // Reset del "tap" para que dure solo un frame
    void LateUpdate()
    {
        if (tapNotePressed)
        {
            Debug.Log("[PlayerInput] TAP NOTE reset (end of frame)");
        }

        tapNotePressed = false;
    }
}
