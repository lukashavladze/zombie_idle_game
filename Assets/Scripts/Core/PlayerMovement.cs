using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 100f;
    public float maxX = 10f;

    private float dragSpeed = 0f;
    private Vector2 previousTouchPos;

    void Update()
    {
        HandleTouchDrag();
        HandleKeyboard();
        MovePlayer();
    }

    // ---------------- TOUCH DRAG ----------------
    void HandleTouchDrag()
    {
        // Touch active?
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 currentPos = Touchscreen.current.primaryTouch.position.ReadValue();

            if (previousTouchPos == Vector2.zero)
            {
                previousTouchPos = currentPos;
                return;
            }

            float deltaX = currentPos.x - previousTouchPos.x;
            previousTouchPos = currentPos;

            dragSpeed = deltaX / 100f;   // touch sensitivity
        }
        else
        {
            dragSpeed = 0;
            previousTouchPos = Vector2.zero;
        }
    }

    // ---------------- KEYBOARD (A / D) ----------------
    void HandleKeyboard()
    {
        float dir = 0;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) dir = -1;
            if (Keyboard.current.dKey.isPressed) dir = 1;
        }

        // Only apply if no touch drag happening
        if (Mathf.Abs(dir) > 0)
        {
            dragSpeed = dir * 0.1f;
        }
    }

    // ---------------- APPLY MOVEMENT ----------------
    void MovePlayer()
    {
        Vector3 pos = transform.position;

        pos.x += dragSpeed * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -maxX, maxX);

        transform.position = pos;
    }
}
