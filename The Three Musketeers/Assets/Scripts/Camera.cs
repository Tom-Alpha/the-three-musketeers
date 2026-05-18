using UnityEngine;
using UnityEngine.InputSystem;

// main camera manager with inspecting and proximity interaction
public class InspectSystem : MonoBehaviour
{
    [Header("Player")]
    public PlayerMovement playerMovement;
    public Renderer[] playerRenderers;
    
    public Transform player;
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;
    public Vector3 offset = new Vector3(0, 0, -2);

    [Header("Zoom")]
    public float zoomSpeed = 2f;
    public float minZoom = -1f;
    public float maxZoom = -5f;

    [Header("Detection")]
    public float inspectRange = 5f;
    public LayerMask inspectLayer;

    [Header("Prompt")]
    public GameObject promptUI;

    [Header("References")]
    public UIDragTool dragTool;
    public UISlidePanel uiPanel;

    private Transform target;
    private Transform nearbyTarget;

    private GameObject activePrompt;

    private Vector3 originalLocalPos;
    private Quaternion originalLocalRot;
    private Transform originalParent;

    private bool inspecting = false;

    private float targetZoom;
    private float currentZoom;
    private float zoomVelocity;

    private InputAction rightClickAction;
    private InputAction mouseDeltaAction;
    private InputAction escapeAction;
    private InputAction interactAction;
    private InputAction clickAction;

    private Vector2 currentRotation;
    private Vector2 targetRotation;
    private Vector2 rotationVelocity;

    public float smoothTime = 0.05f;
    public float sensitivity = 0.3f;

    void Awake()
    {
        rightClickAction = new InputAction(
            type: InputActionType.Button,
            binding: "<Mouse>/rightButton"
        );

        clickAction = new InputAction(
            type: InputActionType.Button,
            binding: "<Mouse>/leftButton"
        );

        mouseDeltaAction = new InputAction(
            type: InputActionType.Value,
            binding: "<Mouse>/delta"
        );

        escapeAction = new InputAction(
            type: InputActionType.Button,
            binding: "<Keyboard>/escape"
        );

        interactAction = new InputAction(
            type: InputActionType.Button,
            binding: "<Keyboard>/e"
        );
    }

    void OnEnable()
    {
        rightClickAction.Enable();
        clickAction.Enable();
        mouseDeltaAction.Enable();
        escapeAction.Enable();
        interactAction.Enable();
    }

    void OnDisable()
    {
        rightClickAction.Disable();
        clickAction.Disable();
        mouseDeltaAction.Disable();
        escapeAction.Disable();
        interactAction.Disable();
    }

    void Start()
    {
        targetZoom = offset.z;
        currentZoom = offset.z;

        originalParent = transform.parent;
        originalLocalPos = transform.localPosition;
        originalLocalRot = transform.localRotation;

        Vector3 startEuler = transform.localRotation.eulerAngles;

        currentRotation.x = startEuler.y;
        currentRotation.y = startEuler.x;

        targetRotation = currentRotation;
    }

    void Update()
    {

        Debug.Log("SCRIPT RUNNING");
        CheckForNearbyObject();
        HandleInteraction();

        if (inspecting)
        {
            uiPanel.Show();
        }
        else
        {
            uiPanel.Hide();
        }

        if (inspecting && target != null)
        {
            HandleInspectCamera();
        }

        if (escapeAction.triggered)
        {
            ExitInspect();
        }
    }
    

    void CheckForNearbyObject()
    {
        if (inspecting)
        {
            promptUI.SetActive(false);
            return;
        }
        
        Collider[] hits = Physics.OverlapSphere(
            player.position,
            inspectRange
        );

        Debug.Log("Hits: " + hits.Length);

        foreach (Collider hit in hits)
        {
            Debug.Log("Found object: " + hit.name);

            if (hit.CompareTag("Inspectable"))
            {
                Debug.Log("FOUND INSPECTABLE");

                nearbyTarget = hit.transform;

                promptUI.SetActive(true);

                Renderer rend = nearbyTarget.GetComponent<Renderer>();

                if (rend != null)
                {
                    Vector3 pos = rend.bounds.center;

                    pos.y = rend.bounds.max.y + 0.15f;

                    pos += Camera.main.transform.forward * -0.15f;

                    promptUI.transform.position = pos;
                }

                return;
            }
        }

        promptUI.SetActive(false);
        nearbyTarget = null;
    }

    void HandleInteraction()
    {
        if (dragTool != null && dragTool.IsDragging)
            return;

        if (interactAction.triggered &&
            nearbyTarget != null &&
            !inspecting)
        {
            Debug.Log("Inspecting...");

            target = nearbyTarget;
            inspecting = true;
            playerMovement.enabled = false;
            foreach (Renderer rend in playerRenderers)
            {
                rend.enabled = false;
            }
            transform.SetParent(null);
        }
    }

    void HandleInspectCamera()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll != 0)
        {
            targetZoom += scroll * zoomSpeed * 0.1f;

            targetZoom = Mathf.Clamp(
                targetZoom,
                maxZoom,
                minZoom
            );
        }

        currentZoom = Mathf.SmoothDamp(
            currentZoom,
            targetZoom,
            ref zoomVelocity,
            0.1f
        );

        Vector3 zoomOffset = new Vector3(
            offset.x,
            offset.y,
            currentZoom
        );

        Vector3 desiredPosition =
            target.position + zoomOffset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime * moveSpeed
        );

        transform.LookAt(target);

        if (clickAction.IsPressed())
        {
            if (dragTool == null || !dragTool.IsDragging)
            {
                Vector2 mouseDelta =
                    mouseDeltaAction.ReadValue<Vector2>();

                float mouseX = mouseDelta.x;

                target.Rotate(
                    Vector3.up,
                    -mouseX * rotateSpeed * Time.deltaTime,
                    Space.World
                );
            }
        }
    }

    void ExitInspect()
    {
        inspecting = false;
        playerMovement.enabled = true;
        foreach (Renderer rend in playerRenderers)
        {
            rend.enabled = true;
        }
        target = null;

        if (activePrompt != null)
        {
            activePrompt.SetActive(false);
        }

        transform.SetParent(originalParent);

        transform.localPosition = originalLocalPos;
        transform.localRotation = originalLocalRot;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            inspectRange
        );
    }
    
}

