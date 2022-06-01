using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kebab.BattleEngine.Ships.AI
{
	public class DebugShipAI : baseShipAI
	{
		public override void Play(UnityAction onPlayed)
		{
			Debug.Log(name + " : Debug AI Played");
			onPlayed.Invoke();
		}
	}
}