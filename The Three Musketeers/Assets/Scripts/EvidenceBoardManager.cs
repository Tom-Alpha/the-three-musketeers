using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvidenceBoardManager : MonoBehaviour
{
    public DropZone[] dropZones;
    
    public string lemurSolution;
    
    public SuccessScreen successScreen;
    
    public string lorikeetSolution;
    
    public GameObject solutionTextObject;
    public GameObject helpButton;
    
    public GameObject solutionBox;
    
    public TMP_Text solutionText;
    
    public Transform solutionPoint;

    public GameObject linePrefab;

    private List<GameObject> activeLines =
        new List<GameObject>();

    public void ClearDropZones()
    {
        foreach (DropZone zone in dropZones)
        {
            zone.currentEvidence = null;
        }
    }
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
        
        foreach (DropZone zone in dropZones)
        {
            Debug.Log(
                zone.name + " contains: " +
                (zone.currentEvidence != null
                    ? zone.currentEvidence.name
                    : "NULL")
            );
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

        // Show solution after 3 correct clues
        if (correctPoints.Count >= 3)
        {
            CreateLine(
                correctPoints[correctPoints.Count - 1],
                solutionPoint
            );

            ShowSolution();
        }
            }
    
    
    public void ResetBoardVisuals()
    {
        ClearLines();

        solutionBox.SetActive(false);

        solutionText.text = "";
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
    public void HelpAnimal()
    {
        Debug.Log("Help button clicked!");
        if (successScreen == null)
        {
            Debug.LogError("SUCCESS SCREEN IS NULL!");
        }
        else
        {
            Debug.Log("SuccessScreen found");
            successScreen.ShowEndScreen();
        };
    }
    
    void ShowSolution()
    {
        Debug.Log("ShowSolution called!");

        solutionBox.SetActive(true);

        if (CaseManager.currentCase == CaseManager.CaseType.Parrot)
        {
            solutionText.text = lorikeetSolution;
        }
        else if (CaseManager.currentCase == CaseManager.CaseType.Monkey)
        {
            solutionText.text = lemurSolution;
        }

        Debug.Log("Text set to: " + solutionText.text);
    }
}   