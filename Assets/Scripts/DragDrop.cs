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
	public bool isDraged = false;
	
	
	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();

	}

	private void Update()
	{
        if (isDraged && !draggable)
        {
            transform.SetParent(previousParent);
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            imageBody.raycastTarget = true;
            imageIcon.raycastTarget = true;
        }
    }



	public void OnBeginDrag(PointerEventData eventData)
	{
        
        if (draggable)
		{
			isDraged = true;
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            imageBody.raycastTarget = false;
			imageIcon.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();

            SoundManager.Instance.PlayUISound(1);

        }


    }
		
	

	public void OnDrag(PointerEventData eventData)
	{
		
		
		if (draggable)
		{
			isDraged = true;
			transform.position = Input.mousePosition;

			
		}

		Debug.Log($"Im dragging {gameObject.name}");
    }

	public void OnEndDrag(PointerEventData eventData)
	{

        if (draggable)
		{
			isDraged = false;
            transform.SetParent(parentAfterDrag);
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            imageBody.raycastTarget = true;
			imageIcon.raycastTarget = true;

            SoundManager.Instance.PlayUISound(2);

        }

    }


}
