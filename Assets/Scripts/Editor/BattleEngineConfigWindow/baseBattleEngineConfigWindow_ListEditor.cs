using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class baseBattleEngineConfigWindow_ListEditor<SOType> : EditorWindow where SOType : ScriptableObject
{
	protected int sortTypeIndex = 0;
	private bool invertSort = true;
	private Vector2 scrollView = new Vector2();
	private List<SOType> objects = null;
	private BattleEngineConfigWindow window = null;

	public void SetMainWindow(BattleEngineConfigWindow window)
	{
		this.window = window;
	}

	private void OnEnable()
	{
		UpdateObjectList();
	}

	public void OnGUI()
	{
		if (GUILayout.Button("Create..."))
			Create();
		DrawSortDropdowns();
		DrawObjectsList();
	}

	private void DrawSortDropdowns()
	{
		int newSortValue = 0;
		bool newInvertValue = false;

		GUILayout.BeginHorizontal();
		newSortValue = EditorGUILayout.Popup(sortTypeIndex, SortTypes);
		newInvertValue = EditorGUILayout.Popup(invertSort ? 1 : 0, new string[] { "Ascending", "Descending" }) == 1;
		if (GUILayout.Button(EditorGUIUtility.IconContent("Refresh"), GUILayout.Width(50)))
			UpdateObjectList();
		GUILayout.EndHorizontal();

		if (newInvertValue != invertSort || newSortValue != sortTypeIndex)
		{
			invertSort = newInvertValue;
			sortTypeIndex = newSortValue;

			SortList();
		}
	}

	private void Create()
	{
		Debug.Log(typeof(SOType).Name);
		BattleEngineConfigWindow_SOEditor.CreateWindow<SOType>(DataPath, UpdateObjectList);
	}

	protected void SortList()
	{
		objects.Sort(SortComparision);
		if (invertSort)
			objects.Reverse();
	}


	private void DrawObjectsList()
	{
		GUIContent[] shipsContent = objects.Where(s => s != null).Select(s => GetObjectGUIContent(s)).ToArray();

		int xCount = Mathf.Max(1, (int)(window.position.width / ButtonSize.x));
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
		{
			fixedHeight = ButtonSize.y,
			fixedWidth = Mathf.Max(ButtonSize.x, (window.position.width - 20) / xCount),
			imagePosition = ImagePosition.ImageAbove,
		};
		scrollView = GUILayout.BeginScrollView(scrollView);
		int selection = GUILayout.SelectionGrid(-1, shipsContent, xCount, buttonStyle);
		GUILayout.EndScrollView();


		if (selection != -1)
		{
			if (objects[selection] == null)
			{
				Debug.LogError(selection);
				return;
			}
			BattleEngineConfigWindow_SOEditor.EditWindow(objects[selection], UpdateObjectList);
		}
	}


	public void UpdateObjectList()
	{
		objects = Resources.LoadAll<SOType>(DataPath).ToList();

		SortList();
	}

	protected string CurrentSortType
	{
		get
		{
			if (sortTypeIndex < 0 || sortTypeIndex >= SortTypes.Length)
				return SortTypes[0];
			return (SortTypes[sortTypeIndex]);
		}
	}

	#region Abstracts
	protected abstract Vector2 ButtonSize {get ;}
	protected abstract string DataPath {get ;}
	protected abstract string[] SortTypes {get; }
	protected abstract string GetToolTip(SOType target);
	protected abstract int SortComparision(SOType a, SOType b);
	protected abstract GUIContent GetObjectGUIContent(SOType target);
	#endregion
}
