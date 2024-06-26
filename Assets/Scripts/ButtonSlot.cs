

using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSlot : MonoBehaviour, IDropHandler
{
    public int maxChildCount;

    public bool empty;

    public bool isDiscard;

    public bool isShop;

    public GameObject eqquipedButton;

    public void Update()
    {
        if(transform.childCount > 0)
        {
            eqquipedButton = transform.GetChild(0).gameObject;
            DragDrop e = eqquipedButton.GetComponent<DragDrop>();
            e.previousParent = transform;
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
        if (isShop)
        {
            return;
        }
        
        if(transform.childCount < maxChildCount)
        {
            GameObject dropped = eventData.pointerDrag;
            DragDrop dragDropButton = dropped.GetComponent<DragDrop>();
            
            if (dragDropButton.draggable)
            {
                dragDropButton.parentAfterDrag = transform;

                eqquipedButton = dropped;
            }
            
            
        }

        
        if(!empty)
        {


            GameObject dropped = eventData.pointerDrag;
            DragDrop dragDropButton = dropped.GetComponent<DragDrop>();
            dragDropButton.parentAfterDrag = transform;

            DragDrop eqquipedbuttonDrag = eqquipedButton.GetComponent<DragDrop>();

            if (eqquipedbuttonDrag.draggable)
            {
                eqquipedbuttonDrag.transform.SetParent(dragDropButton.previousParent);

                eqquipedButton = dropped;
            }
            
        }
        
        
    }


}
