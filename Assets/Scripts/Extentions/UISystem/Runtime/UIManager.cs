/**
################################################################################
#          File: UIManager.cs                                                  #
#          File Created: Thursday, 26th May 2022 5:38:47 pm                    #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using System;
using UnityEngine;
using Kebab.Managers;
using Kebab.UISystem.Utils;

namespace Kebab.UISystem
{
	/// <summary>
	/// Manager from you can get all UI panel in the scene
	/// </summary>
	/// <typeparam name="UIManager"></typeparam>
	public class UIManager : Manager<UIManager>
	{
		private TypeDict<baseUIPanel> panels = new TypeDict<baseUIPanel>();

	
		protected override void xAwake()
		{
			baseUIPanel[] panelArray = GameObject.FindObjectsOfType<baseUIPanel>(true);

			for (int i = 0; i < panelArray.Length; i++)
			{
				panelArray[i].Init();
				AddPanel(panelArray[i]);
			}
		}

		public T GetPanel<T>() where T : baseUIPanel
		{
			if (panels.Dict.ContainsKey(typeof(T)))
				return (panels.Get<T>());

			baseUIPanel uiPanel = GameObject.FindObjectOfType<T>(true);
			if (uiPanel != null)
			{
				AddPanel(uiPanel);
				return (uiPanel as T);
			}
			Debug.LogError(typeof(T).ToString() + " not found.");
			return (null);
		}

		public T ShowPanel<T>() where T : baseUIPanel
		{
			T panel = GetPanel<T>();

			panel.Show();
			return (panel);
		}

		public T HidePanel<T>() where T : baseUIPanel
		{
			T panel = GetPanel<T>();

			panel.Hide();
			return (panel);
		}

		private void AddPanel(baseUIPanel panel)
		{
			panels.Add(panel);
		}
	}
}
