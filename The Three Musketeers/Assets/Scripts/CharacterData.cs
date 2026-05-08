using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public static CharacterData instance;

    public int hairIndex;
    public int eyesIndex;
    public int mouthIndex;
    public int skinIndex;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}