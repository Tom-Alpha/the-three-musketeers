using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
// dragging the magnifying glass from the container
// and it flying back like thors hammer
public class UIDragTool : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 originalPosition;

    public bool IsDragging { get; private set; }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;

        canvasGroup.blocksRaycasts = false;

 
        rectTransform.localScale = Vector3.one * 1.1f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    IEnumerator SmoothReturn()
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        float duration = 0.25f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;

   
            t = 1 - Mathf.Pow(1 - t, 3);

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, originalPosition, t);

            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;

 
        rectTransform.localScale = Vector3.one;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;

        canvasGroup.blocksRaycasts = true;

        StartCoroutine(SmoothReturn());
    }
}