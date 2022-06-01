using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zen.Hexagons;

namespace Kebab.BattleEngine.Extentions
{
	public static class Vector2IntExtention
	{
		public static HexOffsetCoordinates ToHexOffsetCoords(this Vector2Int vector)
		{
			return (new HexOffsetCoordinates(vector.x, vector.y));
		}
	}
}
