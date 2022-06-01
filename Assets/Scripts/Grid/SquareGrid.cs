using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.Map
{
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

		public override List<Cell> GetCellRange(Vector2Int center, int range, bool getFullCells = true)
		{
			//TODO
			return (null);
		}

		public override List<Cell> GetCellLine(Vector2Int p1, Vector2Int p2, bool isBlocking = false)
		{
			//TODO
			return (null);
		}

		public override GridType GridType => GridType.SQUARE;
	}
}