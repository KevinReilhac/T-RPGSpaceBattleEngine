using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Kebab.BattleEngine.Utils;
using Kebab.BattleEngine.Utils.Editor;

namespace Kebab.BattleEngine.Conditions.EditorTools
{
	[CustomEditor(typeof(ConditionsGroup))]
	public class ConditionsGroupHandlerCustomInspector : Editor
	{
		private ConditionsGroup group = null;
		private List<Editor> conditionsEditors = new List<Editor>();
		private List<Type> conditionsTypes = null;

		private void OnEnable()
		{
			group = target as ConditionsGroup;
			if (target == null)
				return;

			conditionsTypes = ReflectionUtils.GetAllSubtypes(typeof(baseCondition));

			group.conditions = group.gameObject.GetComponentsInChildren<baseCondition>().ToList();
			conditionsEditors = group.conditions.Select((c) => Editor.CreateEditor(c)).ToList();
		}

		public override void OnInspectorGUI()
		{
			DrawConditionsEditors();
			EditorGUILayout.Space();
			DrawAddDropdown();
		}

		public void DrawAddDropdown()
		{
			string[] names = conditionsTypes.Select(c => c.Name).ToArray();
			GUIContent addButton = new GUIContent()
			{
				tooltip = "Add Condition",
				image = EditorGUIUtility.IconContent("Toolbar Plus More").image,
			};

			if (EditorGUILayout.DropdownButton(addButton, FocusType.Passive, GUI.skin.button))
				CreateGenericMenu().ShowAsContext();
		}

		private GenericMenu CreateGenericMenu()
		{
			GenericMenu menu = new GenericMenu();

			foreach (Type type in conditionsTypes)
			{
				Type currentType = type;
				menu.AddItem(new GUIContent(currentType.Name), false, CreateCondition, currentType);
			}

			return (menu);
		}

		public void CreateCondition(object typeObj)
		{
			Type type = (Type)typeObj;
			GameObject go = new GameObject(type.Name, type);

			go.transform.parent = group.transform;
			baseCondition condition = go.GetComponent<baseCondition>();
			group.conditions.Add(condition);
			conditionsEditors.Add(Editor.CreateEditor(condition));
		}

		public void RemoveCondition(Editor conditionEditor)
		{
			baseCondition condition = (baseCondition)conditionEditor.target;
			group.conditions.Remove(condition);
			DestroyImmediate(condition.gameObject);
			conditionsEditors.Remove(conditionEditor);
		}

		public void DrawConditionsEditors()
		{
			foreach (Editor editor in conditionsEditors)
			{
				EditorUtils.DrawHeader("> " + editor.target.name);
				editor.serializedObject.DrawInspectorExcepMName();

				if (GUILayout.Button(new GUIContent("Remove", EditorGUIUtility.IconContent("TreeEditor.Trash").image)))
				{
					RemoveCondition(editor);
					break;
				}

				EditorGUILayout.Space();
			}
		}
	}
}