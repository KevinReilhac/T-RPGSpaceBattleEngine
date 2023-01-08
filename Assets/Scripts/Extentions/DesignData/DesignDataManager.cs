using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.Managers;


namespace Kebab.DesignData
{
	/// <summary>
	/// Singleton that store access to all design files
	/// </summary>
	public class DesignDataManager : AutoInstanceManager<DesignDataManager>
	{
		Dictionary<string, baseDesignData> m_objects;

		object[] m_objectsBuffer;
		/// <summary>
		/// This will retrieve target scriptable object data type in resources and load it in a dictionnary for later use
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Get<T>()
		{
			var l_TString = typeof(T).ToString();
			if (instance.m_objects != null && instance.m_objects.ContainsKey(l_TString))
				return (T)(object)instance.m_objects[l_TString];

			if (instance.m_objectsBuffer == null)
			{
				instance.m_objectsBuffer = Resources.LoadAll("", typeof(baseDesignData));
			}

			if (instance.m_objectsBuffer != null && instance.m_objectsBuffer.Length > 0)
			{
				foreach (var l_obj in instance.m_objectsBuffer)
				{
					if (l_obj.GetType() == typeof(T))
					{
						if (instance.m_objects == null)
							instance.m_objects = new Dictionary<string, baseDesignData>();

						instance.m_objects.Add(l_TString, (baseDesignData)l_obj);

						//TODO find again a way to detect when there is more than one
						//if (instance.m_objectsBuffer.Length > 1)
						//    Debug.LogError("Warning, more than one " + l_TString + " file was found");

						return (T)(object)l_obj;
					}
				}
			}

			Debug.LogError("Data file " + l_TString + " was not found");
			return default(T);
		}

		//Potential plan b could be editor method to autoload all design data refs

	}
}