using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 100f;
    public float maxX = 10f;

    private float dragSpeed = 0f;
    private Vector2 previousTouchPos;

    private Animator animator;

    public Transform model; // Assign your Model child in Inspector


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleTouchDrag();
        HandleKeyboard();
        MovePlayer();
        UpdateAnimation();
    }

    // ---------------- TOUCH DRAG ----------------
    void HandleTouchDrag()
    {
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

            dragSpeed = deltaX / 100f;  // touch sensitivity
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

        // Only apply if keyboard is used
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


    // ---------------- ANIMATION ----------------
    void UpdateAnimation()
    {
        if (!animator) return;

        float absSpeed = Mathf.Abs(dragSpeed * moveSpeed);

        animator.SetFloat("Speed", absSpeed);
        animator.SetFloat("MotionSpeed", 1f);
        animator.SetBool("Grounded", true);

        // ROTATION LEFT / RIGHT ONLY
        if (Mathf.Abs(dragSpeed) > 0.01f)
        {
            float direction = dragSpeed > 0 ? 1f : -1f;

            // IF your model faces +Z -> then right = +90, left = -90
            float targetY = direction > 0 ? 90 : -90;

            Quaternion targetRot = Quaternion.Euler(0, targetY, 0);

            model.rotation = Quaternion.Slerp(
                model.rotation,
                targetRot,
                Time.deltaTime * 10f
            );
        }
    }


}
