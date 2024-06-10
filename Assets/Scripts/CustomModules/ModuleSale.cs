using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModuleSale : AbilityModule
{
	bool modified;

	bool isUsed;

	private void Start()
	{
        LevelManager.Instance.OnShopEnter += OnShopEnter;
		ShopManager.Instance.OnReroll += OnReroll;

        description.text = $"{abilityName}" + "\n" + "\n " + $"{abilityDescription}";
        abilityText.SetActive(false);
    }
    private void OnDestroy()
    {
        LevelManager.Instance.OnShopEnter -= OnShopEnter;
        ShopManager.Instance.OnReroll -= OnReroll;
    }
    private void OnShopEnter()
    {
        if (inShop)
        {
            return;
        }

		Invoke("ChangePrices", 0.1f);
    }

	private void OnReroll()
	{
		if (inShop)
		{
			return;
		}
		
		Invoke("ChangePrices", 0.1f);

    }

    void ChangePrices()
	{

        foreach (var slot in GameObject.FindGameObjectsWithTag("ShopSlot"))
        {
			
            ShopSlot shopSlot = slot.GetComponent<ShopSlot>();
			if (shopSlot.type == ShopSlot.Type.button)
			{
                if (shopSlot.eqquipedButton != null)
                {
                    var ab = shopSlot.eqquipedButton.GetComponent<AbilityButtonScript>();
                    ab.abilityPrice -= 1;
                }
            }
           

        }
    }

    public new void Update()
	{
		if(!inShop && !isUsed)
		{
            Invoke("ChangePrices", 0.1f);
			isUsed = true;
        }


        if (isHovering)
		{
			timeLeft -= Time.deltaTime;
		}
		if (timeLeft <= 0)
		{
			abilityText.SetActive(true);

			if (inShop)
			{
				abilityText.transform.localPosition = new Vector3(abilityText.transform.localPosition.x, 15f, abilityText.transform.localPosition.z);
			}
			else
			{
				abilityText.transform.localPosition = new Vector3(abilityText.transform.localPosition.x, -75f, abilityText.transform.localPosition.z);
			}
		}

		if (!isHovering)
		{
			abilityText.SetActive(false);
		}
	}

	public override void UseAbility(PlayerStats player)
	{
		modified = false;
	}

	
}
