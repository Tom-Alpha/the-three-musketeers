using UnityEngine;
// not to be confused with the camera script
// testing raycasting with inspection
public class InspectionSystem : MonoBehaviour
{
    public UIDragTool tool;

    private Inspectable current;

    void Update()
    {
        if (!tool.IsDragging)
        {
            ClearCurrent();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Inspectable inspectable = hit.collider.GetComponent<Inspectable>();

            if (inspectable != current)
            {
                ClearCurrent();
                current = inspectable;

                if (current != null)
                    current.Show();
            }
        }
        else
        {
            ClearCurrent();
        }
    }

    void ClearCurrent()
    {
        if (current != null)
        {
            current.Hide();
            current = null;
        }
    }
}