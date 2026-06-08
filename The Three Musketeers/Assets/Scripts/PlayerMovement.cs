using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    public float dodgeForce = 3f;
    public float dodgeCooldown = 1f;

    private Rigidbody rb;
    
    private Vector3 movementInput;
    
    private bool canDodge = true;

    void Start()
    {
        Debug.Log("Running PlayerMovement");

        animator = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleDodge();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        float mouseX = Mouse.current.delta.ReadValue().x;

        rb.MoveRotation(
            rb.rotation * Quaternion.Euler(
                0f,
                mouseX * rotationSpeed * Time.deltaTime,
                0f
            )
        );

        float moveX = 0f;
        float moveZ = 0f;

        if (Keyboard.current.wKey.isPressed) moveZ += 1;
        if (Keyboard.current.sKey.isPressed) moveZ -= 1;

        float sidewaysMultiplier = 0.6f;

        if (Keyboard.current.dKey.isPressed) moveX += sidewaysMultiplier;
        if (Keyboard.current.aKey.isPressed) moveX -= sidewaysMultiplier;

        movementInput =
            transform.forward * moveZ +
            transform.right * moveX;

        float speed = movementInput.magnitude;

        if (speed > 0)
            speed = 1f;
        else
            speed = 0f;

        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(
            rb.position + movementInput * movementSpeed * Time.fixedDeltaTime
        );
    }
    
    void HandleDodge()
    {
        if (!canDodge)
            return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Vector3 dodgeDirection = Vector3.zero;

            if (Keyboard.current.aKey.isPressed)
            {
                dodgeDirection = -transform.right;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                dodgeDirection = transform.right;
            }

            if (dodgeDirection != Vector3.zero)
            {
                rb.MovePosition(
                    rb.position + dodgeDirection * dodgeForce
                );

                canDodge = false;

                Invoke(nameof(ResetDodge), dodgeCooldown);
            }
        }
    }

    void ResetDodge()
    {
        canDodge = true;
    }
}