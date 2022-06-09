using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

using Kebab.Extentions.ListExtention;
using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Map;
using Kebab.BattleEngine.Attacks;
using Kebab.BattleEngine.Utils;

namespace Kebab.BattleEngine.Ships.AI
{
	public abstract class baseShipAI : MonoBehaviour
	{
		protected Ship ship = null;

		public void SetupShip(Ship ship)
		{
			this.ship = ship;
		}

		protected SO_Attack SelectPowerfullAttack(Ship target)
		{
			List<SO_Attack> attackList = new List<SO_Attack>(ship.Attacks);
			attackList.Sort((a, b) => ship.GetDamages(a, target).CompareTo(ship.GetDamages(a, target)));

			return (attackList[0]);
		}

		protected SO_Attack SelectPreciseAttack(Ship target)
		{
			List<SO_Attack> attackList = new List<SO_Attack>(ship.Attacks);
			attackList.Sort((a, b) => ship.GetPrecision(a, target).CompareTo(ship.GetDamages(a, target)));

			return (attackList[0]);
		}

		protected Vector2Int GetShipApprochValidPosition(Ship target, int preferedDistance)
		{
			List<Cell> moveRangeCells = ship.GetMoveRangeCells();
			List<Cell> preferedDistanceCells = BattleManager.instance.GridMap.GetCellRange(target.GridPosition, preferedDistance, false);
			List<Cell> interection = moveRangeCells.Intersect(preferedDistanceCells).ToList();

			if (interection.Count > 0)
			{
				interection.Sort((a, b) =>
					Vector2Int.Distance(b.GridPosition, target.GridPosition).CompareTo(Vector2Int.Distance(a.GridPosition, target.GridPosition))
				);

				return (interection[0].GridPosition);
			}


			moveRangeCells.Sort((a, b) =>
				Vector2Int.Distance(a.GridPosition, target.GridPosition).CompareTo(Vector2Int.Distance(b.GridPosition, target.GridPosition))
			);
			return (moveRangeCells[0].GridPosition);
		}

		public abstract void Play(UnityAction onPlayed);
	}
}