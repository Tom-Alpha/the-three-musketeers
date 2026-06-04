using UnityEngine;

public class LemurThrower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Animator lemurAnimator;
    public Transform throwPoint;
    public Transform player;

    public float throwForce = 15f;
    public float throwInterval = 2f;

    private bool active = false;

    public void StartThrowing()
    {
        active = true;

        InvokeRepeating(nameof(ThrowProjectile), 1f, throwInterval);
    }

    void ThrowProjectile()
    {
        if (!active) return;

        lemurAnimator.SetTrigger("Throw");
        
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is null!");
            return;
        }

        GameObject proj = Instantiate(
            projectilePrefab,
            throwPoint.position,
            Quaternion.identity
        );

        Rigidbody rb = proj.GetComponent<Rigidbody>();

        Vector3 dir = (player.position - throwPoint.position).normalized;

        rb.AddForce(dir * throwForce, ForceMode.Impulse);
    }

    public void StopThrowing()
    {
        active = false;

        CancelInvoke(nameof(ThrowProjectile));
    }
}