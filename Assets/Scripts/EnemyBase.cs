using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class EnemyBase : MonoBehaviour
{
    public string enemyName;
    public bool isEnemyTurn = false;
    public int enemyActions;
    public int maxEnemyActions;
    public int enemyAbilitiesCount;
    public bool isCounted;
    public bool canMove;

    public bool isHovering = false;
    public float timeToWait = 0.5f;
    float timeLeft;

    public GameObject shotPoint;
    public GameObject bullet;
    public GameObject explosion;
    public SpriteRenderer activeSprite;

    public GameObject enemyinfoPanel;
    public GameObject enemyAbilityList;
    public GameObject deckPanel;

    public GameObject enemyability1;
    public GameObject enemyability2;
    public GameObject enemyability3;


    public TMP_Text enemyNameText;
    public TMP_Text enemyActionsText;
    public GameObject ability1Image;
    public GameObject ability2Image;
    public GameObject ability3Image;

    public Vector3 rayCheck;

    private void Awake()
    {
        enemyinfoPanel = GameObject.Find("EnemyPanelInfo");
        enemyAbilityList = GameObject.Find("EnemyAbilityList");
        deckPanel = GameObject.Find("DeckPanel");
        enemyNameText = enemyinfoPanel.transform.GetChild(1).GetComponent<TMP_Text>();
        enemyActionsText = enemyinfoPanel.transform.GetChild(2).GetComponent<TMP_Text>();


        ability1Image = enemyAbilityList.transform.GetChild(0).gameObject;
        ability2Image = enemyAbilityList.transform.GetChild(1).gameObject;
        ability3Image = enemyAbilityList.transform.GetChild(2).gameObject;
    }


    private void Start()
    {
        var pos = transform.position;

        pos.x = Mathf.Clamp(transform.position.x, -9.49f, -0.5f);
        pos.y = Mathf.Clamp(transform.position.y, 0.49f, 9.49f);


        activeSprite.enabled = false;
        enemyinfoPanel.GetComponent<CanvasGroup>().alpha = 0f;
    }

    private void Update()
    {
        if (isHovering)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft <= 0 && isHovering)
        {
            enemyinfoPanel.GetComponent<CanvasGroup>().alpha = 1f;
            

            Image ab1 = ability1Image.GetComponent<Image>();
            Image ab2 = ability2Image.GetComponent<Image>();
            Image ab3 = ability3Image.GetComponent<Image>();

            ab1.sprite = enemyability1.GetComponent<Image>().sprite;
            ab2.sprite = enemyability2.GetComponent<Image>().sprite;
            ab3.sprite = enemyability3.GetComponent<Image>().sprite;

            TMP_Text ab1text = ability1Image.GetComponentInChildren<TMP_Text>();
            TMP_Text ab2text = ability2Image.GetComponentInChildren<TMP_Text>();
            TMP_Text ab3text = ability3Image.GetComponentInChildren<TMP_Text>();

            ab1text.text = enemyability1.GetComponent<EnemyAbilityButton>().abilityDescription;
            ab2text.text = enemyability2.GetComponent<EnemyAbilityButton>().abilityDescription;
            ab3text.text = enemyability3.GetComponent<EnemyAbilityButton>().abilityDescription;

            enemyNameText.text = enemyName;
            enemyActionsText.text = maxEnemyActions.ToString();

            Debug.Log("OnEnemy");

            if (transform.eulerAngles.z == 0)
            {
                rayCheck = Vector3.up;
            }
            else if (transform.eulerAngles.z == 90f)
            {
                rayCheck = Vector3.left;
            }
            else if (transform.eulerAngles.z == 270)
            {
                rayCheck = Vector3.right;

            }
            else if (transform.eulerAngles.z == 180)
            {
                rayCheck = Vector3.down;

            }
        }

        
        if(enemyActions == 0)
        {
            activeSprite.enabled = false;
        }

    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(shotPoint.transform.position, rayCheck);

        Debug.DrawRay(shotPoint.transform.position, rayCheck, Color.red);


        if (hit.collider.gameObject.tag == "Border" && hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Enemy")
        {

            canMove = false;
        }
        else if (hit.collider.gameObject.tag != "Border" || hit.collider.gameObject.tag != "Enemy")
        {
            
            canMove = true;
        }
    }

    public virtual void DoAction()
    {
        if (GameManager.Instance.enemyTurn)
        {
            
            EnemyNextAction();
            activeSprite.enabled = true;
        }
   
    }


    public void GetHit(int damage)
    {
        
        var explosionPrefab = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(explosionPrefab, 0.2f);
        PlayerStats.Instance.playerCurrentMoney += 1;
        GameManager.Instance.enemies.Remove(gameObject);
        Destroy(gameObject, 0.2f);

    }

    public void OnMouseEnter()
    {
        timeLeft = timeToWait;
        isHovering = true;
    }

    public void OnMouseExit()
    {
        isHovering = false;
        enemyinfoPanel.GetComponent<CanvasGroup>().alpha = 0f;

    }

    public void EnemyNextAction()
    {
        if (enemyActions > 0)
        {
            int rnd = Random.Range(0, enemyAbilitiesCount);
            switch (rnd)
            {
                case 0:
                    EnemyAbilityButton ab1 = enemyability1.GetComponent<EnemyAbilityButton>();
                    ab1.UseAbility(this);
                    Debug.Log("A1");
                    break;
                case 1:
                    EnemyAbilityButton ab2 = enemyability2.GetComponent<EnemyAbilityButton>();
                    ab2.UseAbility(this);
                    Debug.Log("A2");

                    break;
                case 2:
                    EnemyAbilityButton ab3 = enemyability3.GetComponent<EnemyAbilityButton>();
                    ab3.UseAbility(this);
                    Debug.Log("A3");

                    break;
            }

            enemyActions -= 1;
            GameManager.Instance.maxEnemyActions -= 1;
            Invoke("EnemyNextAction", 0.5f);
        }
    }
}
