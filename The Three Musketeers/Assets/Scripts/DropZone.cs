using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler
{
    [Header("Validation")]
public EvidenceType[] acceptedEvidenceTypes;

    [Header("Feedback")]
    public Image outlineImage;

    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    
    public GameObject pinObject;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject == null) return;

        EvidenceData evidence =
            droppedObject.GetComponent<EvidenceData>();

        if (evidence == null) return;

        bool correct = false;

        for (int i = 0; i < acceptedEvidenceTypes.Length; i++)
        {
            if (evidence.evidenceType == acceptedEvidenceTypes[i])
            {
                correct = true;
                break;
            }
        }

        RectTransform droppedRect =
            droppedObject.GetComponent<RectTransform>();

        droppedRect.anchoredPosition =
            GetComponent<RectTransform>().anchoredPosition;

        if (correct)
        {
            outlineImage.color = correctColor;

            pinObject.SetActive(true);

            Debug.Log("Correct Evidence!");
        }
        else
        {
            outlineImage.color = wrongColor;

            Debug.Log("Wrong Evidence!");
        }
    }
}
