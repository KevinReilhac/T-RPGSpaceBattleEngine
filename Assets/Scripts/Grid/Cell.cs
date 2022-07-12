using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Shapes2D;

using Kebab.DesignData;

namespace Kebab.BattleEngine.Map
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private Color interactableInsideDefaultColor = Color.blue;
		[SerializeField] private float fillAlpha = 0.3f;

		[SerializeField] private Shape shape = null;
		[SerializeField] private Collider2D shapeCollider = null;

		private baseGrid map = null;
		private bool isClicked = false;
		private Vector2Int gridPosition = Vector2Int.zero;
		private bool isInteractable = false;
		private UnityAction<Cell> onSelected = null;
		private GridPlacable placedObject = null;
		private CellColorsDesignData cellColorsDesignData = null;

		private void Awake()
		{
			SetDisabledOutlineColor();
			cellColorsDesignData = DesignDataManager.Get<CellColorsDesignData>();
			shapeCollider = GetComponent<Collider2D>();
		}

		#region Getter
		public Shape Shape
		{
			get => shape;
		}

		public Collider2D Collider
		{
			get => shapeCollider;
		}

		public baseGrid Map
		{
			set => map = value;
			get => map;
		}

		public Vector2 Size
		{
			get => transform.lossyScale;
		}

		public Vector2 HalfSize
		{
			get => Size / 2;
		}

		public Vector2Int GridPosition
		{
			get => gridPosition;
			set => gridPosition = value;
		}

		public bool IsInteractable
		{
			get => isInteractable;
		}

		public UnityAction<Cell> OnSelected
		{
			set => onSelected = value;
		}

		public GridPlacable PlacedObject
		{
			get => placedObject;
			set => placedObject = value;
		}
		#endregion

		#region Setters
		public void SetHoveredColor()
		{
			shape.settings.outlineColor = CellColorsDesignData.hoverOutlineColor;
		}

		public void UpdateOutlineColor()
		{
			if (isInteractable)
				SetSelectableOutlineColor();
			else
				SetDisabledOutlineColor();
		}

		public void SetDisabledOutlineColor()
		{
			shape.settings.outlineColor = CellColorsDesignData.defaultOutlineColor;
		}

		public void SetSelectableOutlineColor()
		{
			shape.settings.outlineColor = CellColorsDesignData.selectableOutlineColor;
		}

		public void SetClickedColor()
		{
			shape.settings.outlineColor = CellColorsDesignData.clickedOutlineColor;
		}

		public void SetInsideColor(Color color)
		{
			color.a = fillAlpha;
			Shape.settings.fillColor = color;
		}

		public void ResetInsideColor()
		{
			Shape.settings.fillColor = Color.clear;
		}

		public void SetInteractable(bool interactable)
		{
			isInteractable = interactable;
			Collider.enabled = interactable;

			UpdateOutlineColor();
		}
		#endregion

		#region Events
		private void OnMouseOver()
		{
			if (EventSystem.current.IsPointerOverGameObject())
				OnMouseExit();
			else if (isInteractable)
				Map.SetHoveredCell(this);
		}

		private void OnMouseExit()
		{
			isClicked = false;
			UpdateOutlineColor();
		}

		private void OnMouseDown()
		{
			if (!isInteractable || EventSystem.current.IsPointerOverGameObject())
				return;
			isClicked = true;
			SetClickedColor();
			if (onSelected != null)
				onSelected.Invoke(this);
		}

		private void OnMouseUp()
		{
			if (!isInteractable)
				return;
			if (isClicked)
			{
				map.CellClicked(this);
				SetHoveredColor();
			}
		}
		#endregion

		private CellColorsDesignData CellColorsDesignData
		{
			get
			{
				if (cellColorsDesignData == null)
					cellColorsDesignData = DesignDataManager.Get<CellColorsDesignData>();
				return (cellColorsDesignData);
			}
		}
	}
}