using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadAnimation : MonoBehaviour
{
    public Sprite[] images;
    public CanvasGroup canvasGroup;
    public GameObject endScreen;

    public Image image;

    public float frameTime = 0.05f;

    
    public void LaunchTurnOn()
    {
        canvasGroup.alpha = 1;
        StartCoroutine(TurnOn());

    }

    public IEnumerator TurnOn()
    {
        for (int i = 0; i < images.Length; i++)
        {
            image.sprite = images[i];

            if (i == images.Length - 1)
            {
                var a = endScreen.GetComponent<CanvasGroup>();
                a.alpha = 1f;
                a.blocksRaycasts = true;
                a.interactable = true;


            }

            yield return new WaitForSeconds(frameTime);
        }
    }
}
