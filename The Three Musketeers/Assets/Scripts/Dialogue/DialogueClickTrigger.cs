using UnityEngine;

public class DialogueClickTrigger : MonoBehaviour
{
    public DialogueLine[] lines;
    public bool triggerOnlyOnce = false;
    private bool hasTriggered = false;

    void OnMouseDown()
    {
        if (triggerOnlyOnce && hasTriggered)
            return;

        hasTriggered = true;
        DialogueManager.Instance.ShowSequence(lines);
    }
}
