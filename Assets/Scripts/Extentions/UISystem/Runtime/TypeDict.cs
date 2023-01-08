/**
################################################################################
#          File: TypeDict.cs                                                   #
#          File Created: Thursday, 26th May 2022 5:39:25 pm                    #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System;
using System.Collections.Generic;

namespace Kebab.UISystem.Utils
{
	/// <summary>
	/// Dictionary that take Type as Key
	/// and instance as Value
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TypeDict<T>
	{
		private Dictionary<Type, T> dict = null;

		public TypeDict()
		{
			dict = new Dictionary<Type, T>();
		}

		public TypeDict(T[] elements)
		{
			dict = new Dictionary<Type, T>();

			for (int i = 0; i < elements.Length; i++)
			{
				Add(elements[i]);
			}
		}

		public void Add(T element)
		{
			Type type = element.GetType();

			if (dict.ContainsKey(type))
			{
				dict[type] = element;
				return;
			}

			dict.Add(type, element);
		}

		public U Get<U>() where U : T
		{
			return ((U)dict[typeof(U)]);
		}

		public Dictionary<Type, T> Dict
		{
			get => dict;
		}
	}
}
