using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public GameObject parrotEvidence;
    public GameObject monkeyEvidence;
    public GameObject alligatorEvidence;

    public void LoadParrot()
    {
        DisableAll();

        parrotEvidence.SetActive(true);

        Debug.Log("PARROT CASE");
    }

    public void LoadMonkey()
    {
        DisableAll();

        monkeyEvidence.SetActive(true);

        Debug.Log("MONKEY CASE");
    }

    public void LoadAlligator()
    {
        DisableAll();

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