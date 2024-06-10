using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleGreed : AbilityModule
{
    public List<AbilityButtonScript> b;

    public bool isFirstGold;
    public bool isSecondGold;
    public bool isThirdGold;

    public new void Update()
    {
        Mathf.Clamp(b.Count, 0, 3);

        foreach (var button in GameManager.Instance.buttonSlots)
        {
            ButtonSlot buttonSlot = button.GetComponent<ButtonSlot>();

            if (buttonSlot.eqquipedButton != null)
            {
                var ab = buttonSlot.eqquipedButton.GetComponent<AbilityButtonScript>();
                if (ab.type == AbilityButtonScript.Category.gold)
                {
                    if (b.Count <= 2)
                    {
                        b.Add(ab);

                    }
                }
                else
                {
                    b.Clear();
                }
            }
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
        if(b.Count == 3)
        {
            player.playerCurrentMoney += 3;
            b.Clear();
        }
    }
}
