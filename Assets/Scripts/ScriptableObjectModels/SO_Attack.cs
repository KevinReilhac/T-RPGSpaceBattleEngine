using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Data/Attack", fileName = "Attack")]
public class SO_Attack : ScriptableObject
{
	public string attackName = "Laser";
	public Vector2Int damages = new Vector2Int(10, 30);
	public int precision = 10;
	public bool ignoreShield = false;
	public Sprite sprite = null;
}
