using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Attacks;
using Kebab.Extentions;

namespace Kebab.BattleEngine.UI
{
	public class UI_ActionsPanel : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private Transform buttonsParent = null;
		[Header("Prefabs")]
		[SerializeField] private UI_ActionsPanelButton buttonPrefab = null;
		[SerializeField] private Button buffButton = null;

		private PlayerShip ship = null;

		private void Awake()
		{
			buffButton.onClick.AddListener(UIManager.instance.OpenBuffPanel);
		}

		public void Setup(PlayerShip ship)
		{
			this.ship = ship;
			gameObject.SetActive(ship != null);
			if (ship == null)
				return;
			buttonsParent.ClearChilds();
			ship.Attacks.ForEach(CreateAttackButton);
			buffButton.interactable = (ship.CurrentActionPoints > 0);
		}

		private void CreateAttackButton(SO_Attack attack)
		{
			UI_ActionsPanelButton buttonInstance = Instantiate(buttonPrefab, buttonsParent);

			buttonInstance.SetInteractable(ship.CurrentActionPoints > 0);
			buttonInstance.Setup(attack);
			buttonInstance.OnSelected.AddListener(OnAttackSelected);
		}

		private void OnAttackSelected(SO_Attack attack)
		{
			ship.DrawAttackSelection(attack);
		}
	}
}