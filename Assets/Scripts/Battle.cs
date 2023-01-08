using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Kebab.BattleEngine.Ships;


namespace Kebab.BattleEngine
{
	public class Battle
	{
		private int sceneBuildIndex = -1;
		private UnityAction winCallback = null;
		private UnityAction loseCallback = null;
		private Scene currentScene = default(Scene);
		private Scene battleScene = default(Scene);

		public Battle(int sceneBuildIndex, UnityAction winCallback, UnityAction loseCallback, int startMoney, List<Ship> ships)
		{
			this.sceneBuildIndex = sceneBuildIndex;
			this.winCallback = winCallback;
			this.loseCallback = loseCallback;
		}

		public static Battle CreateByName(string sceneName, UnityAction winCallback, UnityAction loseCallback, int startMoney, List<Ship> ships)
		{
			Scene scene = SceneManager.GetSceneByName(sceneName);

			if (scene == null)
				throw new System.ArgumentException("Scene not exist.");
			return new Battle(SceneManager.GetSceneByName(sceneName).buildIndex, winCallback, loseCallback, startMoney, ships);
		}

		public void Load()
		{
			currentScene = SceneManager.GetActiveScene();
			DisableCurrentScene();
			SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode loadScene)
		{
			if (scene.buildIndex != sceneBuildIndex)
				return;
			SceneManager.SetActiveScene(scene);
			battleScene = scene;
			SceneManager.sceneLoaded -= OnSceneLoaded;

			BattleManager.instance.OnVictory.AddListener(winCallback.Invoke);
			BattleManager.instance.OnDefeat.AddListener(loseCallback.Invoke);
			BattleManager.instance.OnVictory.AddListener(CloseBattleScene);
			BattleManager.instance.OnDefeat.AddListener(CloseBattleScene);
		}

		private void OnBattleEnd()
		{
			CloseBattleScene();
		}

		private void DisableCurrentScene()
		{
			GameObject[] sceneObjects = currentScene.GetRootGameObjects();

			foreach (GameObject go in sceneObjects)
				go.SetActive(false);
		}

		private void EnableCurrentScene()
		{
			GameObject[] sceneObjects = currentScene.GetRootGameObjects();

			foreach (GameObject go in sceneObjects)
				go.SetActive(true);
		}

		private void CloseBattleScene()
		{
			SceneManager.UnloadSceneAsync(battleScene).completed += (_) => EnableCurrentScene();
		}
	}
}