using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class baseVictoryCondition : MonoBehaviour
{
	public abstract bool IsValidate();
	public abstract string ConditionTitle {get;}
}