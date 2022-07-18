using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Kebab.BattleEngine.Utils.Editor;
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
			EditorUtils.DrawHeader(allDesignData[i].name);
			designDataEditors[i].OnInspectorGUI();
			EditorUtils.HorizontalLine();
			EditorGUILayout.Space();
		}
		GUILayout.EndScrollView();
	}

	private void GetDesignData()
	{
		allDesignData = Resources.LoadAll<baseDesignData>(DESIGN_DATA_PATH);
		designDataEditors = allDesignData.Select(d => Editor.CreateEditor(d)).ToArray();
	}
}
