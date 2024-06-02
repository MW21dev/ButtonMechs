using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject shopSlot;
    
    public GameObject buyText;
    public TMP_Text description;
    public TMP_Text price;

    public bool isHovering = false;
    public float timeToWait = 0.5f;
    float timeLeft;

    [TextArea]
    public string buyDescription;

    private void Start()
    {
        description.text = buyDescription;
        buyText.SetActive(false);
        price.text = "999" + "$";
    }
    void Update()
    {
        if (shopSlot.transform.childCount > 0)
        {
            price.text = shopSlot.transform.GetChild(0).GetComponent<AbilityButtonScript>().abilityPrice.ToString() + "$";
            if (isHovering)
            {
                timeLeft -= Time.deltaTime;
            }
            if (timeLeft <= 0)
            {
                buyText.SetActive(true);
            }
        }

        if (!isHovering)
        {
            buyText.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        timeLeft = timeToWait;
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}

