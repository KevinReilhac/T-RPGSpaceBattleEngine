using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Kebab.BattleEngine.Map
{
	public class GridPlacable : MonoBehaviour
	{
		protected Cell cell = null;

		private void Start()
		{
			AlignToGrid();
		}

		public bool IsSelectable
		{
			get => cell.IsInteractable;
			set => cell.SetInteractable(value);
		}

		[Button]
		public void AlignToGrid()
		{
			SetCell(BattleManager.instance.GridMap.GetNearestCell(transform.position));
			if (cell != null)
				transform.position = cell.transform.position;
		}

		protected void SetCell(Cell cell)
		{
			if (cell == null)
			{
				Debug.LogError("Cell is null");
				return;
			}
			if (this.cell != null)
			{
				this.cell.PlacedObject = null;
			}
			this.cell = cell;
			cell.PlacedObject = this;
		}

		public Vector2Int GridPosition
		{
			get => cell.GridPosition;
		}

		public Cell Cell
		{
			get => cell;
		}
	}
}