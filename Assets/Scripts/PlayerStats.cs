using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int playerMaxHp;
    public int playerCurrentHp;
    public int playerMaxActions;
    public int playerCurrentActions;
    public int playerDamage;

    public static PlayerStats Instance;

    public GameObject explosion;
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
        var explosionPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionPrefab, 0.2f);
        gameObject.SetActive(false);
    }

    public void UseAction(int actionCost)
    {
        playerCurrentActions -= actionCost;
    }

    public void SetMaxActions(int maxActions)
    {
        playerCurrentActions = maxActions;
    }
}
