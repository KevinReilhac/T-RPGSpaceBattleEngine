using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Data/Attack", fileName = "Attack")]
public class SO_Attack : ScriptableObject
{
	[Header("Settings")]
	public Sprite sprite = null;
	public string attackName = "Laser";
	[HideIf("ignoreEvade")] public int precision = 10;
	public RangeInt damages = new RangeInt(10, 30);
	public RangeInt normalDistanceRange = new RangeInt(2, 5);

	[Header("Ignores")]
	public bool ignoreEvade = false;
	public bool ignoreFlac = false;
	public bool ignoreArmor = false;
	[Header("References")]
	public AttackVisual attackVisual = null;
}