using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
	private List<Cell> moveCells = null;
	private bool isSelectedShip = false;

	override protected void Awake()
	{
		base.Awake();
	}

	public void OnSelected()
	{
		isSelectedShip = true;
		if (currentActionPoints > 0)
			DrawMoveSelection();
	}

	public void OnUnselected()
	{
		isSelectedShip = false;
	}

	private void Update()
	{
		if (isSelectedShip && Input.GetMouseButtonDown(1))
			BattleManager.instance.UnselectPlayerShip();
	}

	public void DrawMoveSelection()
	{
		List<Cell> movableCells = GetMoveRangeCells();

		movableCells.Where((c) => c.PlacedObject == null).ToList().ForEach((c) => {
			c.SetInteractable(true);
			c.SetInsideColor(Color.blue);
			c.OnSelected = OnMoveCellSelected;
		});

		moveCells = movableCells;
	}

	public void HideMoveSelection()
	{
		moveCells.ForEach((c) => {
			c.SetInteractable(false);
			c.ResetInsideColor();
			c.OnSelected = null;
		});

		moveCells.Clear();
	}

	private void OnMoveCellSelected(Cell targetCell)
	{
		if (targetCell == this.cell)
			return;
		currentActionPoints--;
		BattleManager.instance.GridMap.ResetAllCells();
		MoveTo(targetCell.GridPosition, BattleManager.instance.UnselectPlayerShip);
	}

	public override ShipOwner Owner => ShipOwner.Player;
}