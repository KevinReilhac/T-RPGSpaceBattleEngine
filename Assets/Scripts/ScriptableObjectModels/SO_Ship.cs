using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Kebab.DesignData;
using Kebab.BattleEngine.Attacks;

namespace Kebab.BattleEngine.Ships
{
	[CreateAssetMenu(menuName = "Data/Ship", fileName = "Ship")]
	public class SO_Ship : ScriptableObject
	{
		public Sprite sprite = null;
		public int price = 1000;
		public int health = 100;
		public int actionPoints = 2;
		public int speed = 3;
		public int evade = 20;
		public int armor = 20;
		[Range(0f, 1f)]
		public float flac = 0f;
		[Dropdown("GetTypesList")] public int type = 0;
		public List<SO_Attack> attacks = new List<SO_Attack>();

		public DropdownList<int> GetTypesList
		{
			get
			{
				ShipTypesDesignData shipTypes = GetShipTypesDesignData();
				DropdownList<int> list = new DropdownList<int>();

				for (int i = 0; i < shipTypes.types.Count; i++)
					list.Add(shipTypes.types[i], i);
				return (list);
			}
		}

		private ShipTypesDesignData GetShipTypesDesignData()
		{
			return Resources.Load<ShipTypesDesignData>("DesignData/ShipTypes");
		}

		public void SuckData(in SO_Ship ship)
		{
			this.name = ship.name;
			this.price = ship.price;
			this.health = ship.health;
			this.actionPoints = ship.actionPoints;
			this.speed = ship.speed;
			this.evade = ship.evade;
			this.armor = ship.armor;
			this.flac = ship.flac;
			this.type = ship.type;
			this.sprite = ship.sprite;
			this.attacks = new List<SO_Attack>(ship.attacks);
		}
	}
}
