using UnityEngine;

public class SignReader : MonoBehaviour
{
    public Transform viewPoint;
    public float signinteractionDistance = 3f;

    private Transform player;
    private Camera playerCamera;

    private Vector3 originalCamPos;
    private Quaternion originalCamRot;

    private bool reading = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = Camera.main;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (!reading)
        {
            if (distance <= signinteractionDistance && Input.GetKeyDown(KeyCode.E))
            {
                EnterReadingMode();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                ExitReadingMode();
            }
        }
    }

    void EnterReadingMode()
    {
        reading = true;

        originalCamPos = playerCamera.transform.position;
        originalCamRot = playerCamera.transform.rotation;

        playerCamera.transform.position = viewPoint.position;
        playerCamera.transform.rotation = viewPoint.rotation;

        // Disable player controls here
        // playerController.enabled = false;
    }

    void ExitReadingMode()
    {
        reading = false;

        playerCamera.transform.position = originalCamPos;
        playerCamera.transform.rotation = originalCamRot;

        // Re-enable player controls here
        // playerController.enabled = true;
    }
}