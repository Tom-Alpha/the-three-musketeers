using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null) 
            Debug.Log("BillboardUI Running");
        {
            transform.forward = Camera.main.transform.forward;
        }
    }

}

