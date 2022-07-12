using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.MoneySystem;
using Kebab.UISystem;
namespace Kebab.BattleEngine.UI
{
	public class UI_SupportShipButton : MonoBehaviour
	{
		[SerializeField] private Image image = null;
		[SerializeField] private Text price = null;
		[SerializeField] private Button button = null;

		private SO_Ship target = null;
		private UnityEvent<SO_Ship> onSelected = new UnityEvent<SO_Ship>();

		private void Awake()
		{
			button.onClick.AddListener(OnButtonClicked);
			MoneyManager.instance.OnPriceChange.AddListener(OnMoneyChanged);
		}

		public void Setup(SO_Ship so_ship)
		{
			image.sprite = so_ship.sprite;
			price.text = so_ship.price.ToString();
			target = so_ship;
			OnMoneyChanged(MoneyManager.instance.Money);
		}

		private void OnMoneyChanged(int money)
		{
			if (target != null)
				button.interactable = money >= target.price;
		}

		private void OnButtonClicked()
		{
			if (MoneyManager.instance.CanPay(target.price))
				onSelected.Invoke(target);
		}

		public UnityEvent<SO_Ship> OnSelected
		{
			get => onSelected;
		}
	}
}