using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.BattleEngine.Ships;

namespace Kebab.BattleEngine.Conditions
{
	public class AllShipDestroyedDefeatCondition : baseDefeatCondition
	{
		bool isValidate = false;
		
		private void Awake()
		{
			BattleManager.instance.OnShipListUpdate.AddListener(CheckAllChipsDestroyed);
		}

		private void CheckAllChipsDestroyed()
		{
			isValidate = BattleManager.instance.GetShips(ShipOwner.Player).Count <= 0;
		}

		public override bool IsValidate()
		{
			return (isValidate);
		}

		public override string ConditionTitle => string.Format("All your ships are destroyed");
	}
}