using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpinWheel : MonoBehaviour
{
    public Text[] winTextArray;
    public Button spinButton;
    public UIParticleSystem emitter;
    public Image WinPopup;
    private bool isStarted;
    private float[] sectorAngles;
    private float finalAngle;
    private float startAngle = 0;
    private float currentLerpRotationTime;
    private int selectedWinIndex = -1;
    private float sectorAngle = 30;
    bool isSpinning = false;
    float spinWin = 0;
    void Start()
    {
        SetWinValues();
    }

    void SetWinValues()
    {
        for(int i = 0; i< winTextArray.Length; i++)
        {
            winTextArray[i].transform.rotation = Quaternion.Euler(0, 0 , sectorAngle * -i);
            winTextArray[i].text =  ((i+1)*sectorAngle).ToString();
        }
    }

    void Update()
    {
        if (isStarted)
        {
            isSpinning = true;
            spinButton.interactable = false;
        }
        else
        {
            if(isSpinning)
            {
                Invoke("OnSpinCompleted", 1);
                isSpinning = false;
            }
        }

        if (!isStarted)
            return;

        float maxLerpRotationTime = 4f;
        currentLerpRotationTime += Time.deltaTime;

        if (currentLerpRotationTime > maxLerpRotationTime || this.transform.eulerAngles.z == finalAngle)
        {
            currentLerpRotationTime = maxLerpRotationTime;
            isStarted = false;
            startAngle = finalAngle % 360;
        }

        float t = currentLerpRotationTime / maxLerpRotationTime;
        t = t * t * t * (t * (6f * t - 15f) + 10f);
        float angle = Mathf.Lerp(startAngle, finalAngle, t);
        this.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void TurnWheel()
    {
        currentLerpRotationTime = 0f;
        sectorAngles = new float[] { 360, 330, 300, 270, 240, 210, 180, 150, 120, 90, 60, 30};
        int fullCircles = 5;
        selectedWinIndex = UnityEngine.Random.Range(0, sectorAngles.Length);
        float randomFinalAngle = sectorAngles[selectedWinIndex];
        finalAngle = -(fullCircles * 360 + randomFinalAngle);
        isStarted = true;
    }

    void OnSpinCompleted()
    {
        spinWin = (selectedWinIndex + 1) * sectorAngle;
        WinPopup.GetComponent<FadeInPopup>().FadeIn(spinWin);
        emitter.Play();
    }

    public void OnWinPopupClosed()
    {
        emitter.Stop();
        Invoke("EnableSpinButton", 1);
    }

    void EnableSpinButton()
    {
        spinButton.interactable = true;
    }
}
