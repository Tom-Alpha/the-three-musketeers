using UnityEngine;

// 1. We create a custom class to hold the prefab PLUS its adjustment settings
[System.Serializable]
public class HairStyle
{
    [Tooltip("The 3D model prefab for this hair.")]
    public GameObject prefab;
    
    [Tooltip("Tweak this to fix floating or clipping hair.")]
    public Vector3 positionOffset = Vector3.zero;
    
    [Tooltip("Tweak this to fix backwards or sideways hair.")]
    public Vector3 rotationOffset = Vector3.zero;
    
    [Tooltip("Tweak this if the hair is too huge or too tiny.")]
    public Vector3 scale = Vector3.one; // Default is (1, 1, 1)
}

public class PlayerAppearanceManager : MonoBehaviour
{
    [Header("Attachment Points")]
    public Transform headAttachmentPoint;

    [Header("Available Hairstyles")]
    // 2. We use our new custom class instead of a simple GameObject array
    public HairStyle[] hairStyles;

    private GameObject currentHairInstance;
    
    // 3. We now remember which number we are currently looking at
    private int currentHairIndex = 0; 

    private void Start()
    {
        // Equip the first hair automatically when the game starts (optional)
        if (hairStyles.Length > 0)
        {
            UpdateHairDisplay();
        }
    }

    /// <summary>
    /// Call this from your "Right Arrow" or "Next" button in the UI.
    /// </summary>
    public void NextHair()
    {
        if (hairStyles.Length == 0) return;

        currentHairIndex++; // Move forward one number
        
        // If we go past the end of the list, loop back to the first hair (0)
        if (currentHairIndex >= hairStyles.Length)
        {
            currentHairIndex = 0;
        }
        
        UpdateHairDisplay();
    }

    /// <summary>
    /// Call this from your "Left Arrow" or "Previous" button in the UI.
    /// </summary>
    public void PreviousHair()
    {
        if (hairStyles.Length == 0) return;

        currentHairIndex--; // Move backward one number
        
        // If we go below 0, loop around to the very last hair in the list
        if (currentHairIndex < 0)
        {
            currentHairIndex = hairStyles.Length - 1;
        }
        
        UpdateHairDisplay();
    }

    /// <summary>
    /// Handles the actual deleting and spawning of the 3D models.
    /// </summary>
    private void UpdateHairDisplay()
    {
        // Delete old hair
        if (currentHairInstance != null)
        {
            Destroy(currentHairInstance);
        }

        // Get the data for the currently selected hair
        HairStyle selectedHair = hairStyles[currentHairIndex];
        
        if (selectedHair.prefab != null)
        {
            // Spawn the new hair
            currentHairInstance = Instantiate(selectedHair.prefab, headAttachmentPoint);

            // 4. Apply our custom fixes so it sits properly!
            currentHairInstance.transform.localPosition = selectedHair.positionOffset;
            currentHairInstance.transform.localEulerAngles = selectedHair.rotationOffset;
            currentHairInstance.transform.localScale = selectedHair.scale;
        }
    }
}