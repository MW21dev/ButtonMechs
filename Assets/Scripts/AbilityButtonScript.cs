using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class AbilityButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Image image;
	public GameObject abilityText;
	public GameObject typeText;
	public TMP_Text description;
	public TMP_Text typeDescription;

	

	public bool isHovering = false;
	public float timeToWait = 0.5f;
	public float abilityTime = 0f;
	public float timeLeft;

	public bool abilityUsed;
	

	public string abilityName;
	public int abilityPrice;

	[TextArea]
	public string abilityDescription;

	[TextArea]
	public string abilityTypeDescription;

	public enum Category
	{
		starter,
		normal,
		chrome,
		silver,
		gold,
		rainbow,
		poker,
		glass,
	}

	public Category type;

    private void Awake()
    {
		abilityPrice = Mathf.Clamp(abilityPrice, 0, 100);
    }

    private void Start()
	{
		description.text = $"{abilityName}" + "\n" + "\n "+ $"{abilityDescription}";
		abilityText.SetActive(false);
		if(typeText != null && typeDescription != null)
		{
			typeDescription.text = $"{type}" + "\n " + "\n" + $"{abilityTypeDescription}";
			typeText.SetActive(false);
		}

	}
	void Update()
	{
		if (isHovering)
		{
			timeLeft -= Time.deltaTime;
		}
		if (timeLeft <= 0)
		{
			abilityText.SetActive(true);

            if (typeText != null && typeDescription != null)
            {
                typeText.SetActive(true);
            }
        }

		if (!isHovering)
		{
			abilityText.SetActive(false);

			if (typeText != null && typeDescription != null)
			{
				typeText.SetActive(false);
			}
		}

		abilityPrice = Mathf.Clamp(abilityPrice, 0, 100);

    }

    public virtual void UseAbility(PlayerStats player)
	{
		player = PlayerStats.Instance;
		
		if (!abilityUsed)
		{
			abilityUsed = true;
		}
	}

	public virtual void UseMenuAbility()
	{

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		timeLeft = timeToWait;
		isHovering = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHovering = false;
	}

}
