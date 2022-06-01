using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.Utils
{
	public static class ReflectionUtils
	{
		public static List<Type> GetAllSubtypes(Type baseType)
		{
			List<Type> types = new List<Type>();

			foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (var type in asm.GetTypes())
				{
					if (type.BaseType == baseType)
						types.Add(type);
				}
			}

			return (types);
		}
	}
}