using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

namespace Kebab.BattleEngine.Conditions
{
	[System.Serializable]
	public class ACondition : MonoBehaviour
	{
		public virtual bool IsComplete() {return false;}
		public virtual string ConditionTitle { get; }
		public virtual Vector2Int State { get; }
		public virtual void Init() {}
		
		protected UnityEvent onConditionUpdated = new UnityEvent();
		protected UnityEvent onConditionComplete = new UnityEvent();

		public UnityEvent OnConditionUpdated {get => onConditionUpdated;}
		public UnityEvent OnConditionComplete {get => onConditionComplete;}
	}
}