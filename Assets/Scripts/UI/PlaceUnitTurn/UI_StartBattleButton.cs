using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kebab.UISystem;

namespace Kebab.BattleEngine.UI
{
	[RequireComponent(typeof(Button))]
	public class UI_StartBattleButton : baseUIPanel
	{
		public enum NextTurnEnum
		{
			Player,
			Enemy,
			Random
		}

		[SerializeField] private NextTurnEnum nextTurn = NextTurnEnum.Player;
		private Button button = null;

		private void Awake()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(OnClick);
		}

		public void SetInteractable(bool interactable)
		{
			button.interactable = interactable;
		}

		private void OnClick()
		{
			switch (nextTurn)
			{
				case NextTurnEnum.Player:
					BattleManager.instance.SetGamePhase(GamePhaseEnum.PlayerTurn);
					break;
				case NextTurnEnum.Enemy:
					BattleManager.instance.SetGamePhase(GamePhaseEnum.EnemyTurn);
					break;
				case NextTurnEnum.Random:
					BattleManager.instance.SetGamePhase(Random.Range(0f, 1f) >= 0.5f ? GamePhaseEnum.PlayerTurn : GamePhaseEnum.EnemyTurn);
					break;
			}
		}
	}
}