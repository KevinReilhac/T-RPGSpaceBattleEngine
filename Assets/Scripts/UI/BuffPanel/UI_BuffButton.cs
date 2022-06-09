using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Kebab.BattleEngine.Ships.Buffs.UI
{
	public class UI_BuffButton : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private TextMeshProUGUI text = null;
		[SerializeField] private Button button = null;

		private UnityAction callback = null;

		private string statName = null;
		private float statDelta = 0;

		private void Awake()
		{
			button.onClick.AddListener(OnClick);
		}

		public void Setup(string statName, float statDelta, UnityAction callback)
		{
			this.statDelta = statDelta;
			this.statName = statName;
			this.callback = callback;

			UpdateText();
		}

		public void OnClick()
		{
			if (callback != null)
				callback.Invoke();
		}

		public void UpdateText()
		{
			char sign = statDelta > 0 ? '+' : '-';
			text.text = string.Format("{0} ({1}{2})", statName, sign, statDelta.ToString("0"));
		}

		public UnityEvent OnButonClick
		{
			get => button.onClick;
		}
	}
}