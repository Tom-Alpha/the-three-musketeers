using UnityEngine;

public class LemurMinigameStart : MonoBehaviour
{
    public LemurThrower thrower;

    private bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        if (started) return;

        if (other.CompareTag("Player"))
        {
            started = true;

            thrower.StartThrowing();

            Debug.Log("Minigame Started");
        }
    }
}