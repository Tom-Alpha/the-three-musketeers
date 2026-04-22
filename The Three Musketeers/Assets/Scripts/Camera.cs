using UnityEngine;
using UnityEngine.InputSystem;
// main camera manager with inspecting and the
// magnifying glass box appearing also depends on this script
public class InspectSystem : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;
    public Vector3 offset = new Vector3(0, 0, -2);
    public UIDragTool dragTool;
    public UISlidePanel uiPanel;
    
    private Transform target;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool inspecting = false;
    
    public float zoomSpeed = 2f;
    public float minZoom = -1f;
    public float maxZoom = -5f;

    private float targetZoom;
    private float currentZoom;
    private float zoomVelocity;
    
    private InputAction rightClickAction;
    private InputAction clickAction;
    private InputAction mouseDeltaAction;
    private InputAction escapeAction;
    
    private Vector2 currentRotation;
    private Vector2 targetRotation;
    private Vector2 rotationVelocity;

    public float smoothTime = 0.05f;
    public float sensitivity = 0.3f;
    
    
    void Awake()
    {
        rightClickAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/rightButton");
        clickAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        mouseDeltaAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        escapeAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
    }

    void OnEnable()
    {
        rightClickAction.Enable();
        clickAction.Enable();
        mouseDeltaAction.Enable();
        escapeAction.Enable();
    }

    void OnDisable()
    {
        rightClickAction.Disable();
        clickAction.Disable();
        mouseDeltaAction.Disable();
        escapeAction.Disable();
    }

    void Start()
    {
        targetZoom = offset.z;
        currentZoom = offset.z;
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    void Update()
    {
        if (inspecting)
        {
            uiPanel.Show();
        }
        else
        {
            uiPanel.Hide();
        }
        HandleClick();

        if (inspecting && target != null)
        {
            
            float scroll = Mouse.current.scroll.ReadValue().y;

            if (scroll != 0)
            {
                targetZoom += scroll * zoomSpeed * 0.1f;

                targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
            }
            
            currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVelocity, 0.1f);

            Vector3 zoomOffset = new Vector3(offset.x, offset.y, currentZoom);

            Vector3 desiredPosition = target.position + zoomOffset;
            
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * moveSpeed);
            transform.LookAt(target);

            if (clickAction.IsPressed())
            {
                if (dragTool == null || !dragTool.IsDragging)
                {
                    Vector2 mouseDelta = mouseDeltaAction.ReadValue<Vector2>();
                    float mouseX = mouseDelta.x;

                    target.Rotate(Vector3.up, -mouseX * rotateSpeed * Time.deltaTime, Space.World);
                }
            }
        }

        HandleFreeLook();
        if (escapeAction.triggered)
        {
            ExitInspect();
        }
    }
    
    void HandleFreeLook()
    {
        if (inspecting)
            return;

        if (dragTool != null && dragTool.IsDragging)
            return;

        if (rightClickAction.IsPressed())
        {
            Vector2 mouseDelta = mouseDeltaAction.ReadValue<Vector2>();
            
            targetRotation.x += mouseDelta.x * sensitivity;
            targetRotation.y -= mouseDelta.y * sensitivity;
        }
        
        currentRotation.x = Mathf.SmoothDamp(currentRotation.x, targetRotation.x, ref rotationVelocity.x, smoothTime);
        currentRotation.y = Mathf.SmoothDamp(currentRotation.y, targetRotation.y, ref rotationVelocity.y, smoothTime);

        currentRotation.y = Mathf.Clamp(currentRotation.y, -80f, 80f);
        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0f);
    }
    
    
    void HandleClick()
    {
        if (dragTool != null && dragTool.IsDragging)
            return;

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

        transform.position = originalPos;
        transform.rotation = originalRot;
    }
}