using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
	Destroyer,
	Cuirrase,
	Chasseur,
	Corvette
}

[CreateAssetMenu(menuName = "Data/Ship", fileName = "Ship")]
public class SO_Ship : ScriptableObject
{
	public int price = 1000;
	public int health = 100;
	public int actionPoints = 2;
	public int speed = 3;
	public int evade = 20;
	public int armor = 20;
	[Range(0f, 1f)]
	public float flac = 0f;
	public ShipType type = ShipType.Destroyer;
	public List<SO_Attack> attacks = new List<SO_Attack>();
	public Sprite sprite = null;
}
