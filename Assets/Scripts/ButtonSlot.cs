using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSlot : MonoBehaviour, IDropHandler
{
    public int maxChildCount;

    public bool empty;

    public GameObject eqquipedButton;

    public void Update()
    {
        if(transform.childCount > 0)
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
