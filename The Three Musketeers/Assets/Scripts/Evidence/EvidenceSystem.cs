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
                    nearbyEvidence.GetComponentInChildren<Renderer>();

                if (rend != null)
                {
                    Vector3 pos =
                        rend.bounds.center +
                        Vector3.up * 0.75f;

                    promptUI.transform.position = pos;
                }

                return;
            }
        }

        promptUI.SetActive(false);
    }

    void CollectEvidence()
    {
       

        EvidenceLink link =
            nearbyEvidence.GetComponent<EvidenceLink>();

        if (link != null && link.linkedEvidence != null)
        {
            EvidenceObject evidence =
                link.linkedEvidence;

            if (
                evidence.spriteRendererToEnable != null
            )
            {
                evidence.spriteRendererToEnable.enabled =
                    true;
            }

            if (
                evidence.draggableEvidence != null
            )
            {
                evidence.draggableEvidence.enabled =
                    true;
            }
        }

        Destroy(nearbyEvidence.gameObject);

        promptUI.SetActive(false);

        nearbyEvidence = null;
    }
}