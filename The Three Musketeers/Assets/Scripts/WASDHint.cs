using UnityEngine;
public class WASDHint : MonoBehaviour
{
    public GameObject wasdHint;
    public InspectSystem inspectSystem;
    public BookSwitcher bookSwitcher;

    void Update()
    {
        wasdHint.SetActive(
            !inspectSystem.IsInspecting &&
            !bookSwitcher.IsBookOpen
        );
    }
}