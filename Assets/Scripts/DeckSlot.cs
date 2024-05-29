using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSlot : MonoBehaviour
{
    private void Update()
    {
        if(transform.childCount > 0)
        {
            int childNmb = transform.childCount;
            for (int i = 0; i < childNmb; i++)
            {
                DragDrop deckSlot = gameObject.transform.GetChild(i).GetComponent<DragDrop>();
                deckSlot.draggable = false;
            }
        }
    }


}
