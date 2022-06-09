using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kebab.Managers;

namespace Kebab.BattleEngine.MoneySystem
{
	public class MoneyManager : Manager<MoneyManager>
	{
		private int currentMoney = 0;
		private UnityEvent<int> onPriceChange = new UnityEvent<int>();

		public bool Pay(int value)
		{
			if (value > currentMoney)
				return (false);
			currentMoney -= value;
			onPriceChange.Invoke(currentMoney);
			return (true);
		}

		public void SetMoney(int money)
		{
			currentMoney = money;
			onPriceChange.Invoke(money);
		}

		public bool CanPay(int price) => (price <= currentMoney);

		public UnityEvent<int> OnPriceChange
		{
			get => onPriceChange;
		}

		public int Money
		{
			get => currentMoney;
		}
	}
}