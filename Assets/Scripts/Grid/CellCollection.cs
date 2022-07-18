using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.Map
{
	public class CellCollection : List<Cell>
	{
		public CellCollection(List<Cell> cellList) : base(cellList) {}
		public CellCollection() : base() {}

		public Cell GetNearestCell(Vector2 position, bool getFullCells = true)
		{
			List<Vector2> cellsWorldPos = this.Select((c) => (Vector2)c.transform.position).ToList();

			float nearestDist = Mathf.Infinity;
			Cell nearestCell = null;

			for (int i = 0; i < cellsWorldPos.Count; i++)
			{
				float dist = Vector2.Distance(position, cellsWorldPos[i]);

				if (getFullCells == false && this[i].PlacedObject != null)
					continue;

				if (dist < nearestDist)
				{
					nearestDist = dist;
					nearestCell = this[i];
				}
			}

			return (nearestCell);
		}

		public CellCollection GetOnlyEmpty()
		{
			return (new CellCollection(this.Where(c => c.PlacedObject == null).ToList()));
		}
	}
}
