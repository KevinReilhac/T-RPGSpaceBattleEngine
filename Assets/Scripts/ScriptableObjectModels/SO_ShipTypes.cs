using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;

[CreateAssetMenu(fileName = "ShipTypes", menuName = "DesignData/ShipTypes")]
public class SO_ShipTypes : baseDesignData
{
	[System.Serializable]
	public class DamageMultiplicatorByType
	{
		[SerializeField] public int type = 0;
		[SerializeField] public List<float> damagesFromType = new List<float>();

		public DamageMultiplicatorByType(int type, int typeNumber)
		{
			this.type = type;
			damagesFromType = Enumerable.Range(0, typeNumber).Select(x => 1f).ToList();
		}
	}

	[SerializeField] public List<string> types;
	[SerializeField] public List<DamageMultiplicatorByType> damagesFromTypes;

	public float GetDamageMultiplicator(int from, int to)
	{

		if (damagesFromTypes == null || (from < 0 || from >= damagesFromTypes.Count) || (to < 0 || to >= damagesFromTypes.Count))
		{
			Debug.LogError("Can't find damage multiplier");
			return (1);
		}

		return (damagesFromTypes[from].damagesFromType[to]);
	}
}
