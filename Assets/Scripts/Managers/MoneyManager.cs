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
		private UnityEvent<int> onMoneyChanged = new UnityEvent<int>();

		public bool Pay(int value)
		{
			if (value > currentMoney)
				return (false);
			currentMoney -= value;
			onMoneyChanged.Invoke(currentMoney);
			return (true);
		}

		public void Refound(int value)
		{
			currentMoney += value;
			onMoneyChanged.Invoke(currentMoney);
		}

		public void SetMoney(int money)
		{
			currentMoney = money;
			onMoneyChanged.Invoke(money);
		}

		public bool CanPay(int price) => (price <= currentMoney);

		public UnityEvent<int> OnMoneyChanged
		{
			get => onMoneyChanged;
		}

		public int Money
		{
			get => currentMoney;
		}
	}
}