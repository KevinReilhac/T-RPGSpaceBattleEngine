using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kebab.Extentions.ListExtention;
using Kebab.BattleEngine.Attacks;

namespace Kebab.BattleEngine.Ships.AI
{
	public class RandomShipAI : baseShipAI
	{
		public override void Play(UnityAction onPlayed)
		{
			float randomValue = UnityEngine.Random.Range(0f, 1f);

			if (randomValue > 0.5f)
				RandomAttack(onPlayed);
			else
				RandomMove(onPlayed);
		}

		private void RandomMove(UnityAction onPlayed)
		{
			ship.MoveTo(ship.GetMoveRangeCells().GetRandom().GridPosition, onPlayed);
		}

		private void RandomAttack(UnityAction onPlayed)
		{
			SO_Attack randomAttack = ship.ShipData.attacks.GetRandom();

			ship.Attack(randomAttack, GetRandomTarget(), onPlayed);
		}

		private Ship GetRandomTarget()
		{
			return BattleManager.instance.GetShips(ShipOwner.Player).GetRandom();
		}
	}
}