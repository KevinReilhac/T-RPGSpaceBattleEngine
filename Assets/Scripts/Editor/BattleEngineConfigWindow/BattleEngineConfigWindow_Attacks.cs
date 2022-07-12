using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Kebab.BattleEngine.Attacks;

public class BattleEngineConfigWindow_Attacks : baseBattleEngineConfigWindow_ListEditor<SO_Attack>
{
	protected override Vector2 ButtonSize => new Vector2(100, 100);
	protected override string DataPath => "Data\\Attacks";
	protected override string[] SortTypes => new string[]
	{
		"Name",
		"MinDamages",
		"MaxDamages",
		"Precision",
	};

	protected override GUIContent GetObjectGUIContent(SO_Attack target)
	{
		Texture texture = null;

		if (target.sprite != null)
			texture = target.sprite.texture;

		return new GUIContent()
		{
			image = texture,
			text = target.name,
			tooltip = GetToolTip(target)
		};
	}

	protected override string GetToolTip(SO_Attack target)
	{
		StringBuilder str = new StringBuilder();

		str.AppendLine(target.name);
		str.AppendLine("");
		str.AppendLine($"Damages : {target.damages}");
		str.AppendLine($"Health : {target.normalDistanceRange}");
		str.AppendLine($"Precision : {target.precision}");

		return (str.ToString());
	}

	protected override int SortComparision(SO_Attack a, SO_Attack b)
	{
		return (GetSortValue(a).CompareTo(GetSortValue(b)));
	}

	private float GetSortValue(SO_Attack attack)
	{
		switch (CurrentSortType)
		{
			default:
			case "MinDamages":
				return attack.damages.min;
			case "MaxDamages":
				return attack.damages.max;
			case "ActionPoints":
				return attack.precision;
		}
	}
}
