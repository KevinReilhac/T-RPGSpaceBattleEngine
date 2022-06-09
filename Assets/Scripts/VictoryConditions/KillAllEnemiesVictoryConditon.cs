using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.VictoryConditions
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