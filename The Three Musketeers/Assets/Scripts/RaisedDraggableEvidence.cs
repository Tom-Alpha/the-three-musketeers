using UnityEngine;

public class RaiseDraggableEvidence : MonoBehaviour
{
    private SpriteRenderer sr;

    private string originalLayer;
    private int originalOrder;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        originalLayer = sr.sortingLayerName;
        originalOrder = sr.sortingOrder;

        sr.sortingLayerName = "DraggingEvidence";
        sr.sortingOrder = 100;
    }

    void OnMouseUp()
    {
        sr.sortingLayerName = originalLayer;
        sr.sortingOrder = originalOrder;
    }
}