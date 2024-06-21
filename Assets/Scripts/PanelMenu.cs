using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMenu : SettingsManager
{
    public GameObject menuPanel;

    public Button exitButton;

    

    private void Start()
    {
        masterVolume.value = SettingsManager.Instance.masterVolume.value;
        musicVolume.value = SettingsManager.Instance.musicVolume.value;
        sfxVolume.value = SettingsManager.Instance.sfxVolume.value;


    }

   
}
