/**
################################################################################
#          File: TextureExtention.cs                                           #
#          File Created: Monday, 23rd May 2022 3:21:02 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.Extentions.TextureExtention
{
	static class Texture2DExtentions
	{
		public static Sprite GetSprite(this Texture2D texture)
		{
			return (Sprite.Create(
				texture,
				new Rect(0f, 0f, texture.width, texture.height),
				new Vector2(0.5f, 0.5f)
			));
		}
	}
}