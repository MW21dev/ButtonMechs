using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int playerMaxHp;
    public int playerCurrentHp;
    public int playerActions;

    public static PlayerStats Instance;

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

    public void SetMaxHealth(int maxHp)
    {
        playerMaxHp = maxHp;
    }

    public void GetHit(int recievedDmg)
    {
        playerCurrentHp -= recievedDmg;
    }

    public void UseAction(int actionCost)
    {
        playerActions -= actionCost;
    }

    public void SetMaxActions(int maxActions)
    {
        playerActions = maxActions;
    }
}
