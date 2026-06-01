using UnityEngine;
using UnityEngine.InputSystem;

public class BookSwitcher : MonoBehaviour
{
    public GameObject gameplayCamera;
    public GameObject bookCamera;
    public PlayerMovement playerMovement;

    private bool inBook = false;

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            inBook = !inBook;

            gameplayCamera.SetActive(!inBook);
            bookCamera.SetActive(inBook);

            playerMovement.enabled = !inBook;
        }
    }
}