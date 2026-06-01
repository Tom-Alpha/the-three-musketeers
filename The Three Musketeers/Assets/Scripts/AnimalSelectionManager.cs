using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalSelectionManager : MonoBehaviour
{
    public CaseManager caseManager;

    public GameObject animalPanel;

    public GameObject parrotButton;
    public GameObject monkeyButton;
    public GameObject alligatorButton;

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
                GameObject clicked =
                    hit.collider.gameObject;

                Debug.Log(clicked.name);

                if (clicked == parrotButton)
                {
                    caseManager.LoadParrot();
                    animalPanel.SetActive(false);
                }

                else if (clicked == monkeyButton)
                {
                    caseManager.LoadMonkey();
                    animalPanel.SetActive(false);
                }

                else if (clicked == alligatorButton)
                {
                    caseManager.LoadAlligator();
                    animalPanel.SetActive(false);
                }
            }
        }
    }
}