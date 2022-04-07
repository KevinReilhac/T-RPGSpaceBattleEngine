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

	private void EndAction()
	{
		currentActionPoints--;
		BattleManager.instance.UnselectPlayerShip();
	}

	public void HideAllSelections()
	{
		HideMoveSelection();
		HideAttackSelection();
	}

#region Attack
 	public void DrawAttackSelection(SO_Attack attack)
	{
		if (currentActionPoints <= 0)
			return;

		List<Cell> cells = BattleManager.instance.GetShips(ShipOwner.Enemy)
			.Select((s) => s.Cell)
			.ToList();
		
		foreach (Cell cell in cells)
		{
			cell.SetInteractable(true);
			cell.SetInsideColor(Color.red);
			cell.OnSelected = (c) => {
				Ship target = (Ship)c.PlacedObject;

				Attack(attack, target);
				EndAction();
			};
		}
	}

	private void HideAttackSelection()
	{
		List<Cell> cells = BattleManager.instance.GetShips(ShipOwner.Enemy)
			.Select((s) => s.Cell)
			.ToList();
		
		foreach (Cell cell in cells)
		{
			cell.SetInteractable(true);
			cell.ResetInsideColor();
			cell.OnSelected = null;
		}
	}
#endregion

#region Move
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
		EndAction();
		BattleManager.instance.GridMap.ResetAllCells();
		MoveTo(targetCell.GridPosition);
	}
#endregion

public override ShipOwner Owner => ShipOwner.Player;
}