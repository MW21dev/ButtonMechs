using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopManager : MonoBehaviour
{

	public int rerollCost = 1;
	public int startRerollCost = 1;

	public Button rerollButton;
	public TMP_Text rerollText;

	public static ShopManager Instance;
	public event Action OnReroll;
	private Color defaultGreen;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	private void Start()
	{
		defaultGreen = new Color(0.1768868f, 0.7075472f, 0.2922177f);
	}

	private void Update()
	{
		rerollText.text = "Reroll" + "" + $"{rerollCost}" + "$";
		
		if (PlayerStats.Instance.playerCurrentMoney >= rerollCost)
		{
			rerollButton.interactable = true;
			rerollButton.image.color = defaultGreen;
		}
		else
		{
			rerollButton.interactable = false;
			rerollButton.image.color = Color.gray;
		}

		if(rerollCost < 0)
		{
			rerollCost = startRerollCost;
		}
	}
	public void Reroll()
	{
		if(PlayerStats.Instance.playerCurrentMoney >= rerollCost)
		{
			foreach (var slot in GameObject.FindGameObjectsWithTag("ShopSlot"))
			{
				ShopSlot shopSlot = slot.GetComponent<ShopSlot>();
				shopSlot.SetShopSlot();
			}

			PlayerStats.Instance.playerCurrentMoney -= rerollCost;
			rerollCost += 1;
		}

        SoundManager.Instance.PlayUISound(0);


        OnReroll?.Invoke();
	}
}
