using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.DesignData;

namespace Kebab.BattleEngine.Difficulty
{
	[System.Serializable]
	public class DifficultyData
	{
		public string name = "Default";
		public float enemyDamageMultiplicator = 1;
		public float enemyLifeMultiplicator = 1;
	}

	[CreateAssetMenu(fileName = "DifficultyDesignData", menuName = "DesignData/DifficultyDesignData", order = 20)]
	public class DifficultyDesignData : baseDesignData
	{
		public List<DifficultyData> difficulties = new List<DifficultyData>();
	}
}