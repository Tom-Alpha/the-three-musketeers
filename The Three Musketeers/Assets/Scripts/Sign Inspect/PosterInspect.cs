using UnityEngine;
using UnityEngine.InputSystem;

public class PosterInspect : MonoBehaviour
{
    public GameObject gameplayCamera;
    public GameObject posterCam;
    public PlayerMovement playerMovement;
    public GameObject playerModel;

    public GameObject interactPrompt;
    public GameObject exitPromptUI;

    private bool playerInRange = false;
    private bool SeePoster = false;

    private void Start()
    {
        interactPrompt.SetActive(false);

        if (exitPromptUI != null)
        {
            exitPromptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SeePoster = !SeePoster;

            gameplayCamera.SetActive(!SeePoster);
            posterCam.SetActive(SeePoster);

            playerMovement.enabled = !SeePoster;

            playerModel.SetActive(!SeePoster);

            interactPrompt.SetActive(!SeePoster);

            if (exitPromptUI != null)
            {
                exitPromptUI.SetActive(SeePoster);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER WORKED");

        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!SeePoster)
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactPrompt.SetActive(false);

            if (SeePoster)
            {
                SeePoster = false;

                gameplayCamera.SetActive(true);
                posterCam.SetActive(false);

                playerMovement.enabled = true;
                playerModel.SetActive(true);

                if (exitPromptUI != null)
                {
                    exitPromptUI.SetActive(false);
                }
            }
        }
    }
}