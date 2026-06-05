using UnityEngine;
using UnityEngine.InputSystem; // Explicitly using the New Input System!

public class MagnifyingGlass : MonoBehaviour
{
    [Header("Raycast Settings")]
    [Tooltip("Put all clues on a specific layer to save performance")]
    public LayerMask evidenceLayer;
    public float scanDistance = 100f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Safety check to make sure a mouse is connected
        if (Mouse.current == null) return;

        // Get the exact mouse position using the New Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // 1. Create a Ray passing precisely through the mouse coordinates
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        // 2. Shoot the Raycast!
        if (Physics.Raycast(ray, out hit, scanDistance, evidenceLayer))
        {
            EvidenceData clue = hit.collider.GetComponent<EvidenceData>();

            // 3. If we hit a clue and it hasn't been found yet...
            if (clue != null && !clue.isFound)
            {
                clue.isFound = true; // Mark as found to prevent spamming

                // 4. Turn on the visual evidence and the draggable script
                if (clue.evidenceSprite != null) clue.evidenceSprite.enabled = true;
                if (clue.draggableScript != null) clue.draggableScript.enabled = true;

                // 5. Send Debug messages based on state
                if (clue.evidenceState == EvidenceState.Correct)
                {
                    Debug.Log($"<color=green>Correct Clue Found:</color> {clue.evidenceName}");
                }
                else
                {
                    Debug.Log($"<color=red>Incorrect Clue Found:</color> {clue.evidenceName}");
                }
            }
        }
    }
}