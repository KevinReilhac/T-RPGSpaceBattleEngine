using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GridPlacable : MonoBehaviour
{
	protected Cell cell = null;

	private void Start()
	{
		AlignToGrid();
	}

	virtual protected void OnCellSelected()
	{
		//Empty implementation
	}

	[Button]
	public void AlignToGrid()
	{
		SetCell(BattleManager.instance.GridMap.GetNearestCell(transform.position));
		transform.position = cell.transform.position;
	}

	protected void SetCell(Cell cell)
	{
		if (this.cell != null)
		{
			this.cell.PlacedObject = null;
			this.cell.OnSelected.RemoveListener(OnCellSelected);
		}
		this.cell = cell;
		cell.PlacedObject = this;
		cell.OnSelected.AddListener(OnCellSelected);
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
