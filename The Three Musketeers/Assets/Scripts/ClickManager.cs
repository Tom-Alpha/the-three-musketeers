using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        // Check for click input
        if (inputActions.Player.Click.triggered)
        {
            // IMPORTANT: Ignore clicks when over UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Debug.Log("Click detected (NEW SYSTEM)");

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.name);

                BookTab tab = hit.collider.GetComponentInParent<BookTab>();

                if (tab != null)
                {
                    tab.OnTabClicked();
                }
            }
            else
            {
                Debug.Log("Raycast hit NOTHING");
            }
        }
    }
}