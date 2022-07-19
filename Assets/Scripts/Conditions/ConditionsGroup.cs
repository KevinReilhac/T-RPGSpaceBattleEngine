using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kebab.BattleEngine.Conditions
{
	[System.Serializable]
	public class ConditionsGroup : MonoBehaviour
	{
		public List<baseCondition> conditions = new List<baseCondition>();

		private UnityEvent onComplete = new UnityEvent();
		private int completed = 0;

		public void Init()
		{
			foreach (baseCondition condition in conditions)
			{
				condition.OnConditionComplete.AddListener(UpdateCompletedConditions);
				condition.Init();
			}
		}

		public void Dispose()
		{
			foreach (baseCondition condition in conditions)
				condition.OnConditionComplete.RemoveListener(UpdateCompletedConditions);
		}

		private void UpdateCompletedConditions()
		{
			completed++;
			if (completed >= conditions.Count)
				OnComplete.Invoke();
		}

		public UnityEvent OnComplete { get => onComplete;}
	}
}