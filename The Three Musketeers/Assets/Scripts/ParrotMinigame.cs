using UnityEngine;

public class ParrotMinigame : MonoBehaviour
{
    public Transform[] waypoints;

    public bool[] stopAtWaypoint;

    public float flySpeed = 5f;

 
    private int currentWaypoint = 0;

    
    private bool isFlying = false;

    private Animator animator;

    // Tracks which checkpoint is currently allowed
    public int requiredCheckpointIndex = 1;

    void Start()
    {
        animator = GetComponent<Animator>();

            animator = GetComponent<Animator>();

            gameObject.tag = "Untagged";

        // Make sure the parrot CANNOT be inspected at the start
        gameObject.tag = "Untagged";
    }

    void Update()
    {
        if (!isFlying) return;

        Transform target = waypoints[currentWaypoint];

        Vector3 direction = (target.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            transform.rotation =
                Quaternion.LookRotation(direction) *
                Quaternion.Euler(0, -110, 0);
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            flySpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 0.1f)
        {
            Debug.Log("Reached waypoint");

            if (stopAtWaypoint[currentWaypoint])
            {
                isFlying = false;

                animator.SetBool("IsFlying", false);
            }
            else
            {
                AdvanceWaypoint();
            }
        }
    }

    public void StartMinigame()
    {
        transform.position = waypoints[0].position;

        currentWaypoint = 1;

        requiredCheckpointIndex = 1;

        isFlying = true;

        animator.SetBool("IsFlying", true);

        animator.SetTrigger("TakeOff");

        // Remove inspectability while flying
        gameObject.tag = "Untagged";
    }

    // Called by checkpoints
    public void TryAdvanceCheckpoint(int checkpointIndex)
    {
        // Only allow the correct checkpoint
        if (checkpointIndex != requiredCheckpointIndex)
        {
            Debug.Log("Wrong checkpoint!");
            return;
        }

        Debug.Log("Correct checkpoint!");

        requiredCheckpointIndex++;

        AdvanceWaypoint();
    }

    public void AdvanceWaypoint()
    {
        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
        {
            Debug.Log("Parrot caught!");

            isFlying = false;

            animator.SetBool("IsFlying", false);

            // Make parrot inspectable after being caught
            gameObject.tag = "Inspectable";

            return;
        }

        isFlying = true;

        animator.SetBool("IsFlying", true);
    }
}