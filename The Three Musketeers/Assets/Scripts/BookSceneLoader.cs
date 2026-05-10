using UnityEngine;
using UnityEngine.SceneManagement;

public class BookSceneLoader : MonoBehaviour
{
    public void LoadBookScene()
    {
        SceneManager.LoadScene("BookScene");
    }
}