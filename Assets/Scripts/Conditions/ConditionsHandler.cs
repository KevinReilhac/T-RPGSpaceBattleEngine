using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kebab.BattleEngine.Conditions
{
	public class ConditionsHandler : MonoBehaviour
	{
		public List<ConditionsGroup> winConditionsGroups = null;
		public List<ConditionsGroup> loseConditionsGroups = null;

		private bool isActive = false;

		public UnityEvent onLose = new UnityEvent();
		public UnityEvent onWin = new UnityEvent();

		private void Awake()
		{
			winConditionsGroups.ForEach(c => c.OnComplete.AddListener(onWin.Invoke));
			loseConditionsGroups.ForEach(c => c.OnComplete.AddListener(onLose.Invoke));
		}

		public void Init()
		{
			winConditionsGroups.ForEach(c => c.Init());
			loseConditionsGroups.ForEach(c => c.Init());

			isActive = true;
		}

		public void Dispose()
		{
			winConditionsGroups.ForEach(c => c.Dispose());
			loseConditionsGroups.ForEach(c => c.Dispose());

			isActive = false;
		}

		public bool IsActive
		{
			get => isActive;
		}
	}
}