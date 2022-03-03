using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : GridMovable
{
	[Header("References")]
	[SerializeField] private SO_Ship shipData = null;
	[SerializeField] private SpriteRenderer spriteRenderer = null;

	private int currentActionPoints = 2;
	private int currentHealth = 0;

	private void Awake()
	{
		ResetStats();
		BattleManager.instance.OnShipSelected.AddListener(OnShipSelected);
		BattleManager.instance.GridMap.OnCellSelected.AddListener(OnMoveCellSelected);
	}

	private void ResetStats()
	{
		currentHealth = shipData.health;
		currentActionPoints = shipData.actionPoints;
	}

	protected override void OnCellSelected()
	{
		base.OnCellSelected();
		BattleManager.instance.SelectShip(this);
	}

	public void SetupData(SO_Ship shipData)
	{
		if (shipData == null)
			return;
		this.shipData = shipData;
		spriteRenderer.sprite = shipData.sprite;
	}

	private void OnShipSelected(Ship ship)
	{
		if (ship == this)
			OnThisShipSelected();
		else
			OnOtherShipSelected();
	}

	private void OnThisShipSelected()
	{
		if (CurrentActionPoints > 0)
			SetMoveSelection();
	}

	private void OnOtherShipSelected()
	{
	}

	private List<Cell> GetMoveRangeCells()
	{
		return BattleManager.instance.GridMap.GetCellRange(GridPosition, shipData.speed);
	}

	private void SetMoveSelection()
	{
		List<Cell> movableCells = GetMoveRangeCells();

		BattleManager.instance.SetShipSelection();
		movableCells.Where((c) => c.PlacedObject == null).ToList().ForEach((c) => {
			c.SetInteractable(true);
			c.SetInsideColor(Color.blue);
		});
	}

	public void HideMoveSelection()
	{
		List<Cell> movableCells = GetMoveRangeCells();

		BattleManager.instance.SetShipSelection();
		movableCells.Where((c) => c.PlacedObject == null).ToList().ForEach((c) => {
			c.SetInteractable(false);
			c.ResetInsideColor();
		});
	}

	private void OnMoveCellSelected(Vector2Int cellPosition)
	{
		if (cellPosition == Cell.GridPosition)
			return;
		if (IsSelectedShip)
		{
			currentActionPoints--;
			BattleManager.instance.GridMap.ResetAllCells();
			MoveTo(cellPosition, () => {
				BattleManager.instance.SelectShip(null);
				BattleManager.instance.SetShipSelection();
			});
		}
	}

#region Getter
	public int CurrentActionPoints
	{
		get => currentActionPoints;
	}

	public int MaxActionPoints
	{
		get => shipData.actionPoints;
	}

	public int CurrentHealth
	{
		get => currentHealth;
	}

	public int MaxHealth
	{
		get => shipData.health;
	}

	public int Speed
	{
		get => shipData.speed;
	}

	public int Armor
	{
		get => shipData.armor;
	}

	public int Evade
	{
		get => shipData.evade;
	}

	public float Flac
	{
		get => shipData.flac;
	}

	public ShipType ShipType
	{
		get => shipData.type;
	}

	public string ShipName
	{
		get => shipData.name;
	}

	public List<SO_Attack> Attacks
	{
		get => shipData.attacks;
	}
#endregion


#region Helpers
	public bool IsSelectedShip
	{
		get => this == BattleManager.instance.SelectedShip;
	}
#endregion
}
