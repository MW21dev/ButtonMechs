using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int level;
    public float levelLoadingTime;

    public GameObject whiteMapCover;
    public GameObject shopPanel;

    public GameObject[] mapObjects;
    

    public bool isInLevel;
    public bool isInShop;

    public string currentStage;
    
    public enum levelStage
    {
        level,
        shop,
    }

    public levelStage stage;

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
        stage = levelStage.level;
        shopPanel.GetComponent<CanvasGroup>().alpha = 0f;
        shopPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        whiteMapCover.SetActive(false);
    }

    private void Update()
    {
        if(stage == levelStage.level)
        {
            if (!isInLevel)
            {
                LaunchLevel();
                isInLevel = true;
            }
        }

        if (stage == levelStage.shop)
        {
            if (!isInShop)
            {
                OpenShop();
                isInShop = true;
            }
        }

        levelLoadingTime  -= Time.deltaTime;

        if (levelLoadingTime < 0)
        {
            levelLoadingTime = 0;
        }

        if(GameManager.Instance.enemies.Count <= 0 && levelLoadingTime == 0)
        {
            Invoke("EndLevel", 0.5f);
        }
    }


    public void LaunchLevel()
    {
        StartCoroutine(SetMap());
        
        levelLoadingTime = 5f;
    }

    public void EndLevel()
    {
        isInLevel = false;
        stage = levelStage.shop;
    }

    public void OpenShop()
    {
        shopPanel.GetComponent<CanvasGroup>().alpha = 1f;
        shopPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        whiteMapCover.SetActive(true);

        foreach (var slot in GameObject.FindGameObjectsWithTag("ShopSlot"))
        {
            ShopSlot shopSlot = slot.GetComponent<ShopSlot>();
            shopSlot.SetShopSlot();
        }
    }

    public void ExitShop()
    {
        shopPanel.GetComponent<CanvasGroup>().alpha = 0f;
        shopPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        whiteMapCover.SetActive(false);

        stage = levelStage.level;
        levelLoadingTime = 5f;
        isInShop = false;
    }

    IEnumerator SetMap()
    {
        foreach(var mapTile in GameObject.FindGameObjectsWithTag("Ground"))
        {
            GroundTile ground = mapTile.GetComponent<GroundTile>();
            if (ground.empty)
            {
                int rnd = Random.Range(0, mapObjects.Length - 1);
                if(mapObjects[rnd] != null)
                {
                    Instantiate(mapObjects[rnd], mapTile.transform.position, Quaternion.identity);
                }

                GameManager.Instance.CountEnemies();
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}
