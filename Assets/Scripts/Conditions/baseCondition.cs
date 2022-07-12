using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.Conditions
{
	public abstract class baseCondition : MonoBehaviour
	{
		public abstract bool IsValidate();
		public abstract string ConditionTitle { get; }
	}
}