using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimation : MonoBehaviour
{
    public Sprite[] images;

    public Image image;

    public float frameTime = 0.05f;

    public GameObject loadingScreen;

    private void Start()
    {
        Invoke("LaunchTurnOn", 0.2f);
    }

    void LaunchTurnOn()
    {
        StartCoroutine(TurnOn());

    }
    public IEnumerator TurnOn()
    {
        for (int i = 0; i < images.Length; i++)
        {
            image.sprite = images[i];

            if (i == images.Length - 1)
            {
                loadingScreen.GetComponent<CanvasGroup>().alpha = 0f;
                loadingScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;

            }

            yield return new WaitForSeconds(frameTime);
        }
    }
}
