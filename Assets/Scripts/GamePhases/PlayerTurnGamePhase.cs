using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.UISystem;
using Kebab.BattleEngine.Map;

namespace Kebab.BattleEngine.GamePhases
{
	public class PlayerTurnGamePhase : baseGamePhase
	{
		public override void Begin()
		{
			BattleManager.instance.SetCanSelectPlayerShips(true);
			UIManager.instance.ShowPanel<UI.UI_PlayerTurnPanelsContainer>();
		}

		public override void Stop()
		{
			BattleManager.instance.GridMap.ResetAllCells();
			UIManager.instance.HidePanel<UI.UI_PlayerTurnPanelsContainer>();
		}

		public override void Update()
		{
			//Empty implementation
		}

		public override string Name => "Place units";
	}
}