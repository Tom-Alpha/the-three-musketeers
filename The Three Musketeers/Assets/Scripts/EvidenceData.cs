using UnityEngine;

public class EvidenceData : MonoBehaviour
{
    [Header("Clue Information")]
    public string evidenceName; 
    public EvidenceState evidenceState;
    
    [Header("Reveal Settings")]
    [Tooltip("Drag the Sprite Renderer you want to turn on here")]
    public SpriteRenderer evidenceSprite;
    
    [Tooltip("Drag the Draggable Evidence script you want to turn on here")]
    public Behaviour draggableScript; // "Behaviour" is a universal Unity term for components that can be enabled/disabled

    [HideInInspector]
    public bool isFound = false; 
}