using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Map;

namespace Kebab.BattleEngine.UI
{

	public class UI_DragAndDropShip : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer spriteRenderer = null;
		private SO_Ship shipData = null;
		private UnityAction<Cell, SO_Ship> callback = null;
		private CellCollection playerFieldCells = null;

		private Cell currentCell = null;

		public void Setup(SO_Ship shipData, UnityAction<Cell, SO_Ship> callback, CellCollection playerFieldCells)
		{
			spriteRenderer.sprite = shipData.sprite;
			this.shipData = shipData;
			this.callback = callback;
			this.playerFieldCells = playerFieldCells;
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
			currentCell = playerFieldCells.GetNearestCell(mouseWorldPos, false);

			transform.position = currentCell.transform.position;
		}

		private void SpawnShip()
		{
			callback.Invoke(currentCell, shipData);
			Destroy(gameObject);
		}

	}
}