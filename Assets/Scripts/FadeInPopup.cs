using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInPopup : MonoBehaviour
{
    public Image popupBg;
    public Button closeButton;
    public SpinWheel spinWheelRef;
    public Text winText;
    public Text youWonText;
    void Start()
    {
        popupBg.canvasRenderer.SetAlpha(0.0f);
    }

    public void FadeIn(float win)
    {
        winText.text = win.ToString();
        popupBg.CrossFadeAlpha(1, 1, false);
        Invoke("ShowYouWonText", 1);
    }

    void ShowYouWonText()
    {
        youWonText.gameObject.SetActive(true);
        Invoke("ShowWinText", 0.5f);
    }
    
    void ShowWinText()
    {
        winText.gameObject.SetActive(true);
        ShowCloseButton();
    }

    void ShowCloseButton()
    {
        closeButton.gameObject.SetActive(true);
        closeButton.interactable = true;
    }

    public void ClosePopup()
    {
        popupBg.CrossFadeAlpha(0, 1, false);
        closeButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        youWonText.gameObject.SetActive(false);
        spinWheelRef.GetComponent<SpinWheel>().OnWinPopupClosed();
    }
}
