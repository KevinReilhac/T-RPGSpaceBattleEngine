/**
################################################################################
#          File: AutoInstanceManager.cs                                        #
#          File Created: Sunday, 22nd May 2022 7:56:38 pm                      #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.Managers
{
	/// <summary>
	/// AutoInstanceManagers ar instanciated on app start by AutoInstanceManagerInstanciator
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AutoInstanceManager<T> : PersistentManager<T> where T : MonoBehaviour
	{
	}
}