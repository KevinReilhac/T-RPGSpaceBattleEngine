using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.DesignData;

using Kebab.BattleEngine.Map;

[CreateAssetMenu(fileName = "CellColorsDesignData", menuName = "DesignData/CellColorsDesignData", order = 20)] 
public class CellColorsDesignData : baseDesignData
{
	public Color hoverOutlineColor = Color.blue;
	public Color selectableOutlineColor = Color.white;
	public Color defaultOutlineColor = Color.gray;
	public Color clickedOutlineColor = Color.green;
	public Color movableFillColor = Color.blue;
	public Color attackFillColor = Color.red;
	public Color separationFillColor = Color.red;
	public Color playerSideFillColor = Color.blue;
}