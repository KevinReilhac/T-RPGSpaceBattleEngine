using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

using Kebab.Extentions.ListExtention;

namespace Kebab.BattleEngine.Utils
{
	public class RandomSprite : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private SpriteRenderer spriteRenderer = null;
		[Header("Settings")]
		[SerializeField] private List<Sprite> sprites = null;

		private void Awake()
		{
			Randomise();
		}

		[Button]
		public void Randomise()
		{
			spriteRenderer.sprite = sprites.GetRandom();
		}
	}
}