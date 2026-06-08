using UnityEngine;
using UnityEngine.InputSystem;

public class MagnifyingGlass : MonoBehaviour
{
    [Header("Raycast Settings")]
    public LayerMask evidenceLayer;
    public float scanDistance = 100f;

    [Header("Inspection")]
    public InspectSystem inspectSystem;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (inspectSystem == null)
            return;

        if (!inspectSystem.IsInspecting)
            return;

        if (Mouse.current == null)
            return;

        Vector2 mousePos =
            Mouse.current.position.ReadValue();

        Ray ray =
            mainCamera.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if (Physics.Raycast(
            ray,
            out hit,
            scanDistance,
            evidenceLayer))
        {
            EvidenceData clue =
                hit.collider.GetComponent<EvidenceData>();

            if (clue != null && !clue.isFound)
            {
                clue.isFound = true;

                if (clue.evidenceSprite != null)
                {
                    clue.evidenceSprite.enabled = true;
                }

                if (clue.draggableScript != null)
                {
                    clue.draggableScript.enabled = true;
                }

                if (CardUnlockNotification.Instance != null)
                {
                    CardUnlockNotification.Instance.ShowNotification();
                }

                if (clue.evidenceState ==
                    EvidenceState.Correct)
                {
                    Debug.Log(
                        $"<color=green>Correct Clue Found:</color> {clue.evidenceName}"
                    );
                }
                else
                {
                    Debug.Log(
                        $"<color=red>Incorrect Clue Found:</color> {clue.evidenceName}"
                    );
                }
            }
        }
    }
}