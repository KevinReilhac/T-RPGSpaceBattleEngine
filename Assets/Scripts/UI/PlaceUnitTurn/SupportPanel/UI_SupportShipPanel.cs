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
		[SerializeField] private Transform shipsParent = null;
		[Header("Prefabs")]
		[SerializeField] private UI_SupportShipButton buttonPrefab = null;
		[SerializeField] private UI_DragAndDropShip dragAndDropShipPrefab = null;
		[SerializeField] private PlayerShip shipPrefab = null;
		
        public override void Init()
        {
            base.Init();
        }

		public void InitShipList(List<SO_Ship> playerShipList)
		{
			content.ClearChilds();

			foreach (SO_Ship ship in playerShipList)
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
			dragAndDropShip.Setup(shipData, OnShipDropped);
		}

		private void OnShipDropped(Cell cell, SO_Ship shipData)
		{
			Ship ship = Instantiate(shipPrefab, cell.transform.position, Quaternion.identity, shipsParent);

			MoneyManager.instance.Pay(shipData.price);
			BattleManager.instance.GridMap.SetHoveredCell(null);
			ship.SetupData(shipData);
			ship.AlignToGrid();
			ship.name = string.Format("{0} (1)", ship.ShipName, BattleManager.instance.GetShips(ShipOwner.Player).Count);
		}
	}
}