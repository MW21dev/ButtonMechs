using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : ButtonSlot
{
    public Button buyButton;
    public Image image;
    private Color defaultGreen;

    public enum Type
    {
        button,
        module,
    }

    public Type type;

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

        if (type == Type.button)
        {
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
        }
        else if (type == Type.module)
        {
            if (eqquipedButton != null && PlayerStats.Instance.playerCurrentMoney >= eqquipedButton.gameObject.GetComponent<AbilityModule>().abilityPrice)
            {
                buyButton.interactable = true;
                buyButton.image.color = defaultGreen;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            else
            {
                buyButton.interactable = false;
                buyButton.image.color = Color.gray;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

            }
        }

       

    }

    public void SetShopSlot()
    {
        int rndB = Random.Range(0, GameManager.Instance.shopButtons.Length);
        int rndM = Random.Range(0, GameManager.Instance.shopModules.Count);
        
        if(type == Type.button)
        {
            if (empty)
            {
                var button = Instantiate(GameManager.Instance.shopButtons[rndB], this.transform);
                int childNmb = transform.childCount;
                for (int i = 0; i < childNmb; i++)
                {
                    DragDrop buttonSlot = gameObject.transform.GetChild(i).GetComponent<DragDrop>();
                    buttonSlot.draggable = false;
                }
            }
            else if (!empty)
            {
                Destroy(eqquipedButton.gameObject);
                var button = Instantiate(GameManager.Instance.shopButtons[rndB], this.transform);
                int childNmb = transform.childCount;
                for (int i = 0; i < childNmb; i++)
                {
                    DragDrop buttonSlot = gameObject.transform.GetChild(i).GetComponent<DragDrop>();
                    buttonSlot.draggable = false;
                }
            }
        }
        else if (type == Type.module)
        {
            if (empty)
            {
                if(GameManager.Instance.shopModules.Count != 0)
                {
                    var module = Instantiate(GameManager.Instance.shopModules[rndM], this.transform);
                    GameManager.Instance.shopModules.Remove(module);
                }
                
            }
            else if (!empty)
            {
                Destroy(eqquipedButton.gameObject);
                var button = Instantiate(GameManager.Instance.shopModules[rndM], this.transform);
                
            }

        }


    }

    public void BuyButton()
    {
        
        if(type == Type.button)
        {
            PlayerStats.Instance.playerCurrentMoney -= eqquipedButton.gameObject.GetComponent<AbilityButtonScript>().abilityPrice;
            eqquipedButton.transform.SetParent(GameManager.Instance.deck.transform, false);
            GameManager.Instance.UpdateDeck();

        }
        else if (type == Type.module)
        {
            PlayerStats.Instance.playerCurrentMoney -= eqquipedButton.gameObject.GetComponent<AbilityModule>().abilityPrice;
            string n = eqquipedButton.gameObject.GetComponent<AbilityModule>().abilityName;
            
            foreach (var item in GameManager.Instance.shopModules)
            {
                if (item.GetComponent<AbilityModule>().abilityName == n)
                {
                    GameManager.Instance.shopModules.Remove(item);
                    break;
                }
            }

            eqquipedButton.transform.SetParent(GameManager.Instance.modules.transform, false);
            GameManager.Instance.UpdateModules();

        }

        SoundManager.Instance.PlayUISound(6);


    }
}
