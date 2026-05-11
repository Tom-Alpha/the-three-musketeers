using UnityEngine;

public class PinConnection : MonoBehaviour
{
    [HideInInspector]
    public int connectionCount = 0;

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void OnEnable()
    {
        ConnectionManager.Instance.RegisterPin(this);
    }
}