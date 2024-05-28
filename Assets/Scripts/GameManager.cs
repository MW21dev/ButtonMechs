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

    public static GameManager Instance;

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

        if(maxEnemyActions == 0 && enemyTurn)
        {
            EndEnemyTurn();
        }
    }

    public void StartTurn()
    {
        playerTurn = true;
        enemyTurn = false;
        PlayerStats.Instance.playerCurrentActions = PlayerStats.Instance.playerMaxActions;

    }

    public void EndTurn()
    {
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
                enemies.Add(enemy);
                EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
                enemyBase.enemyActions = enemyBase.maxEnemyActions;
                maxEnemyActions += enemyBase.maxEnemyActions;
            }
        }
    }

    public void Reset()
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

    IEnumerator NextAction(int startingItem)
    {
        for (int i = startingItem;  i < buttonSlots.Length; i++)
        {
            UseButton(i);
            LightBulb(i);

            if(i == buttonSlots.Length - 1)
            {
                Invoke("Reset", 1f);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator NextEnemy(int enemyCount)
    {
        foreach (GameObject enemy in enemies.ToList())
        {
            EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();

            enemyBase.DoAction();

            yield return new WaitForSeconds(3f);
        }
    }
}
