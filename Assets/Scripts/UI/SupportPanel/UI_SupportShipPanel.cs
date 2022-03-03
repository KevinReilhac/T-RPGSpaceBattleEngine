using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_SupportShipPanel : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform content = null;
	[SerializeField] private List<SO_Ship> shipList = null;
	[Header("Prefabs")]
	[SerializeField] private UI_SupportShipButton buttonPrefab = null;
	[SerializeField] private UI_DragAndDropShip dragAndDropShipPrefab = null;
	[SerializeField] private Ship shipPrefab = null;

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

		dragAndDropShip.Setup(shipData, OnShipDropped);
		BattleManager.instance.SelectShip(null);
	}

	private void OnShipDropped(Cell cell, SO_Ship shipData)
	{
		Ship ship = Instantiate(shipPrefab, cell.transform.position, Quaternion.identity);

		ship.SetupData(shipData);
		BattleManager.instance.AddShip(ship);
		ship.AlignToGrid();
		BattleManager.instance.SetShipSelection();
	}
}
