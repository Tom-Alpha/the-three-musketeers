using UnityEngine;
using UnityEngine.InputSystem;

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
        if (inputActions.Player.Click.triggered)
        {
            Debug.Log("Click detected");

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