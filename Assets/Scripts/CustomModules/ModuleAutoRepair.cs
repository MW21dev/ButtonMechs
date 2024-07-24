using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleAutoRepair : AbilityModule
{
    private void Start()
    {
        LevelManager.Instance.OnLaunchLevel += Heal;

        description.text = $"{abilityName}" + "\n" + "\n " + $"{abilityDescription}";
        abilityText.SetActive(false);

    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnLaunchLevel -= Heal;

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
    }

    void Heal()
    {
        if(PlayerStats.Instance.playerCurrentHp < PlayerStats.Instance.playerMaxHp)
        {
            PlayerStats.Instance.playerCurrentHp += 1;
            UIScript.Instance.ChangeHp(PlayerStats.Instance.playerCurrentHp);

        }
    }
}

