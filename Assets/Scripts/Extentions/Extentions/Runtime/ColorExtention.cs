/**
################################################################################
#          File: ColorExtention.cs                                             #
#          File Created: Monday, 23rd May 2022 3:09:31 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.Extentions.ColorExtention
{
	public static class ColorExtention
	{
		public static Color SetFullAlpha(this ref Color color)
		{
			color.a = 1f;

			return (color);
		}

		public static Color SetNoAlpha(this ref Color color)
		{
			color.a = 0f;

			return (color);
		}

		public static Color SetAlpha(this ref Color color, float alpha)
		{
			color.a = alpha;
			return (color);
		}

		public static Color SetNegative(this ref Color color)
		{
			return (new Color(1 - color.r, 1 - color.g, 1 - color.b));
		}
	}
}
