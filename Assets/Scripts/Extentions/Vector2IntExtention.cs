using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zen.Hexagons;

public static class Vector2IntExtention
{
	public static HexOffsetCoordinates ToHexOffsetCoords(this Vector2Int vector)
	{
		return (new HexOffsetCoordinates(vector.x, vector.y));
	}

	public static int GetRandomRange(this Vector2Int vector)
	{
		return (Random.Range(vector.x, vector.y));
	}
}
