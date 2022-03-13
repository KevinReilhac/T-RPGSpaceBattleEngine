using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Flags]
public enum ShipOwner
{
	All = -1,
	None = 0,
	Player = 1,
	Enemy = 2
}

[RequireComponent(typeof(SpriteRenderer))]
public class Ship : GridMovable
{
	[Header("References")]
	[SerializeField] private SO_Ship shipData = null;
	
	private SpriteRenderer spriteRenderer = null;

	protected int currentActionPoints = 2;
	protected int currentHealth = 0;

	virtual protected void Awake()
	{
		BattleManager.instance.AddShip(this);
		spriteRenderer = GetComponent<SpriteRenderer>();
		ResetStats();
	}

	public void ResetStats()
	{
		currentHealth = shipData.health;
		currentActionPoints = shipData.actionPoints;
	}

	public void SetupData(SO_Ship shipData)
	{
		if (shipData == null)
			return;
		this.shipData = shipData;
		spriteRenderer.sprite = shipData.sprite;
	}

	protected List<Cell> GetMoveRangeCells()
	{
		return BattleManager.instance.GridMap.GetCellRange(GridPosition, shipData.speed, false);
	}

	virtual public ShipOwner Owner => ShipOwner.None;

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

}
