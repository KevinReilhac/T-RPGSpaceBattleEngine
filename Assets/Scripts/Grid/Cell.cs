using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Shapes2D;

public class Cell : MonoBehaviour
{
	[SerializeField] private Color disabledOutlineColor = Color.gray;
	[SerializeField] private Color outlineColor = Color.white;
	[SerializeField] private Color hoveredOutlineColor = Color.blue;
	[SerializeField] private Color clickedColor = Color.green;
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

	private void Awake()
	{
		SetInteractable(false);
		SetBaseOutlineColor();
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
		shape.settings.outlineColor = hoveredOutlineColor;
	}

	public void UpdateOutlineColor()
	{
		if (isInteractable)
			SetBaseOutlineColor();
		else
			SetDisabledOutlineColor();
	}

	public void SetDisabledOutlineColor()
	{
		shape.settings.outlineColor = disabledOutlineColor;
	}

	public void SetBaseOutlineColor()
	{
		shape.settings.outlineColor = outlineColor;
	}

	public void SetClickedColor()
	{
		shape.settings.outlineColor = clickedColor;
	}

	public void SetInsideColor(Color color)
	{
		color.a = fillAlpha;
		Shape.settings.fillColor = color;
	}

	public void ResetInsideColor()
	{
		Shape.settings.fillColor = new Color();
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
		SetBaseOutlineColor();
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
			Debug.Log("Clicked on " + gridPosition + " cell");
		}
	}
#endregion
}
