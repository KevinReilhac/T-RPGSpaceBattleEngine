using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.UISystem
{
	public class baseUIPanelsContainer : baseUIPanel
	{
		List<baseUIPanel> subPanels = new List<baseUIPanel>();

		override public void Init()
		{
			subPanels = gameObject.GetComponentsInChildren<baseUIPanel>(true).ToList();
			subPanels.Remove(this);
		}

		override public void Show()
		{
			subPanels.ForEach((p) => p.Show());
		}

		override public void Hide()
		{
			subPanels.ForEach((p) => p.Hide());
		}
	}
}