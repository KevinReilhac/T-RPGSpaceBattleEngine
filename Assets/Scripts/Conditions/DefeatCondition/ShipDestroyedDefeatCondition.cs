using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.BattleEngine.Ships;

namespace Kebab.BattleEngine.Conditions
{
	public class ShipDestroyedDefeatCondition : baseDefeatCondition
	{
		[SerializeField] private PlayerShip targetShip = null;

		bool isValidate = false;

		private void Awake()
		{
			targetShip.OnDestroy.AddListener(() => isValidate = false);
		}

		public override bool IsValidate()
		{
			return (isValidate);
		}

		public override string ConditionTitle => string.Format("{0} destroyed", targetShip.name);
	}
}