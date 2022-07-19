using System.Collections.Generic;
using UnityEngine;
using Kebab.BattleEngine.Ships;

namespace Kebab.BattleEngine.Conditions
{
	public class ShipDestroyed : baseCondition
	{
		[SerializeField] private Ship targetShip = null;

		private bool isShipDestroyed = false;

		public override void Init()
		{
			targetShip.OnDestroy.AddListener(OnShipDestroyed);
		}

		private void OnShipDestroyed()
		{
			isShipDestroyed = true;
			onConditionUpdated.Invoke();
			OnConditionComplete.Invoke();
		}

		public override bool IsComplete()
		{
			return isShipDestroyed;
		}

		public override Vector2Int State => new Vector2Int(isShipDestroyed ? 1 : 0, 1);
		public override string ConditionTitle => $"Destroy {targetShip.name}";
	}
}