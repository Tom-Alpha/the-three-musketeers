using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 0.0001f;

    void Start()
    {
        
        // finds the Animator on your character model
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {   
        float mouseX = Mouse.current.delta.ReadValue().x;

        transform.Rotate(Vector3.up * mouseX * rotationSpeed);
        
        float moveX = 0f;
        float moveZ = 0f;

        if (Keyboard.current.wKey.isPressed) moveZ += 1;
        if (Keyboard.current.sKey.isPressed) moveZ -= 1;
        if (Keyboard.current.dKey.isPressed) moveX += 1;
        if (Keyboard.current.aKey.isPressed) moveX -= 1;

        Vector3 move = new Vector3(moveX, 0, moveZ);

        // move player
        transform.Translate(move * movementSpeed * Time.deltaTime);

        // tell animator how fast we are moving
        float speed = move.magnitude;

        if (speed > 0)
            speed = 1f;
        else
            speed = 0f;

        if (animator != null)
        {
            animator.SetFloat("speed", speed);
        }
    }
}