using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadCamelScene()
    {
        SceneManager.LoadScene("CamelScene");
    }
}