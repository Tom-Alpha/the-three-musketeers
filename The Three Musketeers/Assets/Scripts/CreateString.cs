using UnityEngine;

public class CreateString : MonoBehaviour
{
    public GameObject stringPrefab;

    void CreateStringBetweenPins(PinConnection a, PinConnection b)
    {
        Debug.Log("STRING CREATED");

        GameObject stringObj =
            Instantiate(stringPrefab, transform);

        RectTransform rect =
            stringObj.GetComponent<RectTransform>();

        rect.localScale = Vector3.one;

        Vector3 start = a.transform.position;
        Vector3 end = b.transform.position;

        Vector3 direction = end - start;

        float distance = direction.magnitude;

        rect.position = (start + end) / 2f;

        rect.sizeDelta = new Vector2(distance, 10f);

        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;

        rect.rotation =
            Quaternion.Euler(0, 0, angle);
    }
}