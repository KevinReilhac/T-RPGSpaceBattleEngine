using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Kebab.BattleEngine.UI;
using Kebab.DesignData;
using Kebab.UISystem;

namespace Kebab.BattleEngine.Ships.Buffs.UI
{
	public class UI_BuffPanel : baseUIPanel
	{
		[Header("References")]
		[SerializeField] private UI_BuffButton buttonPrefab = null;
		[SerializeField] private Transform buttonsParent = null;

		private PlayerShip target = null;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				BattleManager.instance.SelectPlayerShip(target);
				Hide();
			}
		}

		private void Awake()
		{
			CreateButtons();
		}

		public void Setup(PlayerShip ship)
		{
			Show();
			target = ship;
		}

		private void CreateButtons()
		{
			BuffsDesignData buffsDeltas = DesignDataManager.Get<BuffsDesignData>();

			CreateButton("Armor", buffsDeltas.armor, () => target.Buffs.armor += (int)buffsDeltas.armor);
			CreateButton("Speed", buffsDeltas.speed, () => target.Buffs.speed += (int)buffsDeltas.speed);
			CreateButton("Evade", buffsDeltas.evade, () => target.Buffs.evade += (int)buffsDeltas.evade);
			CreateButton("Flac", buffsDeltas.flac * 100, () => target.Buffs.flac += buffsDeltas.flac);
		}

		private void CreateButton(string statName, float statDelta, UnityAction callback)
		{
			UI_BuffButton instance = Instantiate(buttonPrefab, buttonsParent);

			instance.Setup(statName, statDelta, callback);
			instance.OnButonClick.AddListener(Hide);
			instance.OnButonClick.AddListener(() => target.EndAction());
			BattleManager.instance.UnselectPlayerShip();
		}
	}
}