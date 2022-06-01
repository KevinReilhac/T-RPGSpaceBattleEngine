using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.BattleEngine.Map;

namespace Kebab.BattleEngine.SaveLoadSystem
{
	public class BattleMapSaveModel
	{
		public List<ShipSaveModel> ships = new List<ShipSaveModel>();
		public Vector2Int gridSize = Vector2Int.zero;
		public GridType gridType = GridType.HEX;

		private static BattleMapSaveModel Load(string path)
		{
			string json = File.ReadAllText(path);

			return (JsonUtility.FromJson<BattleMapSaveModel>(json));
		}

		public void Save(string path)
		{
			string data = JsonUtility.ToJson(this);

			File.WriteAllText(path, JsonUtility.ToJson(this, true));
		}
	}
}
