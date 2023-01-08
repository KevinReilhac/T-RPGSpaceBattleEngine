using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Map;

namespace Kebab.BattleEngine.SaveLoadSystem
{
	public class SaveLoadManager : MonoBehaviour
	{
		public const string SAVE_PATH = "Save/Save";
		public const string SAVE_FILE_EXTENTION = "sav";

		[Button]
		private void Save()
		{
			string fullPath = Path.Combine(Application.streamingAssetsPath,
				string.Format("{0}.{1}",
					SAVE_PATH,
					SAVE_FILE_EXTENTION
				)
			);

			CreateBattleSave().Save(fullPath);
		}

		private BattleMapSaveModel CreateBattleSave()
		{
			BattleMapSaveModel saveFile = new BattleMapSaveModel();
			List<Ship> ships = BattleManager.instance.GetShips(ShipOwner.All);

			foreach (Ship ship in ships)
				saveFile.ships.Add(new ShipSaveModel(ship));

			saveFile.sceneId = SceneManager.GetActiveScene().buildIndex;
			return (saveFile);
		}
	}
}