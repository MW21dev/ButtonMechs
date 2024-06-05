
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class GameManager : MonoBehaviour
{
	public GameObject[] buttonSlots;
	public GameObject[] bulbs;

	public GameObject[] allAbilityButtons;

	public GameObject[] shopButtons;

	public List<GameObject> enemies;
	public List<AbilityButtonScript> deckList;
	public List<AbilityButtonScript> startDeck;
	public List<GameObject> inventory;
	public List<AbilityButtonScript> discardList;

	public TMP_Text deckCountText;
	public TMP_Text discardCountText;


	public static GameManager Instance;

	public Image raycastBlock;


	public GameObject shopPanel;
	public GameObject deck;
	public GameObject discard;
	public int deckCount;
	public int discardCount;

	public Sprite bulbOn;
	public Sprite bulbOff;

	public bool playerTurn;
	public bool enemyTurn;
	public bool shopTurn;

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

		CreateStartDeck();
		UpdateDeck();
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

		deckCount = deckList.Count;
		discardCount = discardList.Count;
		deckCountText.text = deckCount.ToString();
		discardCountText.text = discardCount.ToString();

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UpdateDiscard(discardList.Count);
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			debugShopKey();
		}
	}

	public void StartTurn()
	{
		playerTurn = true;
		enemyTurn = false;
		PlayerStats.Instance.playerCurrentActions = PlayerStats.Instance.playerMaxActions;
		raycastBlock.enabled = false;

		DrawButton(PlayerStats.Instance.drawCount);
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
		Invoke("StartTurn", 1f);
	}

   

	public void UseButton(int buttonNumber)
	{
		ButtonSlot slot;

		slot = buttonSlots[buttonNumber].GetComponent<ButtonSlot>();
		var ability = slot.eqquipedButton.GetComponent<AbilityButtonScript>();

		foreach (var button in GameObject.FindGameObjectsWithTag("Button"))
		{
				DragDrop dragDrop = button.GetComponent<DragDrop>();
				dragDrop.draggable = false;
		}

		ability.UseAbility(PlayerStats.Instance);
		
		if(ability.type != AbilityButtonScript.Category.starter)
		{
			discardList.Add(ability);
			ability.gameObject.transform.SetParent(discard.transform, false);
			slot.eqquipedButton = null;

		}
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
			Invoke("StartEnemyTurn", 1f);
		} 
	}

	public void DrawButton(int drawCount)
	{
		StartCoroutine(DrawingButtons(drawCount));
		UpdateDeck();
	}

	public void CreateStartDeck()
	{
		foreach(var button in startDeck)
		{
			var newButton = Instantiate(button, deck.transform);
			newButton.transform.SetParent(deck.transform);
		}
	}

	public void UpdateDeck()
	{
		int childnum = deck.transform.childCount;

		deckList.Clear();

		for (int i = 0; i < childnum; i++)
		{
			AbilityButtonScript button = deck.transform.GetChild(i).GetComponent<AbilityButtonScript>();
			deckList.Add(button);
		}
	}

	public void UpdateDiscard(int allDiscard)
	{
		StartCoroutine(DiscardToDeck(allDiscard));
	}

	public IEnumerator DrawingButtons(int drawCount)
	{
		for (int i = drawCount; i > 0; i--)
		{
            if (deckList.Count > 0)
            {
                int rnd = Random.Range(0, deckList.Count);
                AbilityButtonScript buttonToDraw = deckList[rnd].GetComponent<AbilityButtonScript>();

                for (int j = 0; j < inventory.Count; j++)
                {
                    ButtonSlot inventorySlot = inventory[j].GetComponent<ButtonSlot>();
                    if (inventorySlot.empty)
                    {
                        buttonToDraw.transform.SetParent(inventorySlot.transform, false);
                        deckList.Remove(buttonToDraw);

                    }
                }

            }

			yield return new WaitForSeconds(0.2f);
        }
    }

	public IEnumerator DiscardToDeck(int discardToDeck)
	{ 
		for(int i = discardToDeck; i > 0; i--)
		{
            if (discardList.Count > 0)
            {
                for (int j = 0; j < discard.transform.childCount; j++)
                {
                    AbilityButtonScript buttonFromDiscard = discard.transform.GetChild(j).GetComponent<AbilityButtonScript>();
                    buttonFromDiscard.transform.SetParent(deck.transform, false);
                    discardList.Remove(discardList[j]);

                    UpdateDeck();
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

                yield return new WaitForSeconds(2f);
            }
		}
	}

	private void debugShopKey()
	{
		enemies.Clear();
		foreach (var e in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			Destroy(e);
		}
	}
}
