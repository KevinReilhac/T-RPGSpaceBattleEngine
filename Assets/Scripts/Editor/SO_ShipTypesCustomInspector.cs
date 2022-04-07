using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SO_ShipTypes))]
public class SO_ShipTypesCustomInspector : Editor
{
	private SO_ShipTypes shipTypes = null;
	private static bool drawDefaultUI = true;

	private void OnEnable()
	{
		shipTypes = (SO_ShipTypes)target;

		if (shipTypes.damagesFromTypes == null)
			shipTypes.damagesFromTypes = new List<SO_ShipTypes.DamageMultiplicatorByType>();
		if (shipTypes.types == null)
			shipTypes.types = new List<string>();
		UpdateDamageTypeDict();
	}

	public override void OnInspectorGUI()
	{

		DrawTypesList();
		EditorGUILayout.Space();
		DrawDamagesFromType();

		drawDefaultUI = EditorGUILayout.Foldout(drawDefaultUI, "Default UI");

		if (drawDefaultUI)
			base.OnInspectorGUI();
	}

	private void Test()
	{
		for (int x = 0; x < shipTypes.types.Count; x++)
		{
			for (int y = 0; x < shipTypes.types.Count; y++)
			{
				Debug.LogFormat("{0}-->{1}------>{2}", shipTypes.types[x], shipTypes.types[y], shipTypes.GetDamageMultiplicator(x, y));
			}
		}
	}

	private void DrawTypesList()
	{
		for (int i = 0; i < shipTypes.types.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();
			shipTypes.types[i] = EditorGUILayout.TextField(shipTypes.types[i]);
			if (GUILayout.Button("Remove", GUILayout.Width(100f)))
			{
				shipTypes.types.RemoveAt(i);
				UpdateDamageTypeDict();
			}
			EditorGUILayout.EndHorizontal();
		}

		if (GUILayout.Button("Add Type"))
		{
			shipTypes.types.Add("Type " + (shipTypes.types.Count + 1).ToString());
			UpdateDamageTypeDict();
		}
	}

	private void DrawDamagesFromType()
	{
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
		while (shipTypes.damagesFromTypes.Count < shipTypes.types.Count)
			shipTypes.damagesFromTypes.Add(new SO_ShipTypes.DamageMultiplicatorByType(shipTypes.damagesFromTypes.Count, shipTypes.types.Count));

		foreach (SO_ShipTypes.DamageMultiplicatorByType item in shipTypes.damagesFromTypes)
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
