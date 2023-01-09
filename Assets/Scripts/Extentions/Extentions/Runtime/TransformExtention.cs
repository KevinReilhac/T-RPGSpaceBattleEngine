/**
################################################################################
#          File: TransformExtention.cs                                         #
#          File Created: Monday, 23rd May 2022 3:09:31 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kebab.Extentions
{
	public static class TransformExtention
	{
		/// <summary>
		/// Destroy all childs of an object
		/// </summary>
		/// <param name="transform"></param>
		public static void ClearChilds(this Transform transform)
		{
			if (!Application.isPlaying)
				ClearChildsImmediate(transform);

			int childs = transform.childCount;

			for (int i = childs - 1; i >= 0; i--)
				GameObject.Destroy(transform.GetChild(i).gameObject);
		}

		public static void ClearChildsImmediate(this Transform transform)
		{
			int childs = transform.childCount;

			for (int i = childs - 1; i >= 0; i--)
				GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
		}

		public static void SetGraphicsAlpha(this Transform transform, float alpha)
		{
			List<Graphic> graphics = transform.GetComponentsInChildren<Graphic>().ToList();

			foreach (Graphic graphic in graphics)
			{
				Color color = graphic.color;
				color.a = alpha;

				graphic.color = color;
			}
		}
	}
}