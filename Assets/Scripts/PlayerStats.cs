using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int playerMaxHp;
    public int playerCurrentHp;
    public int playerMaxActions;
    public int playerCurrentActions;
    public int playerDamage;
    public int playerCurrentMoney;
    public int drawCount = 1;
    public bool isDead;

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

    private void Start()
    {
        Invoke("SetHealth", 0.2f);

       
    }

    

    public void SetHealth()
    {
        playerCurrentHp = playerMaxHp;
        UIScript.Instance.ChangeHp(playerCurrentHp);

    }

    public void GetHit(int recievedDmg)
    {
        playerCurrentHp -= recievedDmg;
        var explosionPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionPrefab, 0.2f);
        UIScript.Instance.ChangeHp(playerCurrentHp);
        
        if(playerCurrentHp == 0)
        {
            Invoke("Dead", 0.1f);
        }

        

    }

    public void UseAction(int actionCost)
    {
        playerCurrentActions -= actionCost;
    }

    public void SetMaxActions(int maxActions)
    {
        playerCurrentActions = maxActions;
    }
    public void Dead()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
