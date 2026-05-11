using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
    public Transform stringContainer;
    public static ConnectionManager Instance;

    [Header("Connection Settings")]
    public float maxConnectionDistance = 500f;
    public int maxConnectionsPerPin = 2;


    [Header("Prefabs")]
    public GameObject stringPrefab;

    private List<PinConnection> activePins =
        new List<PinConnection>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPin(PinConnection newPin)
    {
        activePins.Add(newPin);

        TryCreateConnections(newPin);
    }

    void TryCreateConnections(PinConnection newPin)
    {
        foreach (PinConnection otherPin in activePins)
        {
            if (otherPin == newPin)
                continue;

            if (newPin.connectionCount >= maxConnectionsPerPin)
                return;

            if (otherPin.connectionCount >= maxConnectionsPerPin)
                continue;

            float distance =
                Vector2.Distance(
                    newPin.GetPosition(),
                    otherPin.GetPosition());

            if (distance <= maxConnectionDistance)
            {
                CreateString(newPin, otherPin);

                newPin.connectionCount++;
                otherPin.connectionCount++;
                Debug.Log("STRING CREATED");
            }
        }
    }

    void CreateString(PinConnection a, PinConnection b)
    {
        GameObject stringObj =
            Instantiate(stringPrefab, stringContainer);

        RectTransform rect =
            stringObj.GetComponent<RectTransform>();

        Vector3 direction =
            b.transform.localPosition -
            a.transform.localPosition;

        float distance = direction.magnitude;

        rect.sizeDelta =
            new Vector2(distance, 6f);

        rect.localPosition =
            (a.transform.localPosition + b.transform.localPosition) / 2f;

        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;

        rect.rotation =
            Quaternion.Euler(0, 0, angle);
    }
}