using UnityEngine;
using System.Collections;
// code for the Magnifying glass container to move down and up
public class UISlidePanel : MonoBehaviour
{
    private RectTransform rect;

    public Vector2 shownPosition;
    public Vector2 hiddenPosition;

    public float duration = 0.3f;

    private Coroutine currentAnim;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Show()
    {
        StartMove(shownPosition);
    }

    public void Hide()
    {
        StartMove(hiddenPosition);
    }

    void StartMove(Vector2 target)
    {
        if (currentAnim != null)
            StopCoroutine(currentAnim);

        currentAnim = StartCoroutine(SmoothMove(target));
    }

    IEnumerator SmoothMove(Vector2 target)
    {
        Vector2 start = rect.anchoredPosition;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;


            t = 1 - Mathf.Pow(1 - t, 3);

            rect.anchoredPosition = Vector2.Lerp(start, target, t);

            yield return null;
        }

        rect.anchoredPosition = target;
    }
}