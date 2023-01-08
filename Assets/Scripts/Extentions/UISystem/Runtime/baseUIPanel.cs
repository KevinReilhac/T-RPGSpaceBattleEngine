/**
################################################################################
#          File: baseUIPanel.cs                                                #
#          File Created: Thursday, 26th May 2022 5:38:14 pm                    #
#          Author: Kévin Reilhac (kevin.reilhac.pro@gmail.com)                 #
################################################################################
**/

using UnityEngine;
using UnityEngine.Events;

namespace Kebab.UISystem
{
	/// <summary>
	/// base class for UI panels (managed by UIManager)
	/// </summary>
	public class baseUIPanel : MonoBehaviour
	{
		protected UnityEvent onShow = new UnityEvent();
		protected UnityEvent onHide = new UnityEvent();

		virtual public void Init()
		{
			//Empty implementation
		}

		virtual public void Show()
		{
			onShow.Invoke();
			gameObject.SetActive(true);
		}

		virtual public void Hide()
		{
			onHide.Invoke();
			gameObject.SetActive(false);
		}

		public UnityEvent OnOpen
		{
			get => onShow;
		}

		public UnityEvent OnClose
		{
			get => onHide;
		}
	}
}