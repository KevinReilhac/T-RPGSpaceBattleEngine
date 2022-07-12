using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace Kebab.BattleEngine.Ships.UI
{
	public class UI_ActionPointBullet : MonoBehaviour
	{
		[SerializeField] [OnValueChanged("UpdateBullet")] private bool isFull = true;
		[SerializeField] private Image fillImage = null;
		
		public bool IsFull
		{
			get => isFull;
			set
			{
				isFull = value;
				UpdateBullet();
			}
		}

		private void UpdateBullet()
		{
			fillImage.enabled = isFull;
		}
	}
}