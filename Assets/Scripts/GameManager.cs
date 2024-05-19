using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public GameObject[] buttonSlots;
    public GameObject[] bulbs;

    public static GameManager Instance;

    public Sprite bulbOn;
    public Sprite bulbOff;

    public bool playerTurn;
    public bool enemyTurn;



    private void Awake()
    {
        if(Instance == null)
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
        
    }

    public void EndTurn()
    {
        StartCoroutine(NextAction(0));
        playerTurn = false;
        enemyTurn = true;
    }

   

    public void UseButton(int buttonNumber)
    {
        ButtonSlot slot;

        slot = buttonSlots[buttonNumber].GetComponent<ButtonSlot>();
        var ability = slot.eqquipedButton.GetComponent<AbilityButtonScript>();

        ability.UseAbility(PlayerStats.Instance);
    }


    public void LightBulb(int bulbNumber)
    {
        GameObject bulb;
        bulb = bulbs[bulbNumber];
        Image bulbImage = bulb.GetComponent<Image>();

        bulbImage.sprite = bulbOn;
    }

    public void Reset()
    {
        foreach (var bulb in bulbs)
        {
            bulb.GetComponent<Image>().sprite = bulbOff;
        }
    }

    IEnumerator NextAction(int startingItem)
    {
        for (int i = startingItem;  i < buttonSlots.Length; i++)
        {
            UseButton(i);
            LightBulb(i);

            if(i == buttonSlots.Length - 1)
            {
                Invoke("Reset", 1f);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
