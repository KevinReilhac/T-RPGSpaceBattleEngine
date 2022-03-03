using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGrid : baseGrid
{
	private static void CreateSquareGrid()
	{
		GameObject go = new GameObject("SquareGrid", typeof(SquareGrid));
		SquareGrid grid = go.GetComponent<SquareGrid>();

		grid.GenerateMap();
	}

	protected override Vector2 GetCellLocalPosFromGridPos(int x, int y)
	{
		return new Vector2(x * cellSize, y * cellSize);
	}

	public override List<Cell> GetCellRange(Vector2Int center, int range)
	{
		return (null);
	}
}
