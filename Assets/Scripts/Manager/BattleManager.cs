using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : Manager<BattleManager>
{
	private baseGrid mapManager = null;
	private UnityEvent<Ship> onShipSelected = new UnityEvent<Ship>();
	private List<Ship> ships = new List<Ship>();

	private Ship selectedShip = null;

	protected override void xAwake()
	{
		base.xAwake();
		ships.AddRange(GameObject.FindObjectsOfType<Ship>().ToList());
	}

	private void Start()
	{
		SelectShip(null);
	}

	public void SelectShip(Ship ship)
	{
		if (selectedShip != null)
			selectedShip.HideMoveSelection();

		SetShipSelection();
		selectedShip = ship;
		onShipSelected.Invoke(ship);
	}

	public void SetShipSelection()
	{
		Debug.Log("Ship selection");
		foreach (Ship ship in ships)
		{
			ship.Cell.SetInteractable(true);
		}
	}

	public void AddShip(Ship ship)
	{
		ships.Add(ship);
	}

#region Getters
	public UnityEvent<Ship> OnShipSelected
	{
		get => onShipSelected;
	}

	public Ship SelectedShip
	{
		get => selectedShip;
	}

	public baseGrid GridMap
	{
		get
		{
			if (mapManager == null)
				mapManager = GameObject.FindObjectOfType<baseGrid>();
			return mapManager;
		}
	}
#endregion
}
