using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActionsPanel : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform buttonsParent = null;
	[Header("Prefabs")]
	[SerializeField] private UI_ActionsPanelButton buttonPrefab = null;

	private PlayerShip ship = null;

	public void Setup(PlayerShip ship)
	{
		this.ship = ship;
		gameObject.SetActive(ship != null);
		if (ship == null)
			return;
		buttonsParent.ClearChilds();
		ship.Attacks.ForEach(CreateButton);
	}

	private void CreateButton(SO_Attack attack)
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
