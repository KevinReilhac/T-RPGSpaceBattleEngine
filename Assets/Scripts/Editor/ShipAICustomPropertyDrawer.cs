using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Kebab.BattleEngine.Utils;


namespace Kebab.BattleEngine.Ships.AI.EditorTools
{
	[CustomPropertyDrawer(typeof(ShipAIType))]
	public class ShipAICustomPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty typeProperty = property.FindPropertyRelative("typeName");
			EditorGUI.BeginProperty(position, label, property);
			List<Type> aiTypes = ReflectionUtils.GetAllSubtypes(typeof(baseShipAI));
			List<string> aiNames = aiTypes.Select((t) => t.Name).ToList();
			int currentlySelected = aiNames.FindIndex((n) => n == typeProperty.stringValue);
			List<string> dropdownList = new List<string>();

			if (currentlySelected == -1)
			{
				currentlySelected = 0;
				dropdownList.Add("< NONE >");
			}
			dropdownList.AddRange(aiNames.ToArray());

			int selectedIndex = EditorGUILayout.Popup("AI Type", currentlySelected, dropdownList.ToArray());
			typeProperty.stringValue = dropdownList[selectedIndex];

			EditorGUI.EndProperty();
		}
	}
}