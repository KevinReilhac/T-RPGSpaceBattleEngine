using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kebab.Managers;

using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Ships.Buffs.UI;
namespace Kebab.BattleEngine.UI
{
	public class UIManager : Manager<UIManager>
	{
		[SerializeField] private UI_ShipDetailsPanel shipDetailsPanel = null;
		[SerializeField] private UI_ActionsPanel actionsPanel = null;
		[SerializeField] private UI_SupportShipPanel supportPanel = null;
		[SerializeField] private UI_BuffPanel buffPanel = null;
		[SerializeField] private Button endTurnButton = null;
		[SerializeField] private Canvas canvas = null;

		private PlayerShip selectedPlayerShip = null;

		protected override void xAwake()
		{
			base.xAwake();

			SelectShip(null);
			BattleManager.instance.OnShipSelected.AddListener(SelectShip);
			endTurnButton.onClick.AddListener(BattleManager.instance.StartEnemyTurn);
		}

		private void Update()
		{
			endTurnButton.gameObject.SetActive(BattleManager.instance.IsPlayerTurn);
			supportPanel.gameObject.SetActive(BattleManager.instance.IsPlayerTurn);
		}

		public void SelectShip(PlayerShip ship)
		{
			shipDetailsPanel.SetShip(ship);
			actionsPanel.Setup(ship);
			selectedPlayerShip = ship;
		}

		public void OpenBuffPanel()
		{
			buffPanel.Open(selectedPlayerShip);
		}

		public Canvas Canvas
		{
			get => canvas;
		}

		public PlayerShip SelectedShip
		{
			get => selectedPlayerShip;
		}

		private void OnDestroy()
		{
			BattleManager.instance.OnShipSelected.RemoveListener(SelectShip);
		}
	}
}