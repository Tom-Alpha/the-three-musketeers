using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public ParrotMinigame parrot;

    // Which checkpoint number this is
    public int checkpointIndex;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (!other.CompareTag("Player")) return;

        // Only allow checkpoints in order
        if (checkpointIndex != parrot.requiredCheckpointIndex)
        {
            Debug.Log("Wrong checkpoint order!");
            return;
        }

        triggered = true;

        // Move to next expected checkpoint
        parrot.requiredCheckpointIndex++;

        parrot.AdvanceWaypoint();

        gameObject.SetActive(false);
    }
}