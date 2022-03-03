using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class BattleEngineMenuItems
{
	[MenuItem("GameObject/BattleEngine/Grids/SquareGrid", false, 40)]
	private static void CreateSquareGrid()
	{
		GameObject go = new GameObject("SquareGrid", typeof(SquareGrid));
		SquareGrid grid = go.GetComponent<SquareGrid>();

		grid.GenerateMap();
	}

	[MenuItem("GameObject/BattleEngine/Grids/HexGrid", false, 40)]
	private static void CreateHexGrid()
	{
		GameObject go = new GameObject("SquareGrid", typeof(HexGrid));
		HexGrid grid = go.GetComponent<HexGrid>();

		grid.GenerateMap();
	}
}
