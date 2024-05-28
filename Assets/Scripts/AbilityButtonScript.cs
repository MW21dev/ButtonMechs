using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonScript : MonoBehaviour
{
	public Image image;

	public bool abilityUsed;

	public string abilityName;


	public virtual void UseAbility(PlayerStats player)
	{
		player = PlayerStats.Instance;
		
		if (!abilityUsed)
		{
			abilityUsed = true;
		}
	}
}
