using UnityEngine;

public class OutlineSimple : MonoBehaviour
{
    public Renderer objectRenderer;
    public Color outlineColor = Color.white;
    public float outlineScale = 1f;

    private GameObject outlineObject;

    void Start()
    {
        CreateOutline();
        SetOutline(false);
    }

    void CreateOutline()
    {
        outlineObject = new GameObject("Outline");
        outlineObject.transform.SetParent(transform);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.localRotation = Quaternion.identity;
        outlineObject.transform.localScale = Vector3.one * outlineScale;

        var meshFilter = outlineObject.AddComponent<MeshFilter>();
        var meshRenderer = outlineObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = GetComponent<MeshFilter>().mesh;

        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = outlineColor;

        meshRenderer.material = mat;
    }

    public void SetOutline(bool enabled)
    {
        if (outlineObject != null)
            outlineObject.SetActive(enabled);
    }
}