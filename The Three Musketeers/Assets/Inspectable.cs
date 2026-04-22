using UnityEngine;
// dont worry about this one man its unfunctional rn
public class Inspectable : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    public Color highlightColor = Color.red;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void Show()
    {
        rend.material.color = highlightColor;
    }

    public void Hide()
    {
        rend.material.color = originalColor;
    }
}