using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShipDetailsPanel : MonoBehaviour
{
	[SerializeField] private Text shipNameText = null;
	[SerializeField] private Image hpImage = null;
	[SerializeField] private Text actionPointText = null;
	[SerializeField] private Text typeText = null;
	[SerializeField] private Text speedText = null;
	[SerializeField] private Text evadeText = null;
	[SerializeField] private Text armorText = null;
	[SerializeField] private Text flac = null;

	private Ship ship = null;

	public void SetShip(Ship ship)
	{
		gameObject.SetActive(ship != null);
		this.ship = ship;
	}

	private void Update()
	{
		DrawShipData();
	}

	private void DrawShipData()
	{
		if (ship == null)
			return;

		shipNameText.text = ship.ShipName;
		hpImage.fillAmount = ship.CurrentHealth / ship.MaxHealth;
		actionPointText.text = string.Format("{0} / {1}", ship.CurrentActionPoints, ship.MaxActionPoints);
		typeText.text = ship.ShipType.ToString();
		speedText.text = ship.Speed.ToString();
		evadeText.text = ship.Evade.ToString();
		armorText.text = ship.Armor.ToString();
		flac.text = (ship.Flac * 100).ToString() + '%';
	}

}