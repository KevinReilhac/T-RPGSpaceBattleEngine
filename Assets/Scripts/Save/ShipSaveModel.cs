using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.BattleEngine.Ships;

namespace Kebab.BattleEngine.SaveLoadSystem
{
	[System.Serializable]
	public class ShipSaveModel
	{
		public SO_Ship shipData;
		public int currentHp = 0;
		public int currentActionPoints = 0;
		public bool ennemyShip = false;
		public Vector2Int gridPosition = new Vector2Int();

		public ShipSaveModel(Ship ship)
		{
			ennemyShip = ship.Owner == ShipOwner.Enemy;
			currentHp = ship.CurrentHealth;
			currentActionPoints = ship.CurrentActionPoints;
			gridPosition = ship.GridPosition;
			shipData = ship.ShipData;
		}
	}
}