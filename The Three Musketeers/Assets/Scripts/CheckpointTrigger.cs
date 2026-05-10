using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public ParrotMinigame parrot;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            parrot.AdvanceWaypoint();

            gameObject.SetActive(false);
        }
    }
}