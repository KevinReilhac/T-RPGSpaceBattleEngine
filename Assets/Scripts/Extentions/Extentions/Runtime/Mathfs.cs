/**
################################################################################
#          File: Mathfs.cs                                                     #
#          File Created: Monday, 23rd May 2022 3:19:27 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.Extentions
{
	public static class Mathfs
	{
		public static float Remap(float oldMin, float oldMax, float newMin, float newMax, float value)
		{
			return (Mathf.Lerp(newMin, newMax, Mathf.InverseLerp(oldMin, oldMax, value)));
		}
	}
}
