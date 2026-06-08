using UnityEngine;
using UnityEngine.InputSystem;

public class SuccessScreen : MonoBehaviour
{
    public GameObject successScreen;

    public GameObject lemurSuccessImage;
    public GameObject lorikeetSuccessImage;

    private bool showing = false;

    public void ShowEndScreen()
    {
        Debug.Log("ShowEndScreen called!");

        successScreen.SetActive(true);

        lemurSuccessImage.SetActive(false);
        lorikeetSuccessImage.SetActive(false);

        if (CaseManager.currentCase == CaseManager.CaseType.Monkey)
        {
            Debug.Log("Showing lemur image");
            lemurSuccessImage.SetActive(true);
        }
        else if (CaseManager.currentCase == CaseManager.CaseType.Parrot)
        {
            Debug.Log("Showing lorikeet image");
            lorikeetSuccessImage.SetActive(true);
        }

        showing = true;
    }

    void Update()
    {
        if (showing &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            successScreen.SetActive(false);
            showing = false;
        }
    }
}