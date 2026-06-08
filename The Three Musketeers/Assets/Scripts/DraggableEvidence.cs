using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableEvidence : MonoBehaviour
{
    bool dragging;
    Vector3 offset;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Mouse Click");
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
            Camera.main.ScreenPointToRay(
                Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit: " + hit.transform.name);

            if (hit.transform == transform)
            {
                Debug.Log("CLICKED");

                dragging = true;

                offset =
                    transform.position -
                    MouseWorldPosition();
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing");
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
                Camera.main.WorldToScreenPoint(
                    transform.position).z);

        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}