using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler
{
	
	public Image imageBody;
	public Image imageIcon;
	public Transform parentAfterDrag;
	public Transform previousParent;
	private CanvasGroup canvasGroup;

	public bool draggable = true;
	
	
	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (draggable)
		{
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            imageBody.raycastTarget = false;
			imageIcon.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }

    }
		
	

	public void OnDrag(PointerEventData eventData)
	{
		if (draggable)
		{
			transform.position = Input.mousePosition;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (draggable)
		{
            transform.SetParent(parentAfterDrag);
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            imageBody.raycastTarget = true;
			imageIcon.raycastTarget = true;
        }

    }


}
