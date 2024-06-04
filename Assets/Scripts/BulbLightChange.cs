using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulbLightChange : MonoBehaviour
{
    public Image[] bulbs;

    public bool isActive;

    public GameObject shopPanel;

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

    private void Update()
    {
        
    }

    public IEnumerator LightOn()
    {
        if (shopPanel.activeSelf)
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
        
    }

    public IEnumerator LightOff()
    {
        if (shopPanel.activeSelf)
        {
            for (int i = 0; i < bulbs.Length; i++)
            {
                bulbs[i].GetComponent<Image>().sprite = bulbOff;

                if (i == bulbs.Length - 1)
                {
                    Invoke("StartOn", lightSpeed);
                }

                yield return new WaitForSeconds(lightSpeed);
            }
        }
    }
    
}
