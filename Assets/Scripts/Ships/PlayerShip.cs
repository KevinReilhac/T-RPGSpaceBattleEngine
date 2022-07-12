using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.BattleEngine.Map;
using Kebab.BattleEngine.Attacks;

using Kebab.UISystem;
using Kebab.DesignData;

namespace Kebab.BattleEngine.Ships
{
	public class PlayerShip : Ship
	{
		private List<Cell> moveCells = null;
		private bool isSelectedShip = false;
		private CellColorsDesignData cellColorsDesignData = null;

		override protected void Awake()
		{
			base.Awake();
			cellColorsDesignData = DesignDataManager.Get<CellColorsDesignData>();
		}

		public void Select()
		{
			isSelectedShip = true;
			if (currentActionPoints > 0)
				DrawMoveSelection();

			UIManager.instance.GetPanel<UI.UI_ActionsPanel>().SetShip(this);
			UIManager.instance.GetPanel<UI.UI_ShipDetailsPanel>().SetShip(this);
		}

		public void Unselect()
		{
			isSelectedShip = false;
			HideAllSelections();

			UIManager.instance.GetPanel<UI.UI_ActionsPanel>().SetShip(null);
			UIManager.instance.GetPanel<UI.UI_ShipDetailsPanel>().SetShip(null);
		}

		private void Update()
		{
			if (isSelectedShip && Input.GetMouseButtonDown(1))
				BattleManager.instance.UnselectPlayerShip();
		}

		public void EndAction()
		{
			currentActionPoints--;
			BattleManager.instance.UnselectPlayerShip();
			BattleManager.instance.SetCanSelectPlayerShips(true);
		}

		public void HideAllSelections()
		{
			ClearMoveSelection();
			HideAttackSelection();
		}

		#region Attack
		public void DrawAttackSelection(SO_Attack attack)
		{
			if (currentActionPoints <= 0)
				return;

			List<Cell> enemyShipCells = GetEnemyShipCells();

			foreach (Cell cell in enemyShipCells)
			{
				cell.SetInteractable(true);
				cell.SetInsideColor(cellColorsDesignData.attackFillColor);
				cell.OnSelected = (c) =>
				{
					Ship target = (Ship)c.PlacedObject;

					Attack(attack, target, null);
					EndAction();
				};
			}
		}

		private void HideAttackSelection()
		{
			List<Cell> enemyShipCells = GetEnemyShipCells();

			foreach (Cell cell in enemyShipCells)
			{
				cell.SetInteractable(true);
				cell.ResetInsideColor();
				cell.OnSelected = null;
			}
		}

		private List<Cell> GetEnemyShipCells()
		{
			return BattleManager.instance.GetShips(ShipOwner.Enemy)
				.Select((s) => s.Cell)
				.ToList();
		}
		#endregion

		#region Move
		public void DrawMoveSelection()
		{
			List<Cell> movableCells = GetMoveRangeCells();

			movableCells.Where((c) => c.PlacedObject == null).ToList().ForEach((c) =>
			{
				c.SetInteractable(true);
				c.SetInsideColor(cellColorsDesignData.movableFillColor);
				c.OnSelected = OnMoveCellSelected;
			});

			moveCells = movableCells;
		}

		public void ClearMoveSelection()
		{
			moveCells.ForEach((c) =>
			{
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
			BattleManager.instance.GridMap.ResetAllCells();
			MoveTo(targetCell.GridPosition);
			EndAction();
		}
		#endregion

		public override ShipOwner Owner => ShipOwner.Player;
	}
}