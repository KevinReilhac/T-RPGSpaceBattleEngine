using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.DesignData;
using Kebab.Managers;

namespace Kebab.BattleEngine.Difficulty
{
	public class DifficultyManager : Manager<DifficultyManager>
	{
		private DifficultyDesignData difficultyDesignData = null;

		public List<string> GetDifficultyNames()
		{
			return DifficultyDesignData.difficulties.Select(d => d.name).ToList();
		}

		public void SetDifficulty(int difficultyIndex)
		{
			if (difficultyIndex < 0 || difficultyIndex >= DifficultyDesignData.difficulties.Count)
			{
				Debug.LogError("Difficulty index out of bounds");
				return;
			}
			CurrentDifficultyIndex = difficultyIndex;
		}

		public void SetDifficulty(string difficultyName)
		{
			DifficultyData difficulty = DifficultyDesignData.difficulties.Find((d) => d.name == difficultyName);

			if (difficulty == null)
			{
				Debug.LogErrorFormat("\"{0}\" difficulty not exist", difficultyName);
				return;
			}

			CurrentDifficultyIndex = DifficultyDesignData.difficulties.IndexOf(difficulty);
		}

		public DifficultyData CurrentDifficulty
		{
			get
			{
				int currentDifficultyIndex = CurrentDifficultyIndex;

				if (currentDifficultyIndex >= 0 && currentDifficultyIndex < DifficultyDesignData.difficulties.Count)
					return DifficultyDesignData.difficulties[currentDifficultyIndex];
				Debug.LogError("Difficulty index out of bounds");
				return new DifficultyData();
			}
		}

		public int CurrentDifficultyIndex
		{
			get => PlayerPrefs.GetInt("Difficulty", 0);
			set => PlayerPrefs.SetInt("Difficulty", value);
		}

		private DifficultyDesignData DifficultyDesignData
		{
			get
			{
				if (difficultyDesignData == null)
					difficultyDesignData = DesignDataManager.Get<DifficultyDesignData>();
				return (difficultyDesignData);
			}
		}
	}
}