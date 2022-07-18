using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kebab.BattleEngine.Utils.Editor
{
	public static class EditorUtils
	{
		public static void DrawHeader(string label)
		{
			EditorGUILayout.Space();
			GUILayout.Label(label, EditorStyles.boldLabel);
		}

		public static void HorizontalLine(int size = 1)
		{
			EditorGUILayout.Space();
			Rect rect = EditorGUILayout.GetControlRect(false, size);
			rect.height = size;
			EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
			EditorGUILayout.Space();
		}
	}
}