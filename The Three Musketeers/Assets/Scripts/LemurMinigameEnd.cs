using UnityEngine;

public class LemurMinigameEnd : MonoBehaviour
{
    public LemurThrower thrower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thrower.StopThrowing();

            Debug.Log("Minigame Complete");
        }
    }
}