using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UI_ActionsPanelButton : MonoBehaviour
{
	[SerializeField] private Image image = null;

	private SO_Attack attackData = null;

	public void Setup(SO_Attack attackData)
	{
		image.sprite = attackData.sprite;
		this.attackData = attackData;
	}
}
