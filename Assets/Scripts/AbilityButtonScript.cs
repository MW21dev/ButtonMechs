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
	public TMP_Text description;
    

    public bool isHovering = false;
    public float timeToWait = 0.5f;
    float timeLeft;

	public bool abilityUsed;
    

	public string abilityName;
    public int abilityPrice;

	[TextArea]
	public string abilityDescription;

    public enum Category
    {
        starter,
        normal,
        chrome,
        silver,
        gold,
        rainbow,
        poker,
    }

    public Category type;

    private void Start()
    {
        description.text = abilityDescription;
        abilityText.SetActive(false);

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
        }

        if (!isHovering)
        {
            abilityText.SetActive(false);
        }

        
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
