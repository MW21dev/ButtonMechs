using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBloodyMoney : AbilityModule
{
	bool isUsed;
	private void Start()
	{
		EnemyBase.OnEnemyDestroy += EnemyDestroyed;

		description.text = $"{abilityName}" + "\n" + "\n " + $"{abilityDescription}";
		abilityText.SetActive(false);
	}
	private void OnDestroy()
	{
		EnemyBase.OnEnemyDestroy -= EnemyDestroyed;
	}

	public new void Update()
	{
		

		if (isHovering)
		{
			timeLeft -= Time.deltaTime;
		}
		if (timeLeft <= 0)
		{
			abilityText.SetActive(true);

			if (inShop)
			{
				abilityText.transform.localPosition = new Vector3(abilityText.transform.localPosition.x, 15f, abilityText.transform.localPosition.z);
			}
			else
			{
				abilityText.transform.localPosition = new Vector3(abilityText.transform.localPosition.x, -75f, abilityText.transform.localPosition.z);
			}
		}

		if (!isHovering)
		{
			abilityText.SetActive(false);
		}

		if (!inShop)
		{
			foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				EnemyBase en = enemy.GetComponent<EnemyBase>();

				if (!isUsed && inShop)
				{
					EnemyBase.OnEnemyDestroy += EnemyDestroyed;
					isUsed = true;
				}
			}
		}
		
	}

	void EnemyDestroyed()
	{
		PlayerStats.Instance.playerCurrentMoney += 1;
	}
}
