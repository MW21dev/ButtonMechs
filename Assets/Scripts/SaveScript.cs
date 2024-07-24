using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
	public static SaveScript Instance;

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


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F3))
		{
			ResetSave();
		}
	}

	public void SaveGame()
	{
		PlayerPrefs.SetInt("Tutorial", GameManager.Instance.isTutorial);
	}

	public void LoadGame()
	{
		GameManager.Instance.isTutorial = PlayerPrefs.GetInt("Tutorial");
	}

	public void ResetSave()
	{
        GameManager.Instance.isTutorial = 0;
        SaveGame();
    }

	public void RestartAll()
	{
		PlayerPrefs.DeleteAll();
	}
}
