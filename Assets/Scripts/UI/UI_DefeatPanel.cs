using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using Kebab.UISystem;

namespace Kebab.BattleEngine.UI
{
	public class UI_DefeatPanel : baseUIPanel
	{
		private RectTransform rectTransform = null;
		private Vector2 defaultSizeDelta = Vector2.zero;

		public override void Init()
		{
			base.Init();
			rectTransform = GetComponent<RectTransform>();
			defaultSizeDelta = rectTransform.sizeDelta;
			gameObject.SetActive(false);
		}

		public override void Show()
		{
			base.Show();
			gameObject.SetActive(true);

			rectTransform.DOSizeDelta(defaultSizeDelta, 0.5f)
				.ChangeStartValue(new Vector2(defaultSizeDelta.x, 0f));
		}

		public override void Hide()
		{
			base.Hide();
			rectTransform.DOSizeDelta(new Vector2(defaultSizeDelta.x, 0f), 0.5f)
				.OnComplete(() => rectTransform.gameObject.SetActive(false));
		}
	}
}