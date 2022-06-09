using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Kebab.BattleEngine.Ships.EditorTools
{
	[CustomEditor(typeof(ShipTypesDesignData))]
	public class ShipTypesDesignDataCustomInspector : Editor
	{
		private ShipTypesDesignData shipTypes = null;
		private ReorderableList typeReorderableList = null;
		private SerializedProperty typeListProperty = null;

		private void OnEnable()
		{
			shipTypes = (ShipTypesDesignData)target;

			if (shipTypes.damagesFromTypes == null)
				shipTypes.damagesFromTypes = new List<ShipTypesDesignData.DamageMultiplicatorByType>();

			//List
			typeListProperty = serializedObject.FindProperty("types");
			typeReorderableList = new ReorderableList(serializedObject, typeListProperty, false, true, true, true);
			typeReorderableList.drawElementCallback += DrawListItems;
			typeReorderableList.drawHeaderCallback += DrawHeader;
			typeReorderableList.onChangedCallback += (_) => UpdateDamageTypeDict();
		}

		public override void OnInspectorGUI()
		{
			typeReorderableList.DoLayoutList();
			EditorGUILayout.Space();
			DrawDamagesFromType();

			serializedObject.ApplyModifiedProperties();
		}

		// Draws the elements on the list
		void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
		{
			SerializedProperty element = typeReorderableList.serializedProperty.GetArrayElementAtIndex(index); // The element in the list

			//Create a property field and label field for each property. 

			//The 'mobs' property. Since the enum is self-evident, I am not making a label field for it. 
			//The property field for mobs (width 100, height of a single line)
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, 200f, EditorGUIUtility.singleLineHeight),
				element,
				GUIContent.none
			);
		}

		//Draws the header
		void DrawHeader(Rect rect)
		{
			string name = "Ship types";
			EditorGUI.LabelField(rect, name);
		}

		private void DrawDamagesFromType()
		{
			EditorGUILayout.LabelField("Damages multipliers");
			if (shipTypes.damagesFromTypes.Count <= 0)
				return;

			EditorGUILayout.BeginHorizontal();
			GUI.enabled = false;
			EditorGUILayout.TextField("From / To");
			for (int i = 0; i < shipTypes.types.Count; i++)
				EditorGUILayout.TextField(shipTypes.types[i]);
			GUI.enabled = true;
			EditorGUILayout.EndHorizontal();
			for (int x = 0; x < shipTypes.types.Count; x++)
			{
				EditorGUILayout.BeginHorizontal();
				GUI.enabled = false;
				EditorGUILayout.TextField(shipTypes.types[x]);
				GUI.enabled = true;

				for (int y = 0; y < shipTypes.types.Count; y++)
				{
					EditorGUILayout.BeginVertical();
					shipTypes.damagesFromTypes[x].damagesFromType[y] = EditorGUILayout.FloatField(shipTypes.damagesFromTypes[x].damagesFromType[y]);
					EditorGUILayout.EndVertical();
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		private void UpdateDamageTypeDict()
		{
			serializedObject.ApplyModifiedProperties();
			while (shipTypes.damagesFromTypes.Count < shipTypes.types.Count)
				shipTypes.damagesFromTypes.Add(new ShipTypesDesignData.DamageMultiplicatorByType(shipTypes.damagesFromTypes.Count, shipTypes.types.Count));

			foreach (ShipTypesDesignData.DamageMultiplicatorByType item in shipTypes.damagesFromTypes)
			{
				while (item.damagesFromType.Count < shipTypes.types.Count)
					item.damagesFromType.Add(1f);
			}
		}

		private List<(int, int)> GetTypesCombination()
		{
			List<int> types = Enumerable.Range(0, shipTypes.types.Count).ToList();

			return (GetPermutationsWithRept<int>(types, 2).Select(c => (c[0], c[1])).ToList());
		}

		private List<List<T>> GetPermutationsWithRept<T>(List<T> list, int length)
		{
			if (length == 1)
				return list.Select(t => new List<T> { t }).ToList();
			return GetPermutationsWithRept(list, length - 1).SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }).ToList()).ToList();
		}
	}
}