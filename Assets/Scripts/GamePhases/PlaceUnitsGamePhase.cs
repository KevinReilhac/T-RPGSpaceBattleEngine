using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.UISystem;
using Kebab.DesignData;
using Kebab.BattleEngine.Map;
using Kebab.BattleEngine.Ships;

using Kebab.BattleEngine.UI;

namespace Kebab.BattleEngine.GamePhases
{
	public class PlaceUnitsGamePhase : baseGamePhase
	{
		private CellColorsDesignData cellColorsDesignData = null;
		private UI_StartBattleButton startBattleButton = null;

		public PlaceUnitsGamePhase()
		{
			cellColorsDesignData = DesignDataManager.Get<CellColorsDesignData>();
			startBattleButton = UIManager.instance.GetPanel<UI_StartBattleButton>();
		}

		public override void Begin()
		{
			UIManager.instance.ShowPanel<UI.UI_PlaceUnitsTurnPanelsContainer>();
			GetSeparationLine().ForEach((c) => c.SetInsideColor(cellColorsDesignData.separationFillColor));
			GetPlayerSideCells().ForEach((c) => c.SetInsideColor(cellColorsDesignData.playerSideFillColor));
			BattleManager.instance.OnShipListUpdate.AddListener(UpdateStartBattleButton);
			UpdateStartBattleButton();
		}

		private CellCollection GetPlayerSideCells()
		{
			CellCollection cells = BattleManager.instance.GridMap.GetAllCells();

			return new CellCollection(cells.Where((c) => c.GridPosition.x < BattleManager.instance.GridXSeparation).ToList());
		}

		private CellCollection GetSeparationLine()
		{
			Vector2Int start = new Vector2Int(BattleManager.instance.GridXSeparation, 0);
			Vector2Int end = new Vector2Int(BattleManager.instance.GridXSeparation, BattleManager.instance.GridMap.Size.y - 1);

			return BattleManager.instance.GridMap.GetCellLine(start, end, false);
		}

		public override void Stop()
		{
			UIManager.instance.HidePanel<UI.UI_PlaceUnitsTurnPanelsContainer>();
			GetSeparationLine().ForEach((c) => c.ResetInsideColor());
			GetPlayerSideCells().ForEach((c) => c.ResetInsideColor());
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