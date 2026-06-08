using UnityEngine;

public class LoriInteractPrompt : MonoBehaviour
{

    public GameObject interactPrompt;
    private bool playerInRange = false;
    private bool SeeLori = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER WORKED");

        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!SeeLori)
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
        }
    }
}
