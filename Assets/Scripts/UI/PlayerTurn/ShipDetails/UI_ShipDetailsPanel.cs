using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Kebab.BattleEngine.Ships;
using Kebab.DesignData;
using Kebab.UISystem;

namespace Kebab.BattleEngine.Ships.UI
{
	public class UI_ShipDetailsPanel : baseUIPanel
	{
		[SerializeField] private TextMeshProUGUI shipNameText = null;
		[SerializeField] private Image hpFillImage = null;
		[SerializeField] private UI_ActionPointBulletList actionPoints = null;
		[SerializeField] private TextMeshProUGUI typeText = null;
		[SerializeField] private TextMeshProUGUI speedText = null;
		[SerializeField] private TextMeshProUGUI evadeText = null;
		[SerializeField] private TextMeshProUGUI armorText = null;
		[SerializeField] private TextMeshProUGUI flac = null;


		private ShipTypesDesignData shipTypes = null;
		private Ship ship = null;

		private void Awake()
		{
			shipTypes = DesignDataManager.Get<ShipTypesDesignData>();
		}

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
			hpFillImage.fillAmount = ship.CurrentHealth / ship.MaxHealth;
			actionPoints.Setup(ship.MaxActionPoints, ship.CurrentActionPoints);
			typeText.text = shipTypes.types[ship.ShipType].ToString();
			speedText.text = ship.ShipData.speed.ToString() + GetBuffString(ship.Buffs.speed);
			evadeText.text = ship.ShipData.evade.ToString() + GetBuffString(ship.Buffs.evade);
			armorText.text = ship.ShipData.armor.ToString() + GetBuffString(ship.Buffs.armor);
			flac.text = (ship.Flac * 100).ToString() + '%' + GetBuffString(ship.Buffs.flac * 100, "%");
		}

		public string GetBuffString(float buffValue, string suffix = "")
		{
			return (buffValue > 0 ? string.Format(" (+{0}{1})", buffValue.ToString("0"), suffix) : "");
		}

	}
}