using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.Conditions
{
	public class KillAllEnemiesVictoryConditon : baseVictoryCondition
	{
		public override bool IsValidate()
		{
			return (BattleManager.instance.GetShips(ShipOwner.Enemy).Count == 0);
		}

		public override string ConditionTitle => "Kill all enemies.";
	}
}