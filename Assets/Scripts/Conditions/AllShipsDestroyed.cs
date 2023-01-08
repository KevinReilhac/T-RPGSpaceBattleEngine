using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Logs;

namespace Kebab.BattleEngine.Conditions
{
	[System.Serializable]
	public class AllShipsDestroyed : baseCondition
	{
		public enum Target
		{
			Player,
			Enemy,
		}

		[SerializeField] private Target owner = Target.Player;

		private List<Ship> targetShips = null;
		private int startShipNumber = 0;

		public override void Init()
		{
			targetShips = BattleManager.instance.GetShips(owner == Target.Player ? ShipOwner.Player : ShipOwner.Enemy);
			foreach (Ship targetShip in targetShips)
				targetShip.OnDestroy.AddListener(() => OnShipDestroyed(targetShip));
			startShipNumber = targetShips.Count;
		}

		private void OnShipDestroyed(Ship ship)
		{
			targetShips.Remove(ship);
			onConditionUpdated.Invoke();


			BattleEngineLogs.Log(LogVerbosity.High, "Condition {0} : {1} {2} ships remaining", ConditionTitle, targetShips.Count, owner);
			if (IsComplete())
				onConditionComplete.Invoke();
		}

		public override bool IsComplete()
		{
			return targetShips.Count == 0;
		}

		public override Vector2Int State => new Vector2Int(startShipNumber - targetShips.Count, startShipNumber);
		public override string ConditionTitle => $"Destroy all {owner.ToString()} ships";
	}
}