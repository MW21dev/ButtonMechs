using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenuScript : MonoBehaviour
{
    public ButtonSlot menuUseButtonSlot;

    public Button useButton;

    public Color defaultGreen;

    public GameObject whitePanel;
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    public TMP_Text versionText;

    public string version;

    private void Start()
    {
        defaultGreen = new Color(0.1768868f, 0.7075472f, 0.2922177f);

        


        versionText.SetText("V" + version);
    }

    private void Update()
    {
        if (menuUseButtonSlot.eqquipedButton == null)
        {
            useButton.interactable = false;
            useButton.image.color = Color.gray;
        }
        else
        {
            useButton.interactable = true;
            useButton.image.color = defaultGreen;
        }
    }

    public void UseAbility()
    {
        AbilityButtonScript buttonAbility = menuUseButtonSlot.eqquipedButton.gameObject.GetComponent<AbilityButtonScript>();

        buttonAbility.UseMenuAbility();
        SoundManager.Instance.PlayUISound(0);
    }

    public void ClosePanel()
    {
        if(settingsPanel.GetComponent<CanvasGroup>().alpha == 1f)
        {
            settingsPanel.GetComponent<CanvasGroup>().alpha = 0f;
            settingsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            whitePanel.GetComponent<CanvasGroup>().alpha = 0f;
        }
        else if(creditsPanel.GetComponent<CanvasGroup>().alpha == 1f)
        {
            creditsPanel.GetComponent<CanvasGroup>().alpha = 0f;
            creditsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            whitePanel.GetComponent<CanvasGroup>().alpha = 0f;

        }
        
        SoundManager.Instance.PlayUISound(0);

    }
}
