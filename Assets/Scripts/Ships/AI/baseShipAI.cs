using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Map;
using Kebab.BattleEngine.Attacks;

namespace Kebab.BattleEngine.Ships.AI
{
	public abstract class baseShipAI : MonoBehaviour
	{
		protected Ship ship = null;

		public void SetupShip(Ship ship)
		{
			this.ship = ship;
		}

		public abstract void Play(UnityAction onPlayed);
	}
}