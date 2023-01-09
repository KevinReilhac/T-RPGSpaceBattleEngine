/**
################################################################################
#          File: RectTransformExtention.cs                                     #
#          File Created: Monday, 23rd May 2022 3:09:31 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.Extentions.RectTransformExtention
{
	public static class RectTransformExtention
	{
		public static Vector3 GetTopPosition(this RectTransform rect)
		{
			return (new Vector3(rect.anchoredPosition.x, rect.offsetMax.y, 0f));
		}

		public static Vector3 GetBottomPosition(this RectTransform rect)
		{
			return (new Vector3(rect.anchoredPosition.x, rect.offsetMin.y, 0f));
		}
	}
}