using UnityEngine;


public class BookPageManager : MonoBehaviour
{
    [Header("Pages (ROOT OBJECTS)")] public GameObject mapPage;
    public GameObject evidencePage;
    public GameObject customizationPage;
    public GameObject collectionPage;

    [Header("Tabs (3D objects)")] public Transform tabMap;
    public Transform tabEvidence;
    public Transform tabCustomization;
    public Transform tabCollection;

    Vector3 _mapPos;
    Vector3 _evidencePos;
    Vector3 _customizationPos;
    Vector3 _collectionPos;
    private Vector3 _raisedOffset = new Vector3(0, 0.02f, 0);

    void Start()
    {
        _mapPos = tabMap.position;
        _evidencePos = tabEvidence.position;
        _customizationPos = tabCustomization.position;
       _collectionPos = tabCollection.position;

        ShowMap();
    }

    void ResetPages()
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
        ResetPages();
        mapPage.SetActive(true);
        tabMap.position = _mapPos + _raisedOffset;
        tabMap.position = _mapPos +_raisedOffset;
    }

    public void ShowEvidence()
    {
        ResetPages();
        evidencePage.SetActive(true);
        tabEvidence.position = _evidencePos + _raisedOffset;
    }

    public void ShowCustomization()
    {
        ResetPages();
        customizationPage.SetActive(true);
        tabCustomization.position = _customizationPos + _raisedOffset;
    }

    public void ShowCollection()
    {
        ResetPages();
        collectionPage.SetActive(true);
        tabCollection.position = _collectionPos + _raisedOffset;

    }
}
