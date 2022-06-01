using System.Reflection;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.BattleEngine.Utils;

namespace Kebab.BattleEngine.Ships.AI
{
	[System.Serializable]
	public class ShipAIType
	{
		public string typeName = "< NONE >";

		public bool GetType(out Type type)
		{
			List<Type> types = ReflectionUtils.GetAllSubtypes(typeof(baseShipAI));
			List<string> names = types.Select((t) => t.Name).ToList();

			if (!names.Contains(typeName))
			{
				type = default(Type);
				return (false);
			}

			type = types.Find((t) => t.Name == typeName);
			return (true);
		}
	}
}
