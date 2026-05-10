using UnityEngine;
using UnityEngine.InputSystem;

public class ParrotInteraction : MonoBehaviour
{
    public ParrotMinigame parrot;

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby)
        {
            Debug.Log("Player nearby");

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("E PRESSED");

                parrot.StartMinigame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            Debug.Log("PLAYER ENTERED");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            Debug.Log("PLAYER EXITED");
        }
    }
}