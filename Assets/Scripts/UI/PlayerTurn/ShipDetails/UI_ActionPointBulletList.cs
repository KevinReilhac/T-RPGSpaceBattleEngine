using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.Extentions;

namespace Kebab.BattleEngine.Ships.UI
{
	public class UI_ActionPointBulletList : MonoBehaviour
	{
		[SerializeField] private UI_ActionPointBullet bulletPrefab = null;
		[SerializeField] private Transform bulletsParent = null;

		private List<UI_ActionPointBullet> bullets = new List<UI_ActionPointBullet>();

		private void Awake()
		{
			bulletsParent.ClearChilds();
		}

		public void Setup(int max, int current)
		{
			current = Mathf.Clamp(current, 0, max);

			while (bullets.Count < max)
				bullets.Add(Instantiate(bulletPrefab, bulletsParent));
			for (int i = 0; i < bullets.Count; i++)
				bullets[i].gameObject.SetActive(i < max);

			for (int i = 0; i < max; i++)
				bullets[i].IsFull = i < current;
		}
	}
}