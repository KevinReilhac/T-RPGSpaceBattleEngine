using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_DragAndDropShip : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer = null;
	private SO_Ship shipData = null;
	private UnityAction<Cell, SO_Ship> callback = null;

	private Cell currentCell = null;

	public void Setup(SO_Ship shipData, UnityAction<Cell, SO_Ship> callback)
	{
		spriteRenderer.sprite = shipData.sprite;
		this.shipData = shipData;
		this.callback = callback;
	}

	private void Update()
	{
		UpdatePosition();
		if (Input.GetMouseButtonDown(0))
			SpawnShip();
	}

	private void UpdatePosition()
	{
		Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currentCell = BattleManager.instance.GridMap.GetNearestCell(mouseWorldPos, false);

		transform.position = currentCell.transform.position;
	}

	private void SpawnShip()
	{
		callback.Invoke(currentCell, shipData);
		Destroy(gameObject);
	}

}
