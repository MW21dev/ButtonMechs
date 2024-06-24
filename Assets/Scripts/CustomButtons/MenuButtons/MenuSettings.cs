using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class MenuSettings : AbilityButtonScript
{
    private GameObject settingsPanel;
    private GameObject whitePanel;

    private new void Start()
    {
        description.text = $"{abilityName}" + "\n" + "\n " + $"{abilityDescription}";
        abilityText.SetActive(false);
        if (typeText != null && typeDescription != null)
        {
            typeDescription.text = $"{type}" + "\n " + "\n" + $"{abilityTypeDescription}";
            typeText.SetActive(false);
        }

        settingsPanel = GameObject.Find("SettingsPanel");
        whitePanel = GameObject.Find("WhitePanel");
        whitePanel.GetComponent<CanvasGroup>().alpha = 0f;
        whitePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

        settingsPanel.GetComponent<CanvasGroup>().alpha = 0f;
        settingsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void UseMenuAbility()
    {
        settingsPanel.GetComponent<CanvasGroup>().alpha = 1f;
        settingsPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;

        whitePanel.GetComponent<CanvasGroup>().alpha = 1f;
        
    }
}
