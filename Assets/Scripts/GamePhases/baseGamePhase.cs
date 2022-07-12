using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.GamePhases
{
	public abstract class baseGamePhase
	{
		public abstract void Begin();
		public abstract void Stop();
		public abstract void Update();
		public abstract string Name {get;}
	}
}