using UnityEngine;

public class BookPageManager : MonoBehaviour
{
    private BookTab _currentTab;
    
    public GameObject mapPage;
    public GameObject evidencePage;
    public GameObject customizationPage;
    public GameObject collectionPage;

    public Transform tabMap;
    public Transform tabEvidence;
    public Transform tabCustomization;
    public Transform tabCollection;

    Vector3 _mapPos;
    Vector3 _evidencePos;
    Vector3 _customizationPos;
    Vector3 _collectionPos;

    Vector3 _raisedOffset = new Vector3(0, 0.02f, 0);
    
    public void SetActiveTab(BookTab tab)
    {
        _currentTab = tab;

        // reset visuals
        tabMap.GetComponent<BookTab>().SetSelected(false);
        tabEvidence.GetComponent<BookTab>().SetSelected(false);
        tabCustomization.GetComponent<BookTab>().SetSelected(false);
        tabCollection.GetComponent<BookTab>().SetSelected(false);

        // activate selected
        _currentTab.SetSelected(true);

        // switch pages
        switch (tab.tabType)
        {
            case BookTab.TabType.Map:
                ShowMap();
                break;

            case BookTab.TabType.Evidence:
                ShowEvidence();
                break;

            case BookTab.TabType.Customization:
                ShowCustomization();
                break;

            case BookTab.TabType.Collection:
                ShowCollection();
                break;
        }
    }

    void Start()
    {
        _mapPos = tabMap.position;
        _evidencePos = tabEvidence.position;
        _customizationPos = tabCustomization.position;
        _collectionPos = tabCollection.position;

        ShowMap();
    }

    void ResetAll()
    {
        mapPage.SetActive(false);
        evidencePage.SetActive(false);
        customizationPage.SetActive(false);
        collectionPage.SetActive(false);

        tabMap.position = _mapPos;
        tabEvidence.position = _evidencePos;
        tabCustomization.position = _customizationPos;
        tabCollection.position = _collectionPos;
    }

    public void ShowMap()
    {
        ResetAll();
        mapPage.SetActive(true);
        tabMap.position = _mapPos + _raisedOffset;
    }

    public void ShowEvidence()
    {
        ResetAll();
        evidencePage.SetActive(true);
        tabEvidence.position = _evidencePos + _raisedOffset;
    }

    public void ShowCustomization()
    {
        ResetAll();
        customizationPage.SetActive(true);
        tabCustomization.position = _customizationPos + _raisedOffset;
    }

    public void ShowCollection()
    {
        ResetAll();
        collectionPage.SetActive(true);
        tabCollection.position = _collectionPos + _raisedOffset;
    }
}
