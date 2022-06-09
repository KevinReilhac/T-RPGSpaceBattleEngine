using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.BattleEngine.Ships;

public class KillEnemyVictoryCondition : baseVictoryCondition
{
	[SerializeField] private EnemyShip targetShip = null;

	private bool isTargetDestroyed = false;
	private string targetName = null;

	private void Awake()
	{
		targetShip.OnDestroy.AddListener(() => isTargetDestroyed = true);
		targetName = targetShip.name;
	}

	public override bool IsValidate()
	{
		return isTargetDestroyed;
	}

	public override string ConditionTitle => string.Format("Destory \"{0}\"", targetName);
}
