using UnityEngine;
using System.Collections;

public class CardUnlockNotification : MonoBehaviour
{
    public static CardUnlockNotification Instance;

    public RectTransform popup;

    public float slideTime = 0.3f;
    public float visibleTime = 2f;

    private Vector2 hiddenPos;
    private Vector2 shownPos;

    private Coroutine currentRoutine;

    private void Awake()
    {
        Instance = this;

        shownPos = popup.anchoredPosition;
        hiddenPos = shownPos + new Vector2(500f, 0);

        popup.anchoredPosition = hiddenPos;
    }

    public void ShowNotification()
    {


        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(
            NotificationRoutine()
        );
    }

    IEnumerator NotificationRoutine()
    {
        float t = 0f;

        while (t < slideTime)
        {
            t += Time.deltaTime;

            popup.anchoredPosition =
                Vector2.Lerp(
                    hiddenPos,
                    shownPos,
                    t / slideTime
                );

            yield return null;
        }

        yield return new WaitForSeconds(
            visibleTime
        );

        t = 0f;

        while (t < slideTime)
        {
            t += Time.deltaTime;

            popup.anchoredPosition =
                Vector2.Lerp(
                    shownPos,
                    hiddenPos,
                    t / slideTime
                );

            yield return null;
        }
    }
}