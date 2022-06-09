using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.DesignData;

namespace Kebab.BattleEngine.Ships.Buffs
{
	[CreateAssetMenu(fileName = "BuffsDesignData", menuName = "DesignData/BuffsDesignData", order = 20)]
	public class BuffsDesignData : baseDesignData
	{
		public float armor = 10;
		public float speed = 1;
		public float evade = 10;
		public float flac = 2;
	}
}