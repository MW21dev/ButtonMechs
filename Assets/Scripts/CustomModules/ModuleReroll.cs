using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleReroll : AbilityModule
{
    bool modified;
    public int priceModification
    {
        get
        {
            return ShopManager.Instance.startRerollCost -= 1;
        }
    }

    public new void Update()
    {
        if (!inShop && !modified)
        {
            ShopManager.Instance.startRerollCost = priceModification;
            modified = true;
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

}
