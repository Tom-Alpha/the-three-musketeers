using UnityEngine;
using UnityEngine.InputSystem;

public class CaseSelector : MonoBehaviour
{
    public GameObject animalSelectionPanel;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray =
                Camera.main.ScreenPointToRay(
                    Mouse.current.position.ReadValue()
                );

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.name);

                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("CENTER PHOTO CLICKED");

                    animalSelectionPanel.SetActive(true);
                }
            }
        }
    }
}