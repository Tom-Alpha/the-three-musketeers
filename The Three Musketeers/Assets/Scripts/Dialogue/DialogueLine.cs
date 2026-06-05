using UnityEngine;

[System.Serializable]
public struct DialogueLine
{
    [TextArea] public string text;
    public float duration;
}