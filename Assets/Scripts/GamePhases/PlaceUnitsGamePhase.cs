using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.UISystem;
using Kebab.DesignData;
using Kebab.BattleEngine.Map;
using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.MoneySystem;

using Kebab.BattleEngine.UI;

namespace Kebab.BattleEngine.GamePhases
{
	public class PlaceUnitsGamePhase : baseGamePhase
	{
		private CellColorsDesignData cellColorsDesignData = null;
		private UI_StartBattleButton startBattleButton = null;

		public PlaceUnitsGamePhase()
		{
			Debug.Log("test");
			cellColorsDesignData = DesignDataManager.Get<CellColorsDesignData>();
			startBattleButton = UIManager.instance.GetPanel<UI_StartBattleButton>();
		}

		public override void Begin()
		{
			UIManager.instance.ShowPanel<UI.UI_PlaceUnitsTurnPanelsContainer>();
			BattleManager.instance.GetSeparationLine().ForEach((c) => c.SetInsideColor(cellColorsDesignData.separationFillColor));
			BattleManager.instance.GetPlayerSideCells().ForEach(c => c.SetInsideColor(cellColorsDesignData.playerSideFillColor));
			BattleManager.instance.OnShipListUpdate.AddListener(UpdateStartBattleButton);
			UpdateStartBattleButton();
		}

		private void SetupPlayerSideCell(Cell cell)
		{
			cell.SetInteractable(true);
		}

		public override void Stop()
		{
			UIManager.instance.HidePanel<UI.UI_PlaceUnitsTurnPanelsContainer>();
			BattleManager.instance.GridMap.ResetAllCells();
			BattleManager.instance.OnShipListUpdate.RemoveListener(UpdateStartBattleButton);
		}

		private void UpdateStartBattleButton()
		{
			startBattleButton.SetInteractable(BattleManager.instance.GetShips(ShipOwner.Player).Count > 0);
		}

		public override void Update()
		{

		}

		public override string Name => "Place units";
	}
}