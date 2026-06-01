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
        ClearLines();

        List<Transform> correctPoints =
            new List<Transform>();

        // Collect correct evidence points
        foreach (DropZone zone in dropZones)
        {
            if (zone.currentEvidence == null)
                continue;

            bool correct =
                zone.currentEvidence.evidenceState
                == zone.requiredState;

            if (correct)
            {
                correctPoints.Add(
                    zone.connectionPoint
                );
            }
        }

        // Only show lines if ALL slots are filled
        foreach (DropZone zone in dropZones)
        {
            if (zone.currentEvidence == null)
            {
                return;
            }
        }

        // Connect correct evidence together
        for (int i = 0; i < correctPoints.Count - 1; i++)
        {
            CreateLine(
                correctPoints[i],
                correctPoints[i + 1]
            );
        }

        // Connect last correct evidence to solution
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
            Instantiate(
            linePrefab,
            transform
        );

        activeLines.Add(lineObj);

        LineRenderer lr =
            lineObj.GetComponent<LineRenderer>();

        // Slightly above the board
        Vector3 offset =
            new Vector3(0, 0.1f, 0);

        lr.SetPosition(
            0,
            start.position + offset
        );

        lr.SetPosition(
            1,
            end.position + offset
        );
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