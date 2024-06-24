
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
	public static TutorialManager Instance;
	public GameObject blur;

	public GameObject[] popUps;
	public int popUpIndex;
	public int i;

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

		blur.SetActive(true);
	}

	private void Update()
	{
		for (i = 0; i < popUps.Length; i++)
		{
			if (i == popUpIndex)
			{
				popUps[popUpIndex].SetActive(true);
				return;
			}

			if (i != popUpIndex)
			{
				popUps[popUpIndex - 1].SetActive(false);
			}
		}


		if (popUpIndex == popUps.Length)
		{
			blur.SetActive(false);
		}
	}

	public void NextPopUp()
	{
		SoundManager.Instance.PlayUISound(0);
		popUpIndex++;

		if (popUpIndex == popUps.Length -1)
		{
			GameManager.Instance.isTutorial = 1;
			SaveScript.Instance.SaveGame();
		}
		
	}
}
