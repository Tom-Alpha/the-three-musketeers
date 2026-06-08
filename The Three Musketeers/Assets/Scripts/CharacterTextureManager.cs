using UnityEngine;

public class CharacterTextureManager : MonoBehaviour
{
    [Header("Materials")]
    public Material skinMaterial;
    public Material eyeMaterial;
    public Material mouthMaterial;

    [Header("Textures")]
    public Texture2D[] skinTextures;
    public Texture2D[] eyeTextures;
    public Texture2D[] mouthTextures;

    private int skinIndex;
    private int eyeIndex;
    private int mouthIndex;

    void Start()
    {
        ApplyTextures();
    }

    // SKIN
    public void NextSkin()
    {
        skinIndex = (skinIndex + 1) % skinTextures.Length;
        ApplyTextures();
    }

    public void PreviousSkin()
    {
        skinIndex--;

        if (skinIndex < 0)
            skinIndex = skinTextures.Length - 1;

        ApplyTextures();
    }

    // EYES
    public void NextEyes()
    {
        eyeIndex = (eyeIndex + 1) % eyeTextures.Length;
        ApplyTextures();
    }

    public void PreviousEyes()
    {
        eyeIndex--;

        if (eyeIndex < 0)
            eyeIndex = eyeTextures.Length - 1;

        ApplyTextures();
    }

    // MOUTH
    public void NextMouth()
    {
        mouthIndex = (mouthIndex + 1) % mouthTextures.Length;
        ApplyTextures();
    }

    public void PreviousMouth()
    {
        mouthIndex--;

        if (mouthIndex < 0)
            mouthIndex = mouthTextures.Length - 1;

        ApplyTextures();
    }

    void ApplyTextures()
    {
        skinMaterial.mainTexture = skinTextures[skinIndex];
        eyeMaterial.mainTexture = eyeTextures[eyeIndex];
        mouthMaterial.mainTexture = mouthTextures[mouthIndex];
    }
}