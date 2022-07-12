using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kebab.BattleEngine;
using Kebab.Managers;

public class DemoSceneManager : MonoBehaviour
{
	private void Awake()
	{
		BattleManager.instance.OnVictory.AddListener(OnVictory);
		BattleManager.instance.OnDefeat.AddListener(OnDefeat);
	}

	private void OnVictory()
	{
		Invoke("NextScene", 3f);
	}

	private void OnDefeat()
	{
		Invoke("RestartScene", 3f);
	}

	private void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void NextScene()
	{
		if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1)
			Application.Quit();
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
