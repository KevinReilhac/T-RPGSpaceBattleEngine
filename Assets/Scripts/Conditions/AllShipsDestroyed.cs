using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.BattleEngine.Ships;

namespace Kebab.BattleEngine.Conditions
{
	[System.Serializable]
	public class AllShipsDestroyed : ACondition
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
			Debug.Log("Destroyed");
			targetShips.Remove(ship);
			onConditionUpdated.Invoke();

			if (IsComplete())
				onConditionComplete.Invoke();
		}

		public override bool IsComplete()
		{
			Debug.Log("Complete");
			return targetShips.Count == 0;
		}

		public override Vector2Int State => new Vector2Int(startShipNumber - targetShips.Count, startShipNumber);
		public override string ConditionTitle => $"Destroy all {owner.ToString()} ships";
	}
}