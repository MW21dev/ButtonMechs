using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public ButtonSlot menuUseButtonSlot;

    public Button useButton;

    private void Update()
    {
        if (menuUseButtonSlot.eqquipedButton == null)
        {
            useButton.interactable = false;
        }
        else
        {
            useButton.interactable = true;

        }
    }

    public void UseAbility()
    {
        AbilityButtonScript buttonAbility = menuUseButtonSlot.eqquipedButton.gameObject.GetComponent<AbilityButtonScript>();

        buttonAbility.UseMenuAbility();
    }
}
