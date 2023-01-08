/**
################################################################################
#          File: PersistentManager.cs                                          #
#          File Created: Sunday, 22nd May 2022 7:03:39 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kebab.Managers
{
	/// <summary>
	/// Manager that not destroy at scene load
	/// </summary>
	/// <typeparam name="T">Singleton type</typeparam>
	public class PersistentManager<T> : Manager<T> where T : MonoBehaviour
	{
		protected override void Awake()
		{
			//Destory if this persistent manager already exist
			if (FindObjectsOfType<PersistentManager<T>>().Length > 1)
			{
				Destroy(gameObject);
				return;
			}
			
			DontDestroyOnLoad(gameObject);
			xAwake();
		}
	}
}