using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using NaughtyAttributes;

using Kebab.BattleEngine.Ships;

namespace Kebab.BattleEngine.UI
{
	public class ShipFieldUI : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private Ship ship;
		[SerializeField] private Image healthBarFiller = null;
		[SerializeField] private TextMeshProUGUI hitText = null;
		[Header("Settings")]
		[SerializeField] private Color hitTextColor = Color.red;
		[SerializeField] private Color missTextColor = Color.white;
		[SerializeField] private string missText = "Miss";
		[SerializeField] private float animTime = 0.5f;
		[SerializeField] private float hitTextAnimYMoveOffset = 1f;
		[SerializeField] private Ease hitTextAnimYMoveEase = Ease.Linear;
		[SerializeField] private Ease hitTextFadeEase = Ease.Linear;

		private Vector3 startTextPosition = Vector3.zero;

		private void Awake()
		{
			startTextPosition = hitText.transform.localPosition;
			hitText.gameObject.SetActive(false);
			ship.OnHit.AddListener(OnHit);
		}

		private void OnHit(int value)
		{
			if (ship.CurrentHealth <= 0)
				return;
			if (value > 0)
				HealthBarAnimation();
			HitTextAnimation(value);
		}

		private void HealthBarAnimation()
		{
			healthBarFiller.fillAmount = ((float)ship.CurrentHealth / (float)ship.MaxHealth);
		}

		private void HitTextAnimation(int value)
		{
			bool isMiss = value == Ship.ON_MISS_DAMAGE_VALUE;
			string text = isMiss ? missText : (value * -1).ToString();
			Color color = isMiss ? missTextColor : hitTextColor;

			hitText.gameObject.SetActive(true);
			hitText.color = color;
			hitText.text = text;
			hitText.transform.localPosition = startTextPosition;
			hitText.transform.DOLocalMoveY(startTextPosition.y + hitTextAnimYMoveOffset, animTime)
				.SetEase(hitTextAnimYMoveEase);
			hitText.DOFade(0, animTime)
				.ChangeStartValue(color)
				.SetEase(hitTextFadeEase)
				.OnComplete(() => hitText.gameObject.SetActive(false));
		}
	}
}