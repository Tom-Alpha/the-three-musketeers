using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CamelSceneReturn : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("CamelScene");
        }
    }
}