using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Kebab.Extentions.ListExtention;

using Kebab.BattleEngine.Attacks;

namespace Kebab.BattleEngine.Ships.AI
{
	public class BasicShipAI : baseShipAI
	{
		private UnityAction onPlayed = null;

		public override void Play(UnityAction onPlayed)
		{
			this.onPlayed = onPlayed;
			Ship targetShip = GetTargetShip();
			SO_Attack attack = SelectPowerfullAttack(targetShip);

			float distanceFromTarget = Vector2Int.Distance(targetShip.GridPosition, ship.GridPosition);

			if (distanceFromTarget > attack.normalDistanceRange.max)
			{
				ship.MoveTo(GetShipApprochValidPosition(targetShip, attack.normalDistanceRange.max - 1), onPlayed);
				return;
			}
			ship.Attack(attack, targetShip, onPlayed);
		}

		private Ship GetTargetShip()
		{
			List<Ship> ships = BattleManager.instance.GetShips(ShipOwner.Player);
			float nearestShipDistance = ships.Min((s) => Vector2Int.Distance(ship.GridPosition, s.GridPosition));
			List<Ship> nearestShips = ships.Where((s) => Vector2Int.Distance(ship.GridPosition, s.GridPosition) == nearestShipDistance).ToList();

			return (nearestShips.GetRandom());
		}
	}
}