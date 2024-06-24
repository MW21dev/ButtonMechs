using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip[] uISounds;
    public AudioClip[] gameSounds;
    public AudioClip[] tracks;

    public AudioSource audioSource;
    public AudioSource musicSource;


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

   

    private void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusicSound(int sound)
    {
        musicSource.clip = tracks[sound];
        musicSource.Play();
    }

    public void PlayUISound(int sound)
    {
        audioSource.clip = uISounds[sound];
        audioSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
