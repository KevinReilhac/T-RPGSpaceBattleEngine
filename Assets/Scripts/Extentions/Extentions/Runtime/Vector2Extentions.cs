using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.Extentions.Vector2Extention
{
	static class Vector2Extentions
	{
		/// <summary>
		/// Get a random value between x and y
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static float GetRandom(this Vector2 vector)
		{
			return (Random.Range(vector.x, vector.y));
		}

		/// <summary>
		/// Check if a number in inside x and y values
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsInside(this Vector2 vector, float value)
		{
			return (value >= vector.x && value <= vector.y);
		}
	}

	static class Vector2IExtentions
	{
		/// <summary>
		/// Get a random value between x and y
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static int GetRandom(this Vector2Int vector)
		{
			return (Random.Range(vector.x, vector.y));
		}

		/// <summary>
		/// Check if a number in inside x and y values
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsInside(this Vector2Int vector, float value, bool minExlusive = false, bool maxExclusive = true)
		{
			bool condition = true;

			if (minExlusive)
			{
				if (value > vector.x)
					condition = false;
			}
			else
			{
				if (value >= vector.x)
					condition = false;
			}

			if (maxExclusive)
			{
				if (value < vector.y)
					condition = false;
			}
			else
			{
				if (value <= vector.y)
					condition = false;
			}

			return condition;
		}
	}
}