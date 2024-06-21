using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider sfxVolume;

    public AudioMixer mainAudioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVolume.value);
    }

    public void ChangeMusicVoulme()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVolume.value);
    }
    public void ChangeSfxVoulme()
    {
        mainAudioMixer.SetFloat("SFXVol", sfxVolume.value);
    }
}
