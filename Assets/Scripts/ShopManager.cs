using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public int rerollCost = 1;

    public Button rerollButton;

    public static ShopManager Instance;
    private Color defaultGreen;

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
    private void Start()
    {
        defaultGreen = new Color(0.1768868f, 0.7075472f, 0.2922177f);
    }

    private void Update()
    {
        if (PlayerStats.Instance.playerCurrentMoney >= rerollCost)
        {
            rerollButton.interactable = true;
            rerollButton.image.color = defaultGreen;
        }
        else
        {
            rerollButton.interactable = false;
            rerollButton.image.color = Color.gray;
        }
    }
    public void Reroll()
    {
        if(PlayerStats.Instance.playerCurrentMoney >= rerollCost)
        {
            foreach (var slot in GameObject.FindGameObjectsWithTag("ShopSlot"))
            {
                ShopSlot shopSlot = slot.GetComponent<ShopSlot>();
                shopSlot.SetShopSlot();
            }

            PlayerStats.Instance.playerCurrentMoney -= rerollCost;
            rerollCost += 1;
        } 
    }
}
