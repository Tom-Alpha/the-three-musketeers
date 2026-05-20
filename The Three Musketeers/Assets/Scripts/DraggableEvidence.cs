using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableEvidence : MonoBehaviour
{
    Camera cam;
    bool dragging;
    Vector3 offset;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryStartDrag();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            dragging = false;
        }

        if (dragging)
        {
            transform.position =
                MouseWorldPosition() + offset;
        }
    }

    void TryStartDrag()
    {
        Ray ray =
            cam.ScreenPointToRay(
                Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                Debug.Log("CLICKED");

                dragging = true;

                offset =
                    transform.position -
                    MouseWorldPosition();
            }
        }
    }

    Vector3 MouseWorldPosition()
    {
        Vector2 mouse =
            Mouse.current.position.ReadValue();

        Vector3 screenPos =
            new Vector3(
                mouse.x,
                mouse.y,
                cam.WorldToScreenPoint(
                    transform.position).z);

        return cam.ScreenToWorldPoint(screenPos);
    }
}