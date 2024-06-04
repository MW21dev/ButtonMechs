using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance;

    public TMP_Text yourTurn;
    public TMP_Text enemiesTurn;
    public TMP_Text playerMoneyText;

    public List<Image> healthBulbs;

    public Button endTurnButton;

    public Sprite healthBulbOn;
    public Sprite healthBulbOff;

    public GameObject deckPanel;
    public GameObject discardPanel;
    public GameObject healthGrid;
    public GameObject healthBulbPrefab;

    public Color defaultGreen;

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
        yourTurn.enabled = false;
        enemiesTurn.enabled = false;


        deckPanel.SetActive(false);
        discardPanel.SetActive(false);

        defaultGreen = new Color(0.1768868f, 0.7075472f, 0.2922177f);

        
        for(int i = 0; i < PlayerStats.Instance.playerMaxHp; i++)
        {
            Instantiate(healthBulbPrefab, healthGrid.transform);
        }

        int healthGridChildren = healthGrid.transform.childCount;

        for(int i = 0; i < healthGridChildren; i++)
        {
            Image healthBulb = healthGrid.transform.GetChild(i).GetComponent<Image>();
            healthBulbs.Add(healthBulb);
        }

        
    }

    private void Update()
    {
        if (GameManager.Instance.playerTurn)
        {
            yourTurn.enabled = true;
            enemiesTurn.enabled = false;
            endTurnButton.interactable = true;
            endTurnButton.image.color = defaultGreen;
        }
        else if (GameManager.Instance.enemyTurn)
        {
            enemiesTurn.enabled = true;
            yourTurn.enabled = false;
            endTurnButton.interactable = false;
            endTurnButton.image.color = Color.gray;
        }

        if (GameManager.Instance.playerTurn)
        {
            foreach(var button in GameManager.Instance.buttonSlots)
            {
                ButtonSlot buttonSlot = button.GetComponent<ButtonSlot>();
                if (!buttonSlot.eqquipedButton)
                {
                    endTurnButton.interactable = false;
                    endTurnButton.image.color = Color.gray;
                }
                
            }
        }

        if (deckPanel.activeSelf)
        {
            discardPanel.SetActive(false);
        }

        if (discardPanel.activeSelf)
        {
            deckPanel.SetActive(false);
        }

        playerMoneyText.text = PlayerStats.Instance.playerCurrentMoney.ToString();

    }

    public void ChangeHp(int playerHp)
    {
        if(playerHp == healthBulbs.Count)
        {
            foreach(var bulb in healthBulbs)
            {
                bulb.GetComponent<Image>().sprite = healthBulbOn;
            }
        }
        
        if(playerHp == healthBulbs.Count - 1)
        {
            foreach (var bulb in healthBulbs.ToList())
            {
                bulb.GetComponent<Image>().sprite = healthBulbOn;
                healthBulbs[^1].GetComponent<Image>().sprite = healthBulbOff;
            }
        }
        
        if (playerHp == healthBulbs.Count - 2)
        {
            foreach (var bulb in healthBulbs.ToList())
            {
                bulb.GetComponent<Image>().sprite = healthBulbOn;
                healthBulbs[^1].GetComponent<Image>().sprite = healthBulbOff;
                healthBulbs[^2].GetComponent<Image>().sprite = healthBulbOff;
            }
        }
        
        if (playerHp == healthBulbs.Count - 3)
        {
            foreach (var bulb in healthBulbs.ToList())
            {
                bulb.GetComponent<Image>().sprite = healthBulbOn;
                healthBulbs[^1].GetComponent<Image>().sprite = healthBulbOff;
                healthBulbs[^2].GetComponent<Image>().sprite = healthBulbOff;
                healthBulbs[^3].GetComponent<Image>().sprite = healthBulbOff;
            }
        }
        
        if (playerHp == healthBulbs.Count - 4)
        {
            foreach (var bulb in healthBulbs.ToList())
            {
                bulb.GetComponent<Image>().sprite = healthBulbOn;
                healthBulbs[^1].GetComponent<Image>().sprite = healthBulbOff;
                healthBulbs[^2].GetComponent<Image>().sprite = healthBulbOff;
                healthBulbs[^3].GetComponent<Image>().sprite = healthBulbOff;
                healthBulbs[^4].GetComponent<Image>().sprite = healthBulbOff;

            }
        }
    }

    public void DeckPanel()
    {
        if (deckPanel.activeSelf)
        {
            deckPanel.SetActive(false);
            
        }
        else if (!deckPanel.activeSelf)
        {
            deckPanel.SetActive(true);
            discardPanel.SetActive(false);
        }
    }

    public void DiscardPanel()
    {
        if (discardPanel.activeSelf)
        {
            discardPanel.SetActive(false);
        }
        else if (!deckPanel.activeSelf)
        {
            discardPanel.SetActive(true);
            deckPanel.SetActive(false);
        }
    }
}
