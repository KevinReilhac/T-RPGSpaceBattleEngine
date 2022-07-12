using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Kebab.DesignData;

public class BattleEngineConfigWindow_Settings : EditorWindow
{
	public const string DESIGN_DATA_PATH = "DesignData";

	private static Vector2 scrollView = new Vector2();
	private baseDesignData[] allDesignData = null;
	private Editor[] designDataEditors = null;

	private void OnEnable()
	{
		GetDesignData();
	}

	public void OnGUI()
	{
		scrollView = GUILayout.BeginScrollView(scrollView);
		for (int i = 0; i < designDataEditors.Length; i++)
		{
			GUILayout.Label(allDesignData[i].name, EditorStyles.boldLabel);
			designDataEditors[i].OnInspectorGUI();
			GuiLine();
			EditorGUILayout.Space();
		}
		GUILayout.EndScrollView();
	}

	private void GetDesignData()
	{
		allDesignData = Resources.LoadAll<baseDesignData>(DESIGN_DATA_PATH);
		designDataEditors = allDesignData.Select(d => Editor.CreateEditor(d)).ToArray();
	}

	void GuiLine(int i_height = 1)
	{

		Rect rect = EditorGUILayout.GetControlRect(false, i_height);

		rect.height = i_height;

		EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
	}
}
