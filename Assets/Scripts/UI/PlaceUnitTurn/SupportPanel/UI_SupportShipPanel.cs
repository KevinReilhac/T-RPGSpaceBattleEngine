using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kebab.UISystem;
using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Map;
using Kebab.BattleEngine.MoneySystem;
using Kebab.Extentions;

namespace Kebab.BattleEngine.UI
{
	public class UI_SupportShipPanel : baseUIPanel
	{
		[Header("References")]
		[SerializeField] private Transform content = null;
		[SerializeField] private List<SO_Ship> shipList = null;
		[SerializeField] private Transform shipsParent = null;
		[Header("Prefabs")]
		[SerializeField] private UI_SupportShipButton buttonPrefab = null;
		[SerializeField] private UI_DragAndDropShip dragAndDropShipPrefab = null;
		[SerializeField] private PlayerShip shipPrefab = null;

		private void Awake()
		{
			InitShipList();
		}

		private void InitShipList()
		{
			content.ClearChilds();

			foreach (SO_Ship ship in shipList)
				CreateShipButton(ship);
		}

		private void CreateShipButton(SO_Ship shipData)
		{
			UI_SupportShipButton shipButton = Instantiate(buttonPrefab, content);

			shipButton.Setup(shipData);
			shipButton.OnSelected.AddListener(SpawnDragAndDropObject);
		}

		private void SpawnDragAndDropObject(SO_Ship shipData)
		{
			UI_DragAndDropShip dragAndDropShip = Instantiate(dragAndDropShipPrefab);

			BattleManager.instance.UnselectPlayerShip();
			dragAndDropShip.Setup(shipData, OnShipDropped, GetPlayerFieldCells());
		}

		private CellCollection GetPlayerFieldCells()
		{
			CellCollection allCells = BattleManager.instance.GridMap.GetAllCells();
			CellCollection placableCells = new CellCollection(allCells.Where((p) => p.GridPosition.x < BattleManager.instance.GridXSeparation).ToList());

			return (placableCells);
		}

		private void OnShipDropped(Cell cell, SO_Ship shipData)
		{
			Ship ship = Instantiate(shipPrefab, cell.transform.position, Quaternion.identity, shipsParent);

			MoneyManager.instance.Pay(shipData.price);
			ship.SetupData(shipData);
			ship.AlignToGrid();
		}
	}
}