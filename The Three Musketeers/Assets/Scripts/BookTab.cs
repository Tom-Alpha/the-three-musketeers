using UnityEngine;

public class BookTab : MonoBehaviour
{
    public enum TabType
    {
        Map,
        Evidence,
        Customization,
        Collection
    }

    public TabType tabType;
    public BookPageManager manager;

    private OutlineSimple outline;

    void Awake()
    {
        outline = GetComponent<OutlineSimple>();
    }

    public void OnTabClicked()
    {
        manager.SetActiveTab(this);
    }

    public void SetSelected(bool selected)
    {
        if (outline != null)
            outline.SetOutline(selected);
    }
}