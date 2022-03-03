using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActionsPanel : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform buttonsParent = null;
	[Header("Prefabs")]
	[SerializeField] private UI_ActionsPanelButton buttonPrefab = null;

	public void Setup(Ship ship)
	{
		gameObject.SetActive(ship != null);
		if (ship == null)
			return;
		buttonsParent.ClearChilds();
		ship.Attacks.ForEach(CreateButton);
	}

	private void CreateButton(SO_Attack attack)
	{
		UI_ActionsPanelButton buttonInstance = Instantiate(buttonPrefab, buttonsParent);

		buttonInstance.Setup(attack);
	}
}
