using UnityEngine;
using UnityEngine.InputSystem;

public class InspectSystem : MonoBehaviour
{
    [Header("Player")]
    public PlayerMovement playerMovement;
    public Transform playerRoot;
    
    public Transform cameraPivot;
    public float orbitSpeed = 150f;
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
    
    [Header("Inspect UI")]
    public GameObject exitPromptUI;
    public bool IsInspecting
    {
        get { return inspecting; }
    }
    [Header("Prompt")]
    public GameObject promptUI;

    [Header("References")]
    public UIDragTool dragTool;
    public UISlidePanel uiPanel;

    private Transform target;
    private Transform nearbyTarget;
    private Quaternion originalRotation;
    private Animator inspectedAnimator;
    
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

        originalRotation = cameraPivot.localRotation;

        Vector3 startEuler = transform.localRotation.eulerAngles;

        currentRotation.x = startEuler.y;
        currentRotation.y = startEuler.x;

        targetRotation = currentRotation;
    }

    void Update()
    {
        if (inspecting)
        {
            if (promptUI != null)
                promptUI.SetActive(false);
            
            if (interactAction.triggered || escapeAction.triggered)
            {
                ExitInspect();
                return;
            }

            uiPanel.Show();

            if (exitPromptUI != null)
                exitPromptUI.SetActive(true);

            if (target != null)
            {
                HandleInspectCamera();
            }

            return;
        }

        uiPanel.Hide();

        if (exitPromptUI != null)
            exitPromptUI.SetActive(false);

        CheckForNearbyObject();
        HandleInteraction();

        if (Mouse.current.rightButton.isPressed)
        {
            float mouseX =
                Mouse.current.delta.ReadValue().x;

            cameraPivot.Rotate(
                Vector3.up,
                mouseX * orbitSpeed * Time.deltaTime,
                Space.Self
            );
        }
        else
        {
            cameraPivot.localRotation =
                Quaternion.Slerp(
                    cameraPivot.localRotation,
                    Quaternion.identity,
                    5f * Time.deltaTime
                );
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

        //Debug.Log("Hits: " + hits.Length);

        foreach (Collider hit in hits)
        {
            //Debug.Log("Found object: " + hit.name);

            if (hit.CompareTag("Inspectable"))
            {
                //Debug.Log("FOUND INSPECTABLE");

                nearbyTarget = hit.transform;

                promptUI.SetActive(true);

                Renderer rend =
                    nearbyTarget.GetComponentInChildren<Renderer>();

                if (rend != null)
                {
                    Vector3 pos = rend.bounds.center;

                    pos.y = rend.bounds.max.y + 0.45f;

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
            // Save camera state BEFORE inspection
            originalParent = transform.parent;
            originalLocalPos = transform.localPosition;
            originalLocalRot = transform.localRotation;

            target = nearbyTarget;

            inspectedAnimator =
                target.GetComponentInParent<Animator>();

            if (inspectedAnimator != null)
            {
                Debug.Log("Animator found: " + inspectedAnimator.name);
                inspectedAnimator.enabled = false;
            }
            else
            {
                Debug.Log("No Animator found");
            }

            inspecting = true;
            playerMovement.enabled = false;

            SetPlayerVisible(false);

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
    void SetPlayerVisible(bool visible)
    {
        Renderer[] renderers =
            playerRoot.GetComponentsInChildren<Renderer>(true);

        foreach (Renderer rend in renderers)
        {
            rend.enabled = visible;
        }
    }
    void ExitInspect()
    {
        
        inspecting = false;
        playerMovement.enabled = true;

        SetPlayerVisible(true);
        
        if (exitPromptUI != null)
        {
            exitPromptUI.SetActive(false);
        }
        if (inspectedAnimator != null)
        {
            inspectedAnimator.enabled = true;
        }
        target = null;

        if (activePrompt != null)
        {
            activePrompt.SetActive(false);
        }

        // Restore camera state
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

