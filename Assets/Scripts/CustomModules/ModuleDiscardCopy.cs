using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModuleDiscardCopy : AbilityModule
{
    bool used;

    private void Start()
    {
        DiscardSlot.OnDiscard += CopyDiscard;
        LevelManager.Instance.OnLaunchLevel += AbilityRestart;

        description.text = $"{abilityName}" + "\n" + "\n " + $"{abilityDescription}";
        abilityText.SetActive(false);
    }

    public void CopyDiscard()
    {
        if (used)
        {
            return;
        }

        DiscardSlot slot = GameObject.FindObjectOfType<DiscardSlot>();
        if (slot.eqquipedButton != null)
        {
            var discardedButton = slot.eqquipedButton.gameObject.GetComponent<AbilityButtonScript>();
            var discardedButtonNew = Instantiate(discardedButton);
            discardedButtonNew.gameObject.transform.SetParent(GameManager.Instance.discard.transform, false);
            GameManager.Instance.discardList.Add(discardedButtonNew.gameObject.GetComponent<AbilityButtonScript>());
        }

        used = true;
    }

    public void AbilityRestart()
    {
        used = false;
    }

    private void OnDestroy()
    {
        DiscardSlot.OnDiscard -= CopyDiscard;
        LevelManager.Instance.OnLaunchLevel -= AbilityRestart;

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

    public override void UseAbility(PlayerStats player)
    {
        
    }
}
