using UnityEngine;

public class ParrotMinigame : MonoBehaviour
{
    private Animator animator;

    public Transform[] waypoints;

    public float flySpeed = 5f;

    private int currentWaypoint = 0;

    private bool isFlying = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (!isFlying) return;

        Transform target = waypoints[currentWaypoint];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            flySpeed * Time.deltaTime
        );
    }

    public void StartMinigame()
    {
        currentWaypoint = 1;
        isFlying = true;
        
        // Start takeoff animation
        animator.SetTrigger("TakeOffTrigger");

        // Enter flying state after takeoff
        animator.SetBool("BirdIsFlying", true);
    }

    public void AdvanceWaypoint()
    {
        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
        {
            Debug.Log("Parrot caught!");
            isFlying = false;
            
            // Return to idle
            animator.SetBool("BirdIsFlying", false);
        }
    }
}