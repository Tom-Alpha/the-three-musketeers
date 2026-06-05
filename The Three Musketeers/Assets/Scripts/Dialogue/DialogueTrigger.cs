using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{

    public DialogueLine[] lines;

    public bool triggerOnlyOnce = false;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        if (triggerOnlyOnce && hasTriggered)
            return;
        
        hasTriggered = true;
        
        DialogueManager.Instance.ShowSequence(lines);
    }
    
}
