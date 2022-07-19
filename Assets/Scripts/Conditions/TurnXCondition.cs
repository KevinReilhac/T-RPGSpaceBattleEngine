using System.Collections.Generic;
using UnityEngine;
using Kebab.BattleEngine.Ships;


namespace Kebab.BattleEngine.Conditions
{
	public class TurnXCondition : baseCondition
	{
		[SerializeField, Min(2)] private int targetTurn = 2;

		private int currentTurn = 0;

		public override void Init()
		{
			BattleManager.instance.OnGameStateChanged.AddListener(UpdateTurn);
		}

		private void UpdateTurn(GamePhaseEnum _)
		{
			currentTurn = BattleManager.instance.CurrentTurn;
			onConditionUpdated.Invoke();

			if (IsComplete())
				onConditionComplete.Invoke();
		}

		public override bool IsComplete()
		{
			return currentTurn >= targetTurn;
		}

		public override Vector2Int State => new Vector2Int(currentTurn, targetTurn);
		public override string ConditionTitle => $"Turn";
	}
}