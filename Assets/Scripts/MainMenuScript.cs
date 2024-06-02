using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public ButtonSlot menuUseButtonSlot;

    public Button useButton;

    public Color defaultGreen;

    private void Start()
    {
        defaultGreen = new Color(0.1768868f, 0.7075472f, 0.2922177f);

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
    }
}
