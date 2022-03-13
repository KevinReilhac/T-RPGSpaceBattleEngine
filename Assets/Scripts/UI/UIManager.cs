using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
	[SerializeField] private UI_ShipDetailsPanel shipDetailsPanel = null;
	[SerializeField] private UI_ActionsPanel actionsPanel = null;
	[SerializeField] private UI_SupportShipPanel supportPanel = null;
	[SerializeField] private Button endTurnButton = null;
	[SerializeField] private Canvas canvas = null;

	private Ship selectedShip = null;

	protected override void xAwake()
	{
		base.xAwake();

		SelectShip(null);
		BattleManager.instance.OnShipSelected.AddListener(SelectShip);
		endTurnButton.onClick.AddListener(BattleManager.instance.StartEnemyTurn);
	}

	private void Update()
	{
		endTurnButton.gameObject.SetActive(BattleManager.instance.IsPlayerTurn);
		supportPanel.gameObject.SetActive(BattleManager.instance.IsPlayerTurn);
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
