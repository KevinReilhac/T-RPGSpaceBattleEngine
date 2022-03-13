using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes2D;
using Zen.Hexagons;

public class HexGrid : baseGrid
{
	public const float HEX_BOUNDS_RATIO_Y = 0.8660254f;

	protected override void Awake()
	{
		base.Awake();
		Hex.RADIUS = cellSize;
	}

	private Vector2 GetCellsOffset()
	{
		return new Vector2((cellSize / 2f) * 3f, cellSize * HEX_BOUNDS_RATIO_Y / 2f);
	}

	protected override Vector2 GetCellLocalPosFromGridPos(int x, int y)
	{
		Vector2 point = HexLib.FromOffsetCoordinatesToPixel(new HexOffsetCoordinates(x, y));

		return (point);
	}

	private HexLibrary hexLib = null;
	private HexLibrary HexLib
	{
		get
		{
			if (hexLib == null)
				hexLib = new HexLibrary(Zen.Hexagons.HexType.FlatTopped, Zen.Hexagons.OffsetCoordinatesType.Odd, cellSize / 2);
			return (hexLib);
		}
	}

	public override List<Cell> GetCellRange(Vector2Int center, int range, bool getFullCells = true)
	{
		List<Cell> cells = new List<Cell>();

		for (int i = 1; i <= range; i++)
		{
			HexOffsetCoordinates[] offsetCoords = HexLib.GetSingleRing(new HexOffsetCoordinates(center.x, center.y), i);

			foreach (HexOffsetCoordinates coord in offsetCoords)
			{
				if (mapMatrix.ContainsKey(coord.ToVector2Int()))
				{
					Cell cell = mapMatrix[coord.ToVector2Int()];

					if (!getFullCells && cell.PlacedObject != null)
						continue;

					cells.Add(cell);
				}
			}
		}

		return (cells);
	}

	public override List<Cell> GetCellLine(Vector2Int p1, Vector2Int p2, bool blocking = false)
	{
		List<Cell> cells = new List<Cell>();
		HexOffsetCoordinates[] line = HexLib.GetLine(p1.ToHexOffsetCoords(), p2.ToHexOffsetCoords());

		foreach (HexOffsetCoordinates coord in line)
		{
			if (mapMatrix.ContainsKey(coord.ToVector2Int()))
			{
				Cell cell = mapMatrix[coord.ToVector2Int()];

				cells.Add(cell);
				if (blocking && cell.PlacedObject != null)
					break;
			}
		}

		return (cells);
	}
}
