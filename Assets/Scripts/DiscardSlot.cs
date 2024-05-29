using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscardSlot : ButtonSlot, IDropHandler
{
    private new void Update()
    {
        if(eqquipedButton != null)
        {
            Invoke("DiscardButton", 0.1f);
        }
    }

    private void DiscardButton()
    {
        var buttonToDiscard = eqquipedButton.gameObject;
        Destroy(buttonToDiscard);
    }
}
