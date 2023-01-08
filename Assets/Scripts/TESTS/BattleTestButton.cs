using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kebab.BattleEngine;

public class BattleTestButton : MonoBehaviour
{
	[SerializeField] private Button button = null;

	private Battle battle = null;


	private void Awake()
	{
		battle = new Battle(0, Win, Lose, 3000, null);
		button.onClick.AddListener(Load);
	}

	private void Load()
	{
		battle.Load();
	}

	private void Win()
	{
		Debug.Log("WIN BATTLE");
	}

	private void Lose()
	{
		Debug.Log("LOSE BATTLE");
	}
}
