using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.Utils
{
	public class SelectionMultiplicator
	{
		[SerializeField] private string leftName = null;
		[SerializeField] private string rightName = null;
		[SerializeField] private float value = 0;

		public float LeftValueMultiplicator => 1 - value;
		public float RightValueMultiplicator => 1;

		public SelectionMultiplicator(string leftName, string rightName, float value)
		{
			this.leftName = leftName;
			this.rightName = rightName;
			this.value = value;
		}
	}
}