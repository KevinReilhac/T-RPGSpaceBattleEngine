/**
################################################################################
#          File: AutoInstanceManagerInstanciator.cs                            #
#          File Created: Sunday, 22nd May 2022 8:16:13 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.Managers.Utils
{
	/// <summary>
	/// Instanciate all AutoInstanceManagers
	/// </summary>
	public class AutoInstanceManagerInstanciator : ScriptableObject
	{
		[RuntimeInitializeOnLoadMethod]
		static public void OnRuntimeMethodLoad()
		{
			List<Type> autoInstances = FindAllDerivedTypes(typeof(AutoInstanceManager<>));

			foreach (Type type in autoInstances)
				new GameObject(type.Name + (" (MANAGER)"), type);
		}

		public static List<Type> FindAllDerivedTypes(Type type)
		{
			return GetDerivedTypes(type, Assembly.GetExecutingAssembly());
		}

		public static List<Type> GetDerivedTypes(Type baseType, Assembly assembly)
		{
			// Get all types from the given assembly
			Type[] types = assembly.GetTypes();
			List<Type> derivedTypes = new List<Type>();

			for (int i = 0, count = types.Length; i < count; i++)
			{
				Type type = types[i];
				if (IsSubclassOf(type, baseType))
				{
					// The current type is derived from the base type,
					// so add it to the list
					derivedTypes.Add(type);
				}
			}

			return derivedTypes;
		}

		public static bool IsSubclassOf(Type type, Type baseType)
		{
			if (type == null || baseType == null || type == baseType)
				return false;

			if (baseType.IsGenericType == false)
			{
				if (type.IsGenericType == false)
					return type.IsSubclassOf(baseType);
			}
			else
			{
				baseType = baseType.GetGenericTypeDefinition();
			}

			type = type.BaseType;
			Type objectType = typeof(object);

			while (type != objectType && type != null)
			{
				Type curentType = type.IsGenericType ?
					type.GetGenericTypeDefinition() : type;
				if (curentType == baseType)
					return true;

				type = type.BaseType;
			}

			return false;
		}
	}
}