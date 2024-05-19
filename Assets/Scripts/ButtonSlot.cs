using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSlot : MonoBehaviour, IDropHandler
{
    public int maxChildCount;

    public GameObject eqquipedButton;

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount < maxChildCount)
        {
            GameObject dropped = eventData.pointerDrag;
            DragDrop dragDropButton = dropped.GetComponent<DragDrop>();
            dragDropButton.parentAfterDrag = transform;

            eqquipedButton = dropped;
        }
        
    }
}
