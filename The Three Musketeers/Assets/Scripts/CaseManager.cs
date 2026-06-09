using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public GameObject parrotEvidence;
    public GameObject monkeyEvidence;
    public GameObject alligatorEvidence;
    public EvidenceBoardManager boardManager;
    
    public EvidenceBoardManager evidenceBoardManager;
    public enum CaseType
    {
        Parrot,
        Monkey,
        Alligator
    }
    
    public static CaseType currentCase;
    
    public void LoadParrot()
    {
        boardManager.ResetBoardVisuals();
        evidenceBoardManager.ClearDropZones();

        DisableAll();

        currentCase = CaseType.Parrot;

        parrotEvidence.SetActive(true);
    }

    public void LoadMonkey()
    {
        boardManager.ResetBoardVisuals();
        evidenceBoardManager.ClearDropZones();

        DisableAll();

        currentCase = CaseType.Monkey;

        monkeyEvidence.SetActive(true);

        Debug.Log("MONKEY CASE");
    }

    public void LoadAlligator()
    
    {
        boardManager.ResetBoardVisuals();
        evidenceBoardManager.ClearDropZones();

        DisableAll();

        currentCase = CaseType.Alligator;

        alligatorEvidence.SetActive(true);

        Debug.Log("ALLIGATOR CASE");
    }

    void DisableAll()
    {
        parrotEvidence.SetActive(false);
        monkeyEvidence.SetActive(false);
        alligatorEvidence.SetActive(false);
    }
}