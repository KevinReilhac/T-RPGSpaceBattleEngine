using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using Shapes2D;

using Kebab.DesignData;
using Kebab.Extentions;

namespace Kebab.BattleEngine.Map
{
	public enum GridType
	{
		HEX,
		SQUARE
	}

	[ExecuteAlways]
	public abstract class baseGrid : MonoBehaviour
	{
		private const string CELLS_PARENT_NAME = "Cells";

		[Header("Map Options")]
		[SerializeField] [OnValueChanged("GenerateMap")] private Vector2Int mapSize = new Vector2Int(30, 15);
		[SerializeField] [OnValueChanged("OnCellsOutlineSizeChanged")] private float cellsOutlineSize = 0.02f;
		[SerializeField] [OnValueChanged("UpdateBounds")] private Vector2 boundsOffset = new Vector2();
		[Header("Size")]
		[SerializeField] [OnValueChanged("OnCellsSizeChanged")] protected float cellSize = 1f;
		[Header("Prefabs")]
		[SerializeField] private Cell cellPrefab = null;
		[Header("Gizmos")]
		[SerializeField] private bool drawGizmos = true;
		[SerializeField] private Color boundsGizmosColor = Color.red;

		protected Dictionary<Vector2Int, Cell> mapMatrix = new Dictionary<Vector2Int, Cell>();
		protected Dictionary<Vector2Int, Vector2> worldPosByGridPos = new Dictionary<Vector2Int, Vector2>();
		private Cell hoveredCell = null;
		private Transform cellsParent = null;
		private Bounds mapBounds = new Bounds();
		private UnityEvent<Vector2Int> onCellSelected = new UnityEvent<Vector2Int>();
		private CellColorsDesignData cellColorsDesignData = null;

		#region Init
		private void OnEnable()
		{
			Awake();
		}

		virtual protected void Awake()
		{
			cellColorsDesignData = DesignData.DesignDataManager.Get<CellColorsDesignData>();
			GetExistingCells();
			UpdateCellsPosition();
			UpdateBounds();
		}

		[Button("ClearCells")]
		private void ClearCells()
		{
			GetCellsParent().ClearChilds();
		}

		private void OnCellsOutlineSizeChanged()
		{
			GetAllCells().ForEach((c) => c.Shape.settings.outlineSize = cellsOutlineSize);
		}

		private void GetExistingCells()
		{
			Queue<Cell> cellsQueu = new Queue<Cell>(GetCellsParent().GetComponentsInChildren<Cell>());
			mapMatrix.Clear();

			for (int x = 0; x < mapSize.x; x++)
			{
				for (int y = 0; y < mapSize.y; y++)
				{
					Cell cell = cellsQueu.Dequeue();

					cell.GridPosition = new Vector2Int(x, y);
					cell.Map = this;
					if (mapMatrix.ContainsKey(new Vector2Int(x, y)))
						mapMatrix[new Vector2Int(x, y)] = cell;
					else
						mapMatrix.Add(new Vector2Int(x, y), cell);
				}
			}
		}

		private void OnCellsSizeChanged()
		{
			GetAllCells().ForEach((c) => c.transform.localScale = Vector3.one * cellSize);

			UpdateCellsPosition();
			UpdateBounds();
		}

		private Cell CreateNewCell()
		{
			Cell cell = Instantiate(cellPrefab, GetCellsParent());

			cell.Shape.settings.outlineSize = cellsOutlineSize;
			return (cell);
		}

		private Transform GetCellsParent()
		{
			if (cellsParent != null)
				return cellsParent;

			cellsParent = transform.Find(CELLS_PARENT_NAME);
			if (cellsParent != null)
			{
				return (cellsParent);
			}

			cellsParent = new GameObject(CELLS_PARENT_NAME).transform;
			cellsParent.parent = transform;
			cellsParent.localPosition = Vector3.zero;
			cellsParent.localScale = Vector3.one;

			return (cellsParent);
		}

		[Button("Generate")]
		public void GenerateMap()
		{
			ClearCells();

			for (int x = 0; x < mapSize.x; x++)
			{
				for (int y = 0; y < mapSize.y; y++)
				{
					Cell cell = CreateNewCell();

					cell.GridPosition = new Vector2Int(x, y);
					mapMatrix[new Vector2Int(x, y)] = cell;
					cell.Shape.settings.outlineSize = cellsOutlineSize;
					cell.transform.localScale = Vector3.one * cellSize;
					cell.Map = this;
				}
			}

			UpdateCellsPosition();
		}

		private void UpdateCellsPosition()
		{
			for (int x = 0; x < mapSize.x; x++)
			{
				for (int y = 0; y < mapSize.y; y++)
				{
					Cell cell = GetCell(x, y);

					if (cell == null)
						return;
					cell.transform.localPosition = GetCellLocalPosFromGridPos(x, y);
					cell.gameObject.name = string.Format("{0} ({1}, {2})", cellPrefab.name, x, y);

					if (!worldPosByGridPos.ContainsKey(cell.GridPosition))
						worldPosByGridPos.Add(cell.GridPosition, cell.transform.position);
					else
						worldPosByGridPos[cell.GridPosition] = cell.transform.position;
				}
			}
		}

		protected abstract Vector2 GetCellLocalPosFromGridPos(int x, int y);

		#endregion

		#region Getters

		public Cell GetHoveredCell()
		{
			return (hoveredCell);
		}

		public CellCollection GetAllCells()
		{
			return (new CellCollection(mapMatrix.Values.ToList()));
		}

		public Cell GetCell(Vector2Int position)
		{
			if (mapMatrix.ContainsKey(position))
				return (mapMatrix[position]);

			Debug.LogError("Out of bounds");
			return (null);
		}

		abstract public CellCollection GetCellRange(Vector2Int center, int range, bool getFullCells = true);
		abstract public CellCollection GetCellLine(Vector2Int start, Vector2Int end, bool blocking = true);
		abstract public GridType GridType {get;}

		public Vector3 GetWorldPosition(Vector2Int gridPosition)
		{
			return (worldPosByGridPos[gridPosition]);
		}

		private Cell GetCell(int x, int y)
		{
			return (GetCell(new Vector2Int(x, y)));
		}

		public Cell GetNearestCell(Vector2 position, bool getFullCells = true)
		{
			List<Vector2> cellsWorldPos = worldPosByGridPos.Values.ToList();

			float nearestDist = Mathf.Infinity;
			Cell nearestCell = null;

			for (int i = 0; i < cellsWorldPos.Count; i++)
			{
				float dist = Vector2.Distance(position, cellsWorldPos[i]);

				if (getFullCells == false && mapMatrix.Values.ElementAt(i).PlacedObject != null)
					continue;

				if (dist < nearestDist)
				{
					nearestDist = dist;
					nearestCell = mapMatrix.Values.ElementAt(i);
				}
			}

			return (nearestCell);
		}

		public UnityEvent<Vector2Int> OnCellSelected
		{
			get => onCellSelected;
		}

		public Vector2Int Size
		{
			get => mapSize;
		}

		#endregion

		#region Bounds

		public void UpdateBounds()
		{
			CellCollection cells = GetAllCells();
			mapBounds = new Bounds(GetCellsParent().transform.position, Vector3.zero);

			foreach (Cell cell in cells)
				mapBounds.Encapsulate(cell.Collider.bounds);
			mapBounds.size += (Vector3)boundsOffset;
		}

		public Bounds Bounds
		{
			get
			{
				UpdateBounds();
				return (mapBounds);
			}
		}
		#endregion

		#region Helpers
		public void ResetAllCells()
		{
			GetAllCells().ForEach((c) =>
			{
				c.SetInteractable(false);
				c.ResetInsideColor();
			});
		}

		#endregion

		#region CellsInteraction
		public void SetHoveredCell(Cell cell)
		{
			if (hoveredCell)
				hoveredCell.SetSelectableOutlineColor();
			hoveredCell = cell;
			hoveredCell.SetHoveredColor();
		}

		public void CellClicked(Cell cell)
		{
			if (cell != hoveredCell)
				return;
			onCellSelected.Invoke(cell.GridPosition);
		}
		#endregion

		#region Gizmos
		private void OnDrawGizmosSelected()
		{
			if (Application.isPlaying && !drawGizmos)
				return;

			Gizmos.color = boundsGizmosColor;
			Gizmos.DrawWireCube(mapBounds.center, mapBounds.size);
		}
		#endregion
	}
}