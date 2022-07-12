using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.Map
{
	[System.Serializable]
	public class CellColors
	{
		public Color fill = Color.clear;
		public Color outline = Color.white;

		public CellColors(Color fill, Color outline)
		{
			this.fill = fill;
			this.outline = outline;
		}
	}
}