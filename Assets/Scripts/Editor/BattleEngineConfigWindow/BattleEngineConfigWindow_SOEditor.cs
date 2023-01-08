using System.Globalization;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

using Kebab.BattleEngine.Ships;
using Kebab.Extentions.StringExtention;

public class BattleEngineConfigWindow_SOEditor : EditorWindow
{
	private static ScriptableObject target = null;
	private static Editor editor = null;
	private static bool isCreate = false;
	private static string savePath = "";
	private static UnityAction updateCallback = null;

	public static void EditWindow(ScriptableObject SOtarget, UnityAction onDestroyed)
	{
		BattleEngineConfigWindow_SOEditor wnd = GetWindow<BattleEngineConfigWindow_SOEditor>();
		wnd.titleContent = new GUIContent($"Edit {SOtarget.name}");
		wnd.ShowPopup();


		target = SOtarget;
		editor = Editor.CreateEditor(target);
		isCreate = false;
		updateCallback = onDestroyed;
	}

	public static void CreateWindow<T>(string path, UnityAction onCreated) where T : ScriptableObject
	{
		BattleEngineConfigWindow_SOEditor wnd = GetWindow<BattleEngineConfigWindow_SOEditor>();
		wnd.titleContent = new GUIContent("Create " + typeof(T).Name);
		wnd.ShowPopup();

		updateCallback = onCreated;
		target = ScriptableObject.CreateInstance<T>();
		isCreate = true;
		editor = Editor.CreateEditor(target);
		savePath = path;
	}

	private void OnGUI()
	{
		if (target == null)
			return;
		target.name = EditorGUILayout.TextField("Name", target.name);
		editor.OnInspectorGUI();


		GUILayout.FlexibleSpace();
		if (isCreate)
		{
			GUI.enabled = CanSave();
			if (GUILayout.Button(new GUIContent("Save", EditorGUIUtility.IconContent("SaveAs").image)))
				Save();
		}
		else
		{
			if (GUILayout.Button(new GUIContent("Delete", EditorGUIUtility.IconContent("TreeEditor.Trash").image)))
				Delete();
		}
		GUI.enabled = true;
	}

	private bool CanSave()
	{
		return (!target.name.IsNullOrEmpty());
	}

	private void Save()
	{
		AssetDatabase.CreateAsset(target, GetSavePath(target.name));
		Debug.Log(target.name + " Saved at " + GetSavePath(target.name));
		if (updateCallback != null)
			updateCallback.Invoke();
		Close();
	}

	private void Delete()
	{
		bool confirm = EditorUtility.DisplayDialog(
			"Are you sure ?",
				$"Do you want to delete {target.name}",
				"Yes",
				"No"
		);

		if (confirm)
		{
			Debug.Log(target.name + " Deleted at " + AssetDatabase.GetAssetPath(target));
			AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(target));
			updateCallback.Invoke();
		}

		Close();
	}

	private string GetSavePath(string fileName)
	{
		return Path.Combine("Assets/Resources", savePath, fileName + ".asset");
	}
}
