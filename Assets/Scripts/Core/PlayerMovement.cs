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

    public Transform model;

    [Header("Model Rotation Fix")]
    public float modelForwardOffset = 0f;
    // Set this based on your FBX forward direction:
    // 0 = Forward (+Z)
    // 90 = Facing right (+X)
    // -90 = Facing left (-X)
    // 180 = Facing backward (-Z)

    [Header("Weapon")]
    public GameObject MP7_prefab;
    private WeaponHolder weaponHolder;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        weaponHolder = GetComponent<WeaponHolder>();

        if (weaponHolder != null && MP7_prefab != null)
            weaponHolder.Equip(MP7_prefab);
    }

    void Update()
    {
        HandleTouchDrag();
        HandleKeyboard();
        MovePlayer();
        UpdateAnimation();
    }

    // =============================
    // TOUCH DRAG MOVEMENT
    // =============================
    void HandleTouchDrag()
    {
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 currentPos = Touchscreen.current.primaryTouch.position.ReadValue();

            if (previousTouchPos == Vector2.zero)
            {
                previousTouchPos = currentPos;
                return;
            }

            float deltaX = currentPos.x - previousTouchPos.x;
            previousTouchPos = currentPos;

            dragSpeed = deltaX / 100f; // sensitivity
        }
        else
        {
            previousTouchPos = Vector2.zero;
            dragSpeed = Mathf.MoveTowards(dragSpeed, 0f, Time.deltaTime * 2f);
        }
    }

    // =============================
    // KEYBOARD MOVEMENT
    // =============================
    void HandleKeyboard()
    {
        if (Keyboard.current == null)
            return;

        float dir = 0f;

        if (Keyboard.current.aKey.isPressed) dir = -1f;
        else if (Keyboard.current.dKey.isPressed) dir = 1f;
        else dir = 0f;

        if (dir != 0f)
            dragSpeed = dir * 0.15f;
        else
            dragSpeed = Mathf.MoveTowards(dragSpeed, 0f, Time.deltaTime * 4f);
    }

    // =============================
    // MOVE PLAYER
    // =============================
    void MovePlayer()
    {
        Vector3 pos = transform.position;
        pos.x += dragSpeed * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
        transform.position = pos;
    }

    // =============================
    // ANIMATION + MODEL ROTATION
    // =============================
    void UpdateAnimation()
    {
        if (animator == null)
            return;

        float speed = Mathf.Abs(dragSpeed * moveSpeed);
        animator.SetFloat("Speed", speed);
        animator.SetBool("Grounded", true);

        float strafeValue = 0f;
        if (dragSpeed > 0.01f) strafeValue = 1f;
        else if (dragSpeed < -0.01f) strafeValue = -1f;
        else strafeValue = 0f;

        animator.SetFloat("Strafe", strafeValue);

        // ---------- MODEL ROTATION FIX ----------
        if (model != null)
        {
            float targetY = modelForwardOffset;

            if (Mathf.Abs(dragSpeed) > 0.01f)
            {
                // modelForwardOffset = your model's natural forward direction
                targetY = dragSpeed > 0
                    ? modelForwardOffset + 90f   // moving right
                    : modelForwardOffset -270f;  // moving left
            }

            Quaternion targetRot = Quaternion.Euler(0, targetY, 0);

            model.localRotation = Quaternion.Slerp(
                model.localRotation,
                targetRot,
                Time.deltaTime * 10f
            );
        }
    }
}
