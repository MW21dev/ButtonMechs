using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Video;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;

	public const float MAP_POSITION_MOD = 9.5f;
	public const float MAP_POSITION_MOD_Y = 0.5f;

	public ScriptableObjectMap[] levels;

	public int level;

	public GameObject whiteMapCover;
	public GameObject shopPanel;
	public GameObject buttonsPanel;
	public GameObject raycastBlock;

	public event Action OnShopEnter;

	[Header("MapGround")]
	public GameObject[] groundPrefab;

	[Header("Player")]
	public GameObject playerPrefab;

	[Header("MapObjects")]
	public GameObject rock0Prefab;
	public GameObject rock1Prefab;
	public GameObject rock2Prefab;
	public GameObject rock3Prefab;
	public GameObject rock4Prefab;
	public GameObject rock5Prefab;
	public GameObject rock6Prefab;
	public GameObject landMinePrefab;

	[Header("Enemies")]
	public GameObject basicTankPrefab;
	

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

		level = Mathf.Clamp(level, 0, levels.Length);
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



		if(GameManager.Instance.enemies.Count <= 0 && isInLevel)
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

	public void ReloadLevel()
	{

		foreach (var child in transform.Cast<Transform>())
        {
            Destroy(child.gameObject);

        }

        foreach (var obj in GameObject.FindGameObjectsWithTag("PickUp"))
        {
            Destroy(obj);
        }

        foreach (var obj in GameObject.FindGameObjectsWithTag("Border"))
        {
            if (!obj.GetComponent<ObjectScript>().mapBorder)
            {
                Destroy(obj);
            }
        }

        SetMap(level);
    }

	public void LaunchLevel()
	{
		SetMap(level);
		GameManager.Instance.DrawButton(PlayerStats.Instance.drawCount);
	}

	public void EndLevel()
	{
		isInLevel = false;
		stage = levelStage.shop;
		GameManager.Instance.UpdateDiscard(GameManager.Instance.discardList.Count);

		foreach(var child in transform.Cast<Transform>())
		{
			Destroy(child.gameObject);

		}

		foreach(var obj in GameObject.FindGameObjectsWithTag("PickUp"))
		{
			Destroy(obj);
		}

		foreach(var obj in GameObject.FindGameObjectsWithTag("Border"))
		{
			if (!obj.GetComponent<ObjectScript>().mapBorder)
			{
				Destroy(obj);
			}
		}

	}

	public void OpenShop()
	{
		shopPanel.GetComponent<CanvasGroup>().alpha = 1f;
		shopPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
		whiteMapCover.SetActive(true);

		ShopManager.Instance.rerollCost = ShopManager.Instance.startRerollCost;

		foreach (var slot in GameObject.FindGameObjectsWithTag("ShopSlot"))
		{
			ShopSlot shopSlot = slot.GetComponent<ShopSlot>();
			shopSlot.SetShopSlot();
		}

		OnShopEnter?.Invoke();
	}

	public void ExitShop()
	{
		isInShop = false;
		stage = levelStage.level;
		level += 1;
		Invoke("CloseShopPanels", 0.1f);
	}

	public void CloseShopPanels()
	{
		
		shopPanel.GetComponent<CanvasGroup>().alpha = 0f;
		shopPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		whiteMapCover.SetActive(false);

	}

	

	public void SetMap(int level)
	{
		var map = levels[level];

		PlayerStats.Instance.transform.position = new Vector3(map.playerPos.x - MAP_POSITION_MOD, map.playerPos.y + MAP_POSITION_MOD_Y, 0f);
		PlayerStats.Instance.transform.rotation = Quaternion.Euler(0f, 0f, (float)map.playerRot);

		for (int y = 0; y < ScriptableObjectMap.MAP_SIZE; ++y)
		{
			for (int x = 0; x < ScriptableObjectMap.MAP_SIZE; ++x)
			{
				var tile = map.ground[ScriptableObjectMap.MAP_SIZE * y + x];
				
				if (groundPrefab[tile] == null)
				{
					continue;
				}
				
				Instantiate(groundPrefab[tile], new Vector3((x - MAP_POSITION_MOD), (y + MAP_POSITION_MOD_Y), 0f), Quaternion.identity, transform);
			}
			
		}

		foreach(var obj in map.mapObj)
		{
			switch (obj.type)
			{
				case ScriptableObjectMap.MapObject.Type.LandMine:
					Instantiate(landMinePrefab, new Vector3(obj.pos.x - MAP_POSITION_MOD, obj.pos.y + MAP_POSITION_MOD_Y , 0f), Quaternion.Euler(new Vector3(0f, 0f, (float)obj.rot)));
					break;
				case ScriptableObjectMap.MapObject.Type.Rock0:
					Instantiate(rock0Prefab, new Vector3(obj.pos.x - MAP_POSITION_MOD, obj.pos.y + MAP_POSITION_MOD_Y, 0f), Quaternion.Euler(new Vector3(0f, 0f, (float)obj.rot)));
					break;
				case ScriptableObjectMap.MapObject.Type.Rock1:
					Instantiate(rock1Prefab, new Vector3(obj.pos.x - MAP_POSITION_MOD, obj.pos.y + MAP_POSITION_MOD_Y, 0f), Quaternion.Euler(new Vector3(0f, 0f, (float)obj.rot)));
					break;
				case ScriptableObjectMap.MapObject.Type.Rock2:
					Instantiate(rock2Prefab, new Vector3(obj.pos.x - MAP_POSITION_MOD, obj.pos.y + MAP_POSITION_MOD_Y, 0f), Quaternion.Euler(new Vector3(0f, 0f, (float)obj.rot)));
					break;
				case ScriptableObjectMap.MapObject.Type.Rock3:
					Instantiate(rock3Prefab, new Vector3(obj.pos.x - MAP_POSITION_MOD, obj.pos.y + MAP_POSITION_MOD_Y, 0f), Quaternion.Euler(new Vector3(0f, 0f, (float)obj.rot)));
					break;
				case ScriptableObjectMap.MapObject.Type.Rock4:
					Instantiate(rock4Prefab, new Vector3(obj.pos.x - MAP_POSITION_MOD, obj.pos.y + MAP_POSITION_MOD_Y, 0f), Quaternion.Euler(new Vector3(0f, 0f, (float)obj.rot)));
					break;
				case ScriptableObjectMap.MapObject.Type.Rock5:
					Instantiate(rock5Prefab, new Vector3(obj.pos.x - MAP_POSITION_MOD, obj.pos.y + MAP_POSITION_MOD_Y, 0f), Quaternion.Euler(new Vector3(0f, 0f, (float)obj.rot)));
					break;
				case ScriptableObjectMap.MapObject.Type.Rock6:
					Instantiate(rock6Prefab, new Vector3(obj.pos.x - MAP_POSITION_MOD, obj.pos.y + MAP_POSITION_MOD_Y, 0f), Quaternion.Euler(new Vector3(0f, 0f, (float)obj.rot)));
					break;
				
			}
		}

		foreach(var obj in map.mapEnemies)
		{
			switch (obj.type)
			{
				case ScriptableObjectMap.MapEnemies.Type.BasicTank:

					
					Instantiate(basicTankPrefab, new Vector3(obj.pos.x - MAP_POSITION_MOD, obj.pos.y + MAP_POSITION_MOD_Y, 0f), Quaternion.Euler(new Vector3(0f, 0f, (float)obj.rotType)));
					break;
			}
		}

		GameManager.Instance.CountEnemies();
	}


}
