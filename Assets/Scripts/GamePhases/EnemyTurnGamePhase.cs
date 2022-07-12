using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.BattleEngine.Ships;

namespace Kebab.BattleEngine.GamePhases
{
	public class EnemyTurnGamePhase : baseGamePhase
	{
		public override void Begin()
		{
			BattleManager.instance.StartCoroutine(__EnemyTurnCoroutine());
		}

		private IEnumerator __EnemyTurnCoroutine()
		{
			List<EnemyShip> ships = BattleManager.instance.GetShips<EnemyShip>(ShipOwner.Enemy);

			foreach (EnemyShip ship in ships)
			{
				bool shipPlayed = false;

				if (BattleManager.instance.GetShips(ShipOwner.Player).Count == 0)
					break;
				ship.Play(() => shipPlayed = true);
				yield return new WaitUntil(() => shipPlayed);
			}
			BattleManager.instance.GetShips(ShipOwner.Player).ForEach((s) => s.ResetActionPoints());
			BattleManager.instance.SetGamePhase(GamePhaseEnum.PlayerTurn);
		}

		public override void Stop()
		{
		}

		public override void Update()
		{
			//Empty implementation
		}

		public override string Name => "Enemy turn";
	}
}