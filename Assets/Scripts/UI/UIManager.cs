using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
	[SerializeField] private UI_ShipDetailsPanel shipDetailsPanel = null;
	[SerializeField] private UI_ActionsPanel actionsPanel = null;
	[SerializeField] private Canvas canvas = null;

	private Ship selectedShip = null;

	protected override void xAwake()
	{
		base.xAwake();

		SelectShip(null);
		BattleManager.instance.OnShipSelected.AddListener(SelectShip);
	}

	public void SelectShip(Ship ship)
	{
		shipDetailsPanel.SetShip(ship);
		actionsPanel.Setup(ship);
		selectedShip = ship;
	}

	public Canvas Canvas
	{
		get => canvas;
	}

	public Ship SelectedShip
	{
		get => selectedShip;
	}

	private void OnDestroy()
	{
		BattleManager.instance.OnShipSelected.RemoveListener(SelectShip);
	}
}
