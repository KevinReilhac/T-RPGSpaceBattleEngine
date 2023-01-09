/**
################################################################################
#          File: StringExtention.cs                                            #
#          File Created: Monday, 23rd May 2022 3:09:31 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.Extentions.StringExtention
{
	public static class StringExtention
	{
		public static string ToBase64(this string str)
		{
			byte[] txtBytes = System.Text.Encoding.UTF8.GetBytes(str);
			return Convert.ToBase64String(txtBytes);
		}

		public static string RemoveFromLastChar(this string str, char delimiter)
		{
			int index = str.LastIndexOf(delimiter);

			if (index == -1)
				return (str);
			return (str.Substring(0, index));
		}

		public static bool IsNullOrEmpty(this string str)
		{
			return (str == null || str == "");
		}

		/// <summary>
		/// Get string without spaces or upper characters
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string Normalized(this string str)
		{
			return (str.ToLower().Trim().Replace(" ", ""));
		}

		public static bool NormalizedCompare(this string str1, string str2)
		{
			return (str1.Normalized() == str2.Normalized());
		}
	}
}