using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Text yourTurn;
    public TMP_Text enemiesTurn;

    public Button endTurnButton;

    public GameObject deckPanel;

    private void Start()
    {
        yourTurn.enabled = false;
        enemiesTurn.enabled = false;

        deckPanel.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.playerTurn)
        {
            yourTurn.enabled = true;
            enemiesTurn.enabled = false;
            endTurnButton.interactable = true;


        }
        else if (GameManager.Instance.enemyTurn)
        {
            enemiesTurn.enabled = true;
            yourTurn.enabled = false;
            endTurnButton.interactable = false;

        }

        if (GameManager.Instance.playerTurn)
        {
            foreach(var button in GameManager.Instance.buttonSlots)
            {
                ButtonSlot buttonSlot = button.GetComponent<ButtonSlot>();
                if (!buttonSlot.eqquipedButton)
                {
                    endTurnButton.interactable = false;
                }
                
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
        }
    }
}
