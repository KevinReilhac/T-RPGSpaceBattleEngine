using System.Text;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Kebab.BattleEngine.Ships;

public class BattleEngineConfigWindow_Ships : baseBattleEngineConfigWindow_ListEditor<SO_Ship>
{
	protected override Vector2 ButtonSize => new Vector2(150, 100);
	protected override string DataPath => "Data\\Ships";
	protected override string[] SortTypes => new string[]
	{
		"Name",
		"Health",
		"ActionPoints",
		"Speed",
		"Evade",
		"Armor",
		"Price",
		"Flac"
	};

	protected override GUIContent GetObjectGUIContent(SO_Ship target)
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

	protected override string GetToolTip(SO_Ship target)
	{
		StringBuilder str = new StringBuilder();

		str.AppendLine(target.name);
		str.AppendLine("");
		str.AppendLine($"Price : {target.price}");
		str.AppendLine($"Health : {target.health}");
		str.AppendLine($"Actions : {target.actionPoints}");
		str.AppendLine($"Speed : {target.speed}");
		str.AppendLine($"Armor : {target.armor}");
		str.AppendLine($"Evade : {target.evade}");
		str.AppendLine($"Flac : {target.flac}");

		return (str.ToString());
	}

	protected override int SortComparision(SO_Ship a, SO_Ship b)
	{
		if (CurrentSortType == "Name")
			return (a.name.CompareTo(b.name));
		return (GetSortValue(a).CompareTo(GetSortValue(b)));
	}

	private float GetSortValue(SO_Ship ship)
	{
		switch (CurrentSortType)
		{
			default:
			case "Price":
				return ship.price;
			case "Health":
				return ship.health;
			case "ActionPoints":
				return ship.actionPoints;
			case "Armor":
				return ship.armor;
			case "Speed":
				return ship.speed;
			case "Evade":
				return ship.evade;
			case "Flac":
				return ship.flac;
		}
	}

}
