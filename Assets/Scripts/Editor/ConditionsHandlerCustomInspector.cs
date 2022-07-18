using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Kebab.BattleEngine.Utils.Editor;

namespace Kebab.BattleEngine.Conditions.EditorTools
{
	[CustomEditor(typeof(ConditionsHandler))]
	public class ConditionsHandlerCustomInspector : Editor
	{
		public enum ConditionsTypes
		{
			WIN,
			LOSE
		}
		public const string WIN_PARENT_NAME = "WinConditions";
		public const string LOSE_PARENT_NAME = "LoseConditions";

		private Transform winGroupsParent = null;
		private Transform loseGroupsParent = null;
		private ConditionsTypes toolbarConditionsType = ConditionsTypes.WIN;
		private Editor currentGroupEditor = null;
		private int groupDropdownValue = 0;

		ConditionsHandler handler = null;

		private void OnEnable()
		{
			handler = (ConditionsHandler)target;
			UpdateParents();
			GetGroupsFromChilds();
			SetDefaultConditions();
			UpdateEditor();
		}

		private void SetDefaultConditions()
		{
			if (handler.winConditionsGroups.Count == 0)
				AddConditionsGroup(ConditionsTypes.WIN);
			if (handler.loseConditionsGroups.Count == 0)
				AddConditionsGroup(ConditionsTypes.LOSE);
		}

		private void GetGroupsFromChilds()
		{
			handler.winConditionsGroups = GetParent(WIN_PARENT_NAME).gameObject.GetComponentsInChildren<ConditionsGroup>().ToList();
			handler.loseConditionsGroups = GetParent(LOSE_PARENT_NAME).gameObject.GetComponentsInChildren<ConditionsGroup>().ToList();
		}

		#region InitParents
		private void UpdateParents()
		{
			if (winGroupsParent == null)
				winGroupsParent = GetParent(WIN_PARENT_NAME);
			if (loseGroupsParent == null)
				loseGroupsParent = GetParent(LOSE_PARENT_NAME);
		}

		private Transform GetParent(string parentName)
		{
			Transform parent = handler.transform.Find(parentName);
			if (parent == null)
			{
				parent = new GameObject(parentName).transform;
				parent.parent = handler.transform;
			}

			return (parent);
		}
		#endregion

		public override void OnInspectorGUI()
		{
			DrawToolbar();

			EditorGUILayout.BeginHorizontal();
			DrawGroupSelection();
			DrawGroupAddButton();
			EditorGUILayout.EndHorizontal();
			DrawRemoveGroupButton();

			EditorUtils.HorizontalLine();
			DrawGroupEditor();
		}

		private void DrawRemoveGroupButton()
		{
			GUIContent deleteButton = new GUIContent()
			{
				text = "Remove group",
				image = EditorGUIUtility.IconContent("TreeEditor.Trash").image
			};

			GUI.enabled = GetSelectedGroups().Count > 1;
			if (GUILayout.Button(deleteButton))
				RemoveGroup();
			GUI.enabled = true;
		}

		private void RemoveGroup()
		{
			ConditionsGroup currentGroup = GetSelectedGroup();

			GetSelectedGroups().Remove(currentGroup);
			DestroyImmediate(currentGroup.gameObject);
			UpdateEditor();
		}

		private ref List<ConditionsGroup> GetSelectedGroups()
		{
			if (toolbarConditionsType == ConditionsTypes.WIN)
				return ref handler.winConditionsGroups;
			else
				return ref handler.loseConditionsGroups;
		}

		private ConditionsGroup GetSelectedGroup()
		{
			groupDropdownValue = Mathf.Clamp(groupDropdownValue, 0, GetSelectedGroups().Count - 1);

			return (GetSelectedGroups()[groupDropdownValue]);
		}

		private void DrawGroupAddButton()
		{
			if (GUILayout.Button(EditorGUIUtility.IconContent("Toolbar Plus"), EditorStyles.iconButton))
			{
				AddConditionsGroup(toolbarConditionsType);
				groupDropdownValue++;
			}
		}

		private void DrawGroupSelection()
		{
			List<ConditionsGroup> groups = GetSelectedGroups();

			int newGroupSelection = EditorGUILayout.Popup(groupDropdownValue, groups.Select(c => c.name).ToArray());

			if (newGroupSelection != groupDropdownValue)
			{
				groupDropdownValue = newGroupSelection;
				UpdateEditor();
			}
		}

		private void UpdateEditor()
		{
			currentGroupEditor = Editor.CreateEditor(GetSelectedGroup());
		}

		private void DrawToolbar()
		{
			ConditionsTypes newConditionsType = (ConditionsTypes)GUILayout.Toolbar((int)toolbarConditionsType, Enum.GetNames(typeof(ConditionsTypes)));

			if (newConditionsType != toolbarConditionsType)
			{
				groupDropdownValue = 0;
				toolbarConditionsType = newConditionsType;
				UpdateEditor();
			}
		}

		private void DrawGroupEditor()
		{
			if (currentGroupEditor == null)
				return;

			GetSelectedGroup().name = EditorGUILayout.TextField("Group name", GetSelectedGroup().name);
			EditorUtils.DrawHeader("Conditions");
			EditorGUI.indentLevel++;
			currentGroupEditor.OnInspectorGUI();
			EditorGUI.indentLevel--;
		}

		private Transform GetGroupsParent(ConditionsTypes conditionType)
		{
			return conditionType == ConditionsTypes.WIN ? winGroupsParent : loseGroupsParent;
		}

		private ConditionsGroup AddConditionsGroup(ConditionsTypes conditionType)
		{
			Transform parent = GetGroupsParent(conditionType);

			GameObject instance = new GameObject(
				string.Format("{0} conditions group {1}", conditionType == ConditionsTypes.WIN ? "Win" : "Lose", parent.childCount + 1),
				typeof(ConditionsGroup)
			);

			instance.transform.parent = parent;
			ConditionsGroup group = instance.GetComponent<ConditionsGroup>();
			GetSelectedGroups().Add(group);
			groupDropdownValue = GetSelectedGroups().IndexOf(group);
			UpdateEditor();
			return (group);
		}
	}
}