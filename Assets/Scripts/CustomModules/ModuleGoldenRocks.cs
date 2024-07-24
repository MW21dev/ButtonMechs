
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModuleGoldenRocks : AbilityModule
{
    bool isUsed;
    
    private void OnDestroy()
    {
        ObjectScript.OnRockDestroy -= RockDestroyed;
    }

    public new void Update()
    {
        

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

        if (!inShop)
        {
            foreach (var Rock in GameObject.FindGameObjectsWithTag("Border"))
            {
                ObjectScript obj = Rock.GetComponent<ObjectScript>();

                if (!obj.mapBorder)
                {
                    if (!isUsed && !inShop)
                    {
                        ObjectScript.OnRockDestroy += RockDestroyed;
                        isUsed = true;
                    }

                }
            }
        }
        
    }

    void RockDestroyed()
    {
        PlayerStats.Instance.playerCurrentMoney += 1;
    }

}
