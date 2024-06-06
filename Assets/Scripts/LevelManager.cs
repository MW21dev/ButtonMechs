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
    public GameObject buttonsPanel;
    public GameObject raycastBlock;
    public GameObject[] mapObjects;
    

    public bool isInLevel;
    public bool isInShop;

    
    
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
 
        switch (stage)
        {
            case levelStage.level:
                if (!isInLevel)
                {
                    LaunchLevel();
                    isInLevel = true;
                }
            break;
            case levelStage.shop:
                if (!isInShop)
                {
                    OpenShop();
                    isInShop = true;
                }
                break;
        }


        if (stage == levelStage.level && isInLevel)
        {
            levelLoadingTime -= Time.deltaTime;

        }

        if (levelLoadingTime < 0)
        {
            levelLoadingTime = 0;
        }

        if(GameManager.Instance.enemies.Count <= 0 && levelLoadingTime == 0 && isInLevel)
        {
            Invoke("EndLevel", 0.5f);
        }

        if (isInLevel)
        {
            buttonsPanel.GetComponent<CanvasGroup>().alpha = 1f;
            buttonsPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            
        }
        else
        {
            buttonsPanel.GetComponent<CanvasGroup>().alpha = 0f;
            buttonsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            ExitShop();
        }
    }


    public void LaunchLevel()
    {
        StartCoroutine(SetMap());
        GameManager.Instance.DrawButton(PlayerStats.Instance.drawCount);
        levelLoadingTime = 2f;
    }

    public void EndLevel()
    {
        isInLevel = false;
        stage = levelStage.shop;
        GameManager.Instance.UpdateDiscard(GameManager.Instance.discardList.Count);

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
        isInShop = false;
        stage = levelStage.level;
        Invoke("CloseShopPanels", 0.1f);
    }

    public void CloseShopPanels()
    {
        
        shopPanel.GetComponent<CanvasGroup>().alpha = 0f;
        shopPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        whiteMapCover.SetActive(false);

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
