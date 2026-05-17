using UnityEngine;
using UnityEngine.InputSystem;

public class EvidenceSystem : MonoBehaviour
{
    public Transform player;

    public float pickupRange = 3f;

    public GameObject promptUI;

    private Transform nearbyEvidence;

    void Update()
    {
        CheckForEvidence();

        if (
            nearbyEvidence != null &&
            Keyboard.current.eKey.wasPressedThisFrame
        )
        {
            CollectEvidence();
        }
    }

    void CheckForEvidence()
    {
        nearbyEvidence = null;

        Collider[] hits = Physics.OverlapSphere(
            player.position,
            pickupRange
        );

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Evidence"))
            {
                nearbyEvidence = hit.transform;

                promptUI.SetActive(true);

                Renderer rend =
                    nearbyEvidence.GetComponent<Renderer>();

                if (rend != null)
                {
                    Vector3 pos = rend.bounds.center;

                    pos.y = rend.bounds.max.y + 0.15f;

                    promptUI.transform.position = pos;
                }

                return;
            }
        }

        promptUI.SetActive(false);
    }

    void CollectEvidence()
    {
        Debug.Log(
            "Collected evidence: " +
            nearbyEvidence.name
        );

        nearbyEvidence.gameObject.SetActive(false);

        promptUI.SetActive(false);

        nearbyEvidence = null;
    }
}