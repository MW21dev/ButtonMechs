using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulbLightChange : MonoBehaviour
{
    public Image[] bulbs;

    public Sprite bulbOn;
    public Sprite bulbOff;

    public float lightSpeed;

    private void Start()
    {
        StartOn();
    }

    public void StartOn()
    {
        StartCoroutine(LightOn());
    }

    public void StartOff()
    {
        StartCoroutine(LightOff());
    }

    public IEnumerator LightOn()
    {
        for (int i = 0; i < bulbs.Length; i++)
        {
            bulbs[i].GetComponent<Image>().sprite = bulbOn;
            if (i == bulbs.Length - 1)
            {
                Invoke("StartOff", lightSpeed);
            }
            yield return new WaitForSeconds(lightSpeed);
        }
    }

    public IEnumerator LightOff()
    {
        for (int i = 0; i < bulbs.Length; i++)
        {
            bulbs[i].GetComponent<Image>().sprite = bulbOff;

            if(i == bulbs.Length - 1)
            {
                Invoke("StartOn", lightSpeed);
            }
            
            yield return new WaitForSeconds(lightSpeed);
        }
    }
}
