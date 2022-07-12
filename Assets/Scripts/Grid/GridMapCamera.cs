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
			transform.position = gridMap.Bounds.ClosestPoint(transform.position);
		}

		private void AutoCenterCam()
		{
			Bounds targetBounds = new Bounds();

			BattleManager.instance.GetShips().ForEach(s => targetBounds.Encapsulate(s.transform.position));

			float screenRatio = (float)Screen.width / (float)Screen.height;
			float targetRatio = targetBounds.size.x / targetBounds.size.y;

			if (screenRatio >= targetRatio)
			{
				Camera.main.orthographicSize = targetBounds.size.y / 2;
			}
			else
			{
				float differenceInSize = targetRatio / screenRatio;
				Camera.main.orthographicSize = targetBounds.size.y / 2 * differenceInSize;
			}

			transform.position = new Vector3(targetBounds.center.x, targetBounds.center.y, -1f);
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