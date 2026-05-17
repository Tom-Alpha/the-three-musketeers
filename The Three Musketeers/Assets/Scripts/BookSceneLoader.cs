using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BookSceneLoader : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("BookScene");
        }
    }
}