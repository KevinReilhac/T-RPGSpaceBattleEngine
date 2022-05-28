using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Kebab.Managers;

public class BattleManager : Manager<BattleManager>
{
	private baseGrid mapManager = null;
	private UnityEvent<PlayerShip> onShipSelected = new UnityEvent<PlayerShip>();
	private List<Ship> ships = new List<Ship>();

	private PlayerShip selectedPlayerShip = null;
	private bool isPlayerTurn = true;
	private bool canSelectPlayerShips = true;

	protected override void xAwake()
	{
		base.xAwake();
	}

	private void Update()
	{
		if (isPlayerTurn && canSelectPlayerShips)
			SetPlayerShipSelection();
		else
			HidePlayerShipsSelection();
	}

#region EnemyTurn
	public void StartEnemyTurn()
	{
		StartCoroutine(__EnemyTurnCoroutine());
	}

	private IEnumerator __EnemyTurnCoroutine()
	{
		List<EnemyShip> ships = GetShips<EnemyShip>(ShipOwner.Enemy);

		isPlayerTurn = false;
		HidePlayerShipsSelection();
		foreach (EnemyShip ship in ships)
		{
			bool shipPlayed = false;

			Debug.Log(ship);
			ship.Play(() => shipPlayed = true);
			yield return new WaitUntil(() => shipPlayed);
		}
		isPlayerTurn = true;
	}
#endregion

#region PlayerTurn
	public void AddShip(Ship ship)
	{
		ships.Add(ship);
	}

	public void SetPlayerShipSelection()
	{
		List<Ship> ships = GetShips(ShipOwner.Player);

		foreach (Ship ship in ships)
		{
			ship.IsSelectable = true;
			ship.Cell.OnSelected = (c) => SelectPlayerShip(ship as PlayerShip);
		}
	}

	public void HidePlayerShipsSelection()
	{
		List<Ship> ships = GetShips(ShipOwner.Player);

		foreach (Ship ship in ships)
		{
			ship.IsSelectable = false;
			ship.Cell.OnSelected = null;
		}
	}

	public void SelectPlayerShip(PlayerShip ship)
	{
		UnselectPlayerShip();
		selectedPlayerShip = ship;
		selectedPlayerShip.OnSelected();
		UIManager.instance.SelectShip(selectedPlayerShip);
	}

	public void UnselectPlayerShip()
	{
		if (selectedPlayerShip == null)
			return;
		selectedPlayerShip.HideAllSelections();
		selectedPlayerShip.OnUnselected();
		UIManager.instance.SelectShip(null);
	}

#endregion

#region Getters
	public List<Ship> GetShips(ShipOwner shipsOwner = ShipOwner.All)
	{
		List<Ship> goodOwnerShips = new List<Ship>();

		foreach (Ship ship in ships)
		{
			if (shipsOwner.HasFlag(ship.Owner))
				goodOwnerShips.Add(ship);
		}

		return (goodOwnerShips);
	}

	public List<T> GetShips<T>(ShipOwner shipOwner = ShipOwner.All) where T : Ship
	{
		List<Ship> ships = GetShips(shipOwner);
		List<T> typedShips = ships.Select((s) => s as T).ToList();

		return (typedShips);
	}

	public UnityEvent<PlayerShip> OnShipSelected
	{
		get => onShipSelected;
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

	public PlayerShip SelectedPlayerShip
	{
		get => selectedPlayerShip;
	}

	public bool IsPlayerTurn
	{
		get => isPlayerTurn;
	}

	public bool CanSelectPlayerShips
	{
		get => canSelectPlayerShips;
		set => canSelectPlayerShips = value;
	}
#endregion
}
