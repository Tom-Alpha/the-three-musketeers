using UnityEngine;

public class DropZone : MonoBehaviour
{
    [Header("Accepted State")]
    public EvidenceState requiredState;

    public Transform connectionPoint;

    public GameObject pinObject;

    public EvidenceBoardManager boardManager;

    public Vector3 snapOffset;

    [HideInInspector]
    public EvidenceData currentEvidence;

    private void OnTriggerEnter(Collider other)
    {
        EvidenceData evidence =
            other.GetComponent<EvidenceData>();

        if (evidence == null)
            return;

        bool correct =
            evidence.evidenceState == requiredState;

        other.transform.position =
            transform.position + snapOffset;

        currentEvidence = evidence;

        if (correct)
        {
            Debug.Log("Correct evidence placed!");

            if (pinObject != null)
                pinObject.SetActive(true);
        }
        else
        {
            Debug.Log("Wrong evidence!");
        }
        boardManager.RefreshConnections();
    }

}