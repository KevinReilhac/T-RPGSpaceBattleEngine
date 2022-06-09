using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Kebab.BattleEngine.MoneySystem;

namespace Kebab.BattleEngine.UI
{
	public class UI_MoneyPanel : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI text = null;

		private void Awake()
		{
			MoneyManager.instance.OnPriceChange.AddListener(UpdateMoney);
		}

		private void UpdateMoney(int money)
		{
			text.text = money.ToString("0000");
		}
	}
}