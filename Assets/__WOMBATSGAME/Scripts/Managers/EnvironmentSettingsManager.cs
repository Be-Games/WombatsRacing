using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSettingsManager : MonoBehaviour
{
    public GameObject nightLightingGO,dayLightingGO;

    private void Start()
    {
        if(GameManager.Instance.lightingMode == 1)
            DayMode();
        if(GameManager.Instance.lightingMode == 2)
            NightMode();
    }

    public void NightMode()
    {
        nightLightingGO.SetActive(true);
        dayLightingGO.SetActive(false);
        RenderSettings.fogDensity = 0.017f;
    }
    
    public void DayMode()
    {
        nightLightingGO.SetActive(false);
        dayLightingGO.SetActive(true);
        RenderSettings.fogDensity = 0f;
    }
    
}
