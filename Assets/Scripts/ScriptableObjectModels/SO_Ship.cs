using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;



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
	[Dropdown("GetTypesList")] public int type = 0;
	public Sprite sprite = null;
	public List<SO_Attack> attacks = new List<SO_Attack>();

	public DropdownList<int> GetTypesList
	{
		get
		{
			SO_ShipTypes shipTypes = DesignDataManager.Get<SO_ShipTypes>();
			DropdownList<int> list = new DropdownList<int>();

			for (int i = 0; i < shipTypes.types.Count; i++)
				list.Add(shipTypes.types[i], i);
			return (list);
		}
	}
}
