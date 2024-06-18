using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCredits : AbilityButtonScript
{
    private GameObject creditsPanel;
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

        creditsPanel = GameObject.Find("CreditsPanel");
        whitePanel = GameObject.Find("WhitePanel");
        whitePanel.GetComponent<CanvasGroup>().alpha = 0f;
        whitePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

        creditsPanel.GetComponent<CanvasGroup>().alpha = 0f;
        creditsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void UseMenuAbility()
    {
        creditsPanel.GetComponent<CanvasGroup>().alpha = 1f;
        creditsPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;

        whitePanel.GetComponent<CanvasGroup>().alpha = 1f;
        
    }
}
