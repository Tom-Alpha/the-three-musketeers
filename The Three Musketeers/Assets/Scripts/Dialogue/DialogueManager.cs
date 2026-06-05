using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    
    public static DialogueManager Instance;

    [SerializeField] private GameObject subtitlePanel;
    [SerializeField] TMP_Text subtitleText;

    private void Awake()
    {
        Instance = this;
        subtitlePanel.SetActive(false);
    }

    public void ShowLine(string line, float duration = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(line, duration));
    }

    IEnumerator ShowRoutine(string line, float duration)
    {
        subtitlePanel.SetActive(true);
        subtitleText.text = line;
        
        yield return new WaitForSeconds(duration);
        
        subtitlePanel.SetActive(false);
    }

    public void ShowSequence(DialogueLine[] lines)
    {
        StopAllCoroutines();
        StartCoroutine(SequenceRoutine(lines));
    }

    IEnumerator SequenceRoutine(DialogueLine[] lines)
    {
        subtitlePanel.SetActive(true);

        foreach (var line in lines)
        {
            subtitleText.text = line.text;
            yield return new WaitForSeconds(line.duration);
        }

        subtitlePanel.SetActive(false);
    }
}
