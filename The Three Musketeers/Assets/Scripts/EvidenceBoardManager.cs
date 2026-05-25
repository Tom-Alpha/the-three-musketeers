using System.Collections.Generic;
using UnityEngine;

public class EvidenceBoardManager : MonoBehaviour
{
    public DropZone[] dropZones;

    public Transform solutionPoint;

    public GameObject linePrefab;

    private List<GameObject> activeLines =
        new List<GameObject>();

    public void RefreshConnections()
    {
        Debug.Log("Refreshing connections");
        ClearLines();

        List<Transform> correctPoints =
            new List<Transform>();

        foreach (DropZone zone in dropZones)
        {
            if (zone.currentEvidence == null)
                continue;

            bool correct =
                zone.currentEvidence.evidenceState
                == zone.requiredState;

            if (correct)
            {
                correctPoints.Add(zone.connectionPoint);
            }
        }
        foreach (DropZone zone in dropZones)
        {
            if (zone.currentEvidence == null)
            {
                return;
            }
        
        }
        for (int i = 0; i < correctPoints.Count - 1; i++)
        {
            CreateLine(
                correctPoints[i],
                correctPoints[i + 1]
            );
        }

        if (correctPoints.Count > 0)
        {
            CreateLine(
                correctPoints[correctPoints.Count - 1],
                solutionPoint
            );
        }
    }

   void CreateLine(Transform start, Transform end)
{
    GameObject lineObj =
        Instantiate(linePrefab);

    activeLines.Add(lineObj);

    LineRenderer lr =
        lineObj.GetComponent<LineRenderer>();

    Vector3 offset =
    new Vector3(0, 0.01f, 0);

    lr.SetPosition(0, start.position + offset);
    lr.SetPosition(1, end.position + offset);
}

    void ClearLines()
    {
        foreach (GameObject line in activeLines)
        {
            Destroy(line);
        }

        activeLines.Clear();
    }
}