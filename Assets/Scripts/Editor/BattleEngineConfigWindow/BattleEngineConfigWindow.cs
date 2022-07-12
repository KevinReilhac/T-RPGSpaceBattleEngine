using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BattleEngineConfigWindow : EditorWindow
{
	public enum SubWindow
	{
		Ships,
		Attacks,
		Settings,
	}

	private static SubWindow selectedWindow = SubWindow.Ships;
	private static BattleEngineConfigWindow_Ships shipWindow = null;
	private static BattleEngineConfigWindow_Attacks attackWindow = null;
	private static BattleEngineConfigWindow_Settings settingsWindow = null;

	[MenuItem("Tools/BattleEngine Configurator")]
	private static void ShowWindow()
	{
		var window = GetWindow<BattleEngineConfigWindow>();
		window.titleContent = GetTitleContent();
		window.Show();
	}

	private static GUIContent GetTitleContent()
	{
		GUIContent content = new GUIContent(EditorGUIUtility.IconContent("_Popup"));

		content.text = "BattleEngine Configurator";
		return (content);
	}

	private void OnEnable()
	{
		shipWindow = ScriptableObject.CreateInstance<BattleEngineConfigWindow_Ships>();
		shipWindow.SetMainWindow(this);

		attackWindow = ScriptableObject.CreateInstance<BattleEngineConfigWindow_Attacks>();
		attackWindow.SetMainWindow(this);

		settingsWindow = ScriptableObject.CreateInstance<BattleEngineConfigWindow_Settings>();
	}

	public void OnGUI()
	{
		selectedWindow = GUIToolbar(selectedWindow);

		switch (selectedWindow)
		{
			case SubWindow.Ships:
				shipWindow.OnGUI();
				break;
			case SubWindow.Attacks:
				attackWindow.OnGUI();
				break;
			case SubWindow.Settings:
				settingsWindow.OnGUI();
				break;
		}
	}

	private SubWindow GUIToolbar(SubWindow selectedWindow)
	{
		return (SubWindow)GUILayout.Toolbar((int)selectedWindow, Enum.GetNames(typeof(SubWindow)));
	}
}
