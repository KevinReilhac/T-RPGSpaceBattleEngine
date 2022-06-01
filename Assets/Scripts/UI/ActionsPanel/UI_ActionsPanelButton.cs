using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Attacks;

namespace Kebab.BattleEngine.UI
{
	[RequireComponent(typeof(Button))]
	public class UI_ActionsPanelButton : MonoBehaviour
	{
		[SerializeField] private Image image = null;
		[SerializeField] private Button button = null;

		private UnityEvent<SO_Attack> onSelected = new UnityEvent<SO_Attack>();
		private SO_Attack attackData = null;

		private void Awake()
		{
			button.onClick.AddListener(() => OnSelected.Invoke(attackData));
		}

		public void Setup(SO_Attack attackData)
		{
			image.sprite = attackData.sprite;
			this.attackData = attackData;
		}

		public void SetInteractable(bool isInteractable)
		{
			button.interactable = isInteractable;
		}

		public UnityEvent<SO_Attack> OnSelected
		{
			get => onSelected;
		}
	}
}