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

    [Header("Weapon")]
    public GameObject MP7_prefab;
    private WeaponHolder weaponHolder;

    [Header("Animation Tuning")]
    public float rightAnimSpeed = 1.2f;   // Right strafe animation speed
    public float leftAnimSpeed = 0.9f;    // Left strafe animation speed
    public float idleAnimSpeed = 1f;      // Idle animation speed


    public float idleAimOffsetY = -10f;


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


    // ---------------- TOUCH DRAG ----------------
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

            dragSpeed = deltaX / 100f;
        }
        else
        {
            previousTouchPos = Vector2.zero;
            dragSpeed = Mathf.MoveTowards(dragSpeed, 0f, Time.deltaTime * 2f);
        }
    }


    // ---------------- KEYBOARD ----------------
    void HandleKeyboard()
    {
        if (Keyboard.current == null)
            return;

        float dir = 0f;

        if (Keyboard.current.aKey.isPressed) dir = -1f;
        else if (Keyboard.current.dKey.isPressed) dir = 1f;

        if (dir != 0f)
            dragSpeed = dir * 0.15f;
        else
            dragSpeed = Mathf.MoveTowards(dragSpeed, 0f, Time.deltaTime * 4f);
    }


    // ---------------- MOVE PLAYER ----------------
    void MovePlayer()
    {
        Vector3 pos = transform.position;
        pos.x += dragSpeed * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
        pos.z = 0f;
        transform.position = pos;
    }


    // ---------------- ANIMATION ----------------
    void UpdateAnimation()
    {
        if (animator == null)
            return;

        float worldSpeed = Mathf.Abs(dragSpeed * moveSpeed);

        // BlendTree Speed (0–1)
        float animBlend = Mathf.Clamp01(worldSpeed / moveSpeed);
        animator.SetFloat("Speed", animBlend);

        animator.SetBool("Grounded", true);

        // STRAFE
        float strafeValue = 0f;
        if (dragSpeed > 0.01f) strafeValue = 1f;
        else if (dragSpeed < -0.01f) strafeValue = -1f;

        animator.SetFloat("Strafe", strafeValue);


        // ---------------- ANIMATION SPEED ----------------
        if (dragSpeed > 0.01f)
            animator.speed = rightAnimSpeed;
        else if (dragSpeed < -0.01f)
            animator.speed = leftAnimSpeed;
        else
            animator.speed = idleAnimSpeed;


        // ---------------- MODEL ROTATION ----------------
        if (model != null)
        {
            float targetY = modelForwardOffset;

            // MOVING RIGHT
            if (dragSpeed > 0.01f)
            {
                targetY = modelForwardOffset + 90f;
            }
            // MOVING LEFT
            else if (dragSpeed < -0.01f)
            {
                targetY = modelForwardOffset - 270f;
            }
            // IDLE → apply slight aim rotation
            else
            {
                targetY = modelForwardOffset + idleAimOffsetY;
            }

            model.localRotation = Quaternion.Slerp(
                model.localRotation,
                Quaternion.Euler(0f, targetY, 0f),
                12f * Time.deltaTime
            );
        }
    }

}
