using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InspectSystem : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;
    public Vector3 offset = new Vector3(0, 0, -2);

    private Transform target;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool inspecting = false;
    
    private InputAction clickAction;
    private InputAction mouseDeltaAction;
    private InputAction escapeAction;
    
    void Awake()
    {
        clickAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        mouseDeltaAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        escapeAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
    }

    void OnEnable()
    {
        clickAction.Enable();
        mouseDeltaAction.Enable();
        escapeAction.Enable();
    }

    void OnDisable()
    {
        clickAction.Disable();
        mouseDeltaAction.Disable();
        escapeAction.Disable();
    }

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        HandleClick();

        if (inspecting && target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * moveSpeed);
            transform.LookAt(target);

            if (clickAction.IsPressed())
            {
                Vector2 mouseDelta = mouseDeltaAction.ReadValue<Vector2>();
                float mouseX = mouseDelta.x;

                target.Rotate(Vector3.up, -mouseX * rotateSpeed * Time.deltaTime, Space.World);
            }
        }

        if (escapeAction.triggered)
        {
            ExitInspect();
        }
    }

    void HandleClick()
    {
        if (clickAction.triggered && !inspecting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");
                target = hit.transform;
                inspecting = true;
            }
            else
            {
                Debug.Log("shit dont work");
            }
        }
    }

    void ExitInspect()
    {
        inspecting = false;
        target = null;

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}