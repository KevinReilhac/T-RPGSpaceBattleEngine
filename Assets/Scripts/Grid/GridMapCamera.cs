using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Kebab.BattleEngine.Extentions;
using Kebab.BattleEngine.Ships;
namespace Kebab.BattleEngine.Map
{
	public class GridMapCamera : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private Camera cameraComponent = null;
		[Header("Options")]
		[SerializeField] private float cameraSizeMin = 0.3f;
		[SerializeField] private float zoomSpeed = 0.5f;

		private float cameraSizeMax = 0f;
		private Vector3 mouseOrigin;
		private Vector3 mouseDifference;
		private bool isDrag = false;
		private baseGrid gridMap = null;

		private Bounds cameraMoveBounds;

		private void Start()
		{
			gridMap = BattleManager.instance.GridMap;
			cameraSizeMax = gridMap.Bounds.extents.y;
			UpdateCameraMoveBounds();
			AutoCenterCam();
		}

		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Space))
				AutoCenterCam();
			UpdateDrag();
			UpdateZoom();
		}

		private void UpdateZoom()
		{
			cameraComponent.orthographicSize += Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;
			cameraComponent.orthographicSize = Mathf.Clamp(cameraComponent.orthographicSize, cameraSizeMin, cameraSizeMax);
			UpdateCameraMoveBounds();
			SetCameraInMoveBounds();
		}

		private void SetCameraInMoveBounds()
		{
			transform.position = cameraMoveBounds.ClosestPoint(transform.position);
		}

		private void AutoCenterCam()
		{
			Bounds bounds = new Bounds();
			List<Ship> ships = BattleManager.instance.GetShips(ShipOwner.All);
			Vector3 center = Vector3.zero;

			foreach (Ship ship in ships)
			{
				center += ship.transform.position;
			}
			transform.position = center;
		}

		private void UpdateDrag()
		{
			if (Input.GetMouseButton(1))
			{
				mouseDifference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
				if (isDrag == false)
				{
					isDrag = true;
					mouseOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				}
			}
			else
			{
				isDrag = false;
			}
			if (isDrag == true)
			{
				transform.position = mouseOrigin - mouseDifference;
				SetCameraInMoveBounds();
			}
		}

		private void UpdateCameraMoveBounds()
		{
			cameraMoveBounds = new Bounds(
				gridMap.Bounds.center,
				gridMap.Bounds.size - cameraComponent.OrthographicBounds().size
			);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.blue;

			Gizmos.DrawWireCube(cameraMoveBounds.center, cameraMoveBounds.size);
		}
	}
}