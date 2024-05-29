using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	
	public GameObject[] buttonSlots;
	public GameObject[] bulbs;

	public List<GameObject> enemies;
	public List<AbilityButtonScript> deck;
	public List<GameObject> inventory;


	public static GameManager Instance;

	public Image raycastBlock;

	public GameObject deckList;

	public Sprite bulbOn;
	public Sprite bulbOff;

	public bool playerTurn;
	public bool enemyTurn;

	public int maxEnemyActions;



	private void Awake()
	{
		if(Instance == null)
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
		playerTurn = true;
		enemyTurn = false;

		raycastBlock.enabled = false;
	}

	private void Update()
	{
		if (playerTurn)
		{
			enemyTurn = false;
		}

		if (enemyTurn)
		{
			playerTurn = false;
		}

		if (enemyTurn)
		{
			foreach (var button in GameObject.FindGameObjectsWithTag("Button"))
			{
				DragDrop dragDrop = button.GetComponent<DragDrop>();
				dragDrop.draggable = false;
			}
		}
		else
		{
			foreach (var button in GameObject.FindGameObjectsWithTag("Button"))
			{
				DragDrop dragDrop = button.GetComponent<DragDrop>();
				dragDrop.draggable = true;
			}
		}

		
	}

	public void StartTurn()
	{
		playerTurn = true;
		enemyTurn = false;
		PlayerStats.Instance.playerCurrentActions = PlayerStats.Instance.playerMaxActions;
		raycastBlock.enabled = false;

		DrawButton(enemies.Count);
	}

	public void EndTurn()
	{
		raycastBlock.enabled = true;
		StartCoroutine(NextAction(0));
	}

	public void StartEnemyTurn()
	{
		enemyTurn = true;
		playerTurn = false;
		StartCoroutine(NextEnemy(0));
	}

	public void EndEnemyTurn()
	{
		Invoke("StartTurn", 1.5f);
	}

   

	public void UseButton(int buttonNumber)
	{
		ButtonSlot slot;

		slot = buttonSlots[buttonNumber].GetComponent<ButtonSlot>();
		var ability = slot.eqquipedButton.GetComponent<AbilityButtonScript>();

		ability.UseAbility(PlayerStats.Instance);
	}


	public void LightBulb(int bulbNumber)
	{
		GameObject bulb;
		bulb = bulbs[bulbNumber];
		Image bulbImage = bulb.GetComponent<Image>();

		bulbImage.sprite = bulbOn;
	}

	public void CountEnemies()
	{
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if(enemy != null)
			{
				EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
				if (!enemyBase.isCounted)
				{
					enemies.Add(enemy);
					enemyBase.isCounted = true;

				}
				enemyBase.enemyActions = enemyBase.maxEnemyActions;
				maxEnemyActions += enemyBase.maxEnemyActions;
			}
		}
	}

	public void ResetPlayerTurn()
	{
		foreach (var bulb in bulbs)
		{
			bulb.GetComponent<Image>().sprite = bulbOff;
		}

		CountEnemies();
		
		if(enemies.Count > 0)
		{
			Invoke("StartEnemyTurn", 2f);
		} 
	}

	public void DrawButton(int drawCount)
	{
        StartCoroutine(DrawingButtons(drawCount));
	}

	IEnumerator DrawingButtons(int drawCount)
	{
		for (int i = drawCount; i > 0; i--)
		{
            if (deck.Count > 0)
            {
                int rnd = Random.Range(0, deck.Count);
                AbilityButtonScript buttonToDraw = deck[rnd].GetComponent<AbilityButtonScript>();

                for (int j = 0; j < inventory.Count; j++)
                {
                    ButtonSlot inventorySlot = inventory[j].GetComponent<ButtonSlot>();
                    if (inventorySlot.empty)
                    {
                        buttonToDraw.transform.SetParent(inventorySlot.transform, false);
                        deck.Remove(buttonToDraw);

                    }
                }

            }

			yield return new WaitForSeconds(0.2f);
        }
    }

	IEnumerator NextAction(int startingItem)
	{
		for (int i = startingItem;  i < buttonSlots.Length; i++)
		{
			UseButton(i);
			LightBulb(i);

			if(i == buttonSlots.Length - 1)
			{
				Invoke("ResetPlayerTurn", 1f);
			}

			yield return new WaitForSeconds(1f);
		}
	}

	IEnumerator NextEnemy(int enemyCount)
	{
		foreach (GameObject enemy in enemies.ToList())
		{
			EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
			
			if (enemy.Equals(enemies.Last()))
			{
                enemyBase.DoAction();
				Invoke("EndEnemyTurn", 1f);
            }
			else
			{
                enemyBase.DoAction();

                yield return new WaitForSeconds(3f);
            }
		}
	}
}
