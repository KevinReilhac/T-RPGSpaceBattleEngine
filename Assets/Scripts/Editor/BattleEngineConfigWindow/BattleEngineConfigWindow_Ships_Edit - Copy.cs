/*
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Kebab.BattleEngine.Ships;
using Kebab.Extentions.StringExtention;

public class BattleEngineConfigWindow_Ships_Edit : EditorWindow
{
	private static SO_Ship targetShip = null;
	private static SO_Ship tmpShip = null;
	private static Editor tmpShipEditor = null;

	public static void ShowWindow(SO_Ship ship = null)
	{
		BattleEngineConfigWindow_Ships_Edit wnd = GetWindow<BattleEngineConfigWindow_Ships_Edit>();
		wnd.titleContent = new GUIContent(ship == null ? "Create ship" : $"Edit {ship.name}");
		wnd.ShowPopup();

		tmpShip = ScriptableObject.CreateInstance<SO_Ship>();

		if (ship != null)
		{
			targetShip = ship;
			tmpShip.SuckData(targetShip);
		}

		tmpShipEditor = Editor.CreateEditor(tmpShip);
	}

	private void OnGUI()
	{
		tmpShip.name = GUILayout.TextField(tmpShip.name);
		tmpShipEditor.OnInspectorGUI();

		GUI.enabled = CanSave();
		if (GUILayout.Button("Save"))
			Save();
		GUI.enabled = true;
	}

	private bool CanSave()
	{
		return (!tmpShip.name.IsNullOrEmpty());
	}

	private void Save()
	{
		if (targetShip != null)
		{
			targetShip.SuckData(tmpShip);
		}
		else
		{
			AssetDatabase.CreateAsset(tmpShip, GetSavePath(tmpShip.name));
			Close();
		}

		BattleEngineConfigWindow_Ships.UpdateShipList();
	}

	void OnDestroy()
	{
		tmpShip.name = "";
		tmpShip = null;
	}


	private string GetSavePath(string fileName)
	{
		return Path.Combine("Assets/Resources", BattleEngineConfigWindow_Ships.SHIPS_DATA_PATH, fileName + ".asset");
	}
}
*/