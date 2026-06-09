using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject gameplayUI;
    public GameObject player;

    public Camera gameplayCamera;
    public Camera menuCamera;

    public void PlayGame()
    {
        Debug.Log("PLAY CLICKED");

        mainMenuCanvas.SetActive(false);

        gameplayUI.SetActive(true);
        player.SetActive(true);

        menuCamera.gameObject.SetActive(false);
        gameplayCamera.gameObject.SetActive(true);
    }
}