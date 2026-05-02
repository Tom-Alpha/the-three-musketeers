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

    public void OnTabClicked()   
    {
        Debug.Log("Tab clicked: " + tabType);

        switch (tabType)
        {
            case TabType.Map:
                manager.ShowMap();
                break;

            case TabType.Evidence:
                manager.ShowEvidence();
                break;

            case TabType.Customization:
                manager.ShowCustomization();
                break;

            case TabType.Collection:
                manager.ShowCollection();
                break;
        }
    }
}