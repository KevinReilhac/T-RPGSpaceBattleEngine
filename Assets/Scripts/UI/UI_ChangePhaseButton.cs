using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kebab.UISystem;

namespace Kebab.BattleEngine.UI
{
	[RequireComponent(typeof(Button))]
	public class UI_ChangePhaseButton : baseUIPanel
	{
		[SerializeField] private GamePhaseEnum newGamePhase = GamePhaseEnum.NONE;

		private Button button = null;

		private void Awake()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			BattleManager.instance.SetGamePhase(newGamePhase);
		}
	}
}