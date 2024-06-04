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
            if(eqquipedButton.GetComponent<AbilityButtonScript>().type == AbilityButtonScript.Category.starter)
            {
                eqquipedButton.gameObject.transform.SetParent(eqquipedButton.GetComponent<DragDrop>().previousParent, false);
            }
            else
            {
                Invoke("DiscardButton", 0.1f);
            }
            
        }

        if (transform.childCount > 0)
        {
            eqquipedButton = transform.GetChild(0).gameObject;
            empty = false;
        }
        else
        {
            eqquipedButton = null;
            empty = true;
        }
    }

    private void DiscardButton()
    {
        var discardedButton = eqquipedButton;
        if (discardedButton != null)
        {
            GameManager.Instance.discardList.Add(discardedButton.gameObject.GetComponent<AbilityButtonScript>());
            discardedButton.gameObject.transform.SetParent(GameManager.Instance.discard.transform, false);
        }
        
        eqquipedButton = null;
    }

    

}
