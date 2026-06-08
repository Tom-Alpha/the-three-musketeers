using UnityEngine;
using UnityEngine.InputSystem;

public class HelpButtonClick : MonoBehaviour
{
    public EvidenceBoardManager boardManager;

    void Update()
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
                if (hit.collider.gameObject == gameObject)
                {
                    boardManager.HelpAnimal();
                }
            }
        }
    }
}
