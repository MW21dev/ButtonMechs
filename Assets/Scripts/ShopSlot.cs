using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : ButtonSlot
{
    public Button buyButton;
    private Color defaultGreen;

    private void Start()
    {
        defaultGreen = new Color(0.1768868f, 0.7075472f, 0.2922177f);
    }

    private new void Update()
    {
        if (transform.childCount > 0)
        {
            eqquipedButton = transform.GetChild(0).gameObject;
            empty = false;

            
        }
        else
        {
            eqquipedButton = null;
            empty = true;
        }

        if (eqquipedButton != null && PlayerStats.Instance.playerCurrentMoney >= eqquipedButton.gameObject.GetComponent<AbilityButtonScript>().abilityPrice)
        {
            buyButton.interactable = true;
            buyButton.image.color = defaultGreen;
        }
        else
        {
            buyButton.interactable = false;
            buyButton.image.color = Color.gray;
        }

       

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetShopSlot();
        }
    }

    public void SetShopSlot()
    {
        int rnd = Random.Range(0, GameManager.Instance.shopButtons.Length);
        
        if (empty)
        {
            Instantiate(GameManager.Instance.shopButtons[rnd], this.transform);
            int childNmb = transform.childCount;
            for (int i = 0; i < childNmb; i++)
            {
                DragDrop buttonSlot = gameObject.transform.GetChild(i).GetComponent<DragDrop>();
                buttonSlot.draggable = false;
            }
        }
        else
        {
            Destroy(eqquipedButton.gameObject);
            Instantiate(GameManager.Instance.shopButtons[rnd], this.transform);
            int childNmb = transform.childCount;
            for (int i = 0; i < childNmb; i++)
            {
                DragDrop buttonSlot = gameObject.transform.GetChild(i).GetComponent<DragDrop>();
                buttonSlot.draggable = false;
            }
        }

    }

    public void BuyButton()
    {
        eqquipedButton.transform.SetParent(GameManager.Instance.deckList.transform, false);
        PlayerStats.Instance.playerCurrentMoney -= eqquipedButton.gameObject.GetComponent<AbilityButtonScript>().abilityPrice;
        GameManager.Instance.UpdateDeck();

    }
}
