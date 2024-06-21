using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TurnOnAnimation : MonoBehaviour
{
    public Sprite[] images;

    public Image image;

    public float frameTime = 0.05f;

    private void Start()
    {
        Invoke("LaunchTurnOn", 0.2f);
    }

    void LaunchTurnOn()
    {
        StartCoroutine(TurnOn());

    }

    public void LaunchTurnOff()
    {
        StartCoroutine(TurnOff());
    }

    public IEnumerator TurnOn()
    {
        for (int i = 0; i < images.Length; i++)
        {
            image.sprite = images[i];

            if(i == images.Length - 1)
            {
                SoundManager.Instance.PlayMusicSound(0);
                gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(frameTime);
        }
    }

    public IEnumerator TurnOff()
    {
        for (int i = images.Length - 1; i > -1; i--)
        {
            image.sprite = images[i];

            if (i == 0)
            {
                Application.Quit();
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
            }

            yield return new WaitForSeconds(frameTime);
        }
    }
}
