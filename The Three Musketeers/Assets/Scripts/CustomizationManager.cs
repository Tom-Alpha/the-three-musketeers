using UnityEngine;
using UnityEngine.UI;

public class CustomizationManager : MonoBehaviour
{
    [Header("Character Parts")] 
    public Image hairImage;
    public Image eyesImage;
    public Image mouthImage;
    public Image skinImage;
    
    [Header("Options")]
    public Sprite[] hairOptions;
    public Sprite[] eyesOptions;
    public Sprite[] mouthOptions;
    public Sprite[] skinOptions;

    private CharacterData data;
    
    void Start()
    {
        data = CharacterData.instance;
        UpdateCharacter();
    }

    void UpdateCharacter()
    {
        hairImage.sprite = hairOptions[data.hairIndex];
        eyesImage.sprite = eyesOptions[data.eyesIndex];
        mouthImage.sprite = mouthOptions[data.mouthIndex];
        skinImage.sprite = skinOptions[data.skinIndex];
    }
    
    //hair
    public void NextHair()
    {
        if (hairOptions == null || hairOptions.Length == 0) return;

        data.hairIndex++;
        if (data.hairIndex >= hairOptions.Length)
            data.hairIndex = 0;

        UpdateCharacter();
    }

    public void PrevHair()
    {
        if (hairOptions == null || hairOptions.Length == 0) return;

        data.hairIndex--;
        if (data.hairIndex < 0)
            data.hairIndex = hairOptions.Length - 1;

        UpdateCharacter();
    
    }
    
    //eyes
    public void NextEyes()
    {
        if (eyesOptions == null || eyesOptions.Length == 0) return;

        data.eyesIndex++;
        if (data.eyesIndex >= eyesOptions.Length)
            data.eyesIndex = 0;

        UpdateCharacter();
    }

    public void PrevEyes()
    {
        if (eyesOptions == null || eyesOptions.Length == 0) return;

        data.eyesIndex--;
        if (data.eyesIndex < 0)
            data.eyesIndex = eyesOptions.Length - 1;

        UpdateCharacter();
    }
    
    // MOUTH
    public void NextMouth()
    {
        if (mouthOptions == null || mouthOptions.Length == 0) return;

        data.mouthIndex++;
        if (data.mouthIndex >= mouthOptions.Length)
            data.mouthIndex = 0;

        UpdateCharacter();
    }

    public void PrevMouth()
    {
        if (mouthOptions == null || mouthOptions.Length == 0) return;

        data.mouthIndex--;
        if (data.mouthIndex < 0)
            data.mouthIndex = mouthOptions.Length - 1;

        UpdateCharacter();
    }

    // SKIN
    public void NextSkin()
    {
        if (skinOptions == null || skinOptions.Length == 0) return;

        data.skinIndex++;
        if (data.skinIndex >= skinOptions.Length)
            data.skinIndex = 0;

        UpdateCharacter();
    }

    public void PrevSkin()
    {
        if (skinOptions == null || skinOptions.Length == 0) return;

        data.skinIndex--;
        if (data.skinIndex < 0)
            data.skinIndex = skinOptions.Length - 1;

        UpdateCharacter();
    }
}