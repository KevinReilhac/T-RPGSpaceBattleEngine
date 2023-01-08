using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kebab.Managers;
using Kebab.UISystem;
using Kebab.BattleEngine.GamePhases;
using Kebab.BattleEngine.Ships;
using Kebab.BattleEngine.Ships.UI;
using Kebab.BattleEngine.Map;
using Kebab.BattleEngine.UI;
using Kebab.BattleEngine.MoneySystem;
using Kebab.BattleEngine.Conditions;
using Kebab.BattleEngine.Logs;

namespace Kebab.BattleEngine
{

    public enum GamePhaseEnum
    {
        NONE,
        PlaceUnits,
        PlayerTurn,
        EnemyTurn,
        EndBattle
    }

    public class BattleManager : Manager<BattleManager>
    {
        [SerializeField] private int defaultStartMoney = 5000;
        [SerializeField] private int playfieldSeparationDelta = 0;
        [SerializeField] private ConditionsHandler conditions = null;
        [SerializeField] private Transform shipsParent = null;

        private baseGrid mapManager = null;
        private List<Ship> ships = new List<Ship>();
        private PlayerShip selectedPlayerShip = null;
        private int currentTurn = 0;
        private GamePhaseEnum firstGamePhase = GamePhaseEnum.NONE;
        private bool isInitialized = false;
        private bool isBattleFinished = false;


        //Game phases
        private Dictionary<GamePhaseEnum, baseGamePhase> gamePhasesDict = null;
        private GamePhaseEnum currentGamePhase = GamePhaseEnum.NONE;

        //Events
        private UnityEvent<PlayerShip> onShipSelected = new UnityEvent<PlayerShip>();
        private UnityEvent onVictory = new UnityEvent();
        private UnityEvent onDefeat = new UnityEvent();
        private UnityEvent<GamePhaseEnum> onGameStateChanged = new UnityEvent<GamePhaseEnum>();
        private UnityEvent onShipListUpdate = new UnityEvent();

        public void Setup(int startMoney, List<SO_Ship> playerShipList, UnityAction victory = null, UnityAction defeat = null)
        {
            UIManager.instance.GetPanel<UI_SupportShipPanel>().InitShipList(playerShipList == null ? LoadAllShips() : playerShipList);
            MoneyManager.instance.SetMoney(startMoney);
            if (victory != null)
                onVictory.AddListener(victory);
            if (onDefeat != null)
                onDefeat.AddListener(defeat);
            UIManager.instance.Init();
            isInitialized = true;
        }

        private List<SO_Ship> LoadAllShips()
        {
            SO_Ship[] ships = Resources.LoadAll<SO_Ship>("Data/Ships");
            return ships.ToList();
        }

        private void Start()
        {
            CreateGamePhases();

            SetGamePhase(GamePhaseEnum.PlaceUnits);
            conditions.onWin.AddListener(Victory);
            conditions.onLose.AddListener(Defeat);
            if (!isInitialized)
                Setup(defaultStartMoney, LoadAllShips());
        }

        private void Update()
        {
            if (gamePhasesDict.ContainsKey(currentGamePhase))
                gamePhasesDict[currentGamePhase].Update();
        }

        #region GamePhases
        private void CreateGamePhases()
        {
            gamePhasesDict = new Dictionary<GamePhaseEnum, baseGamePhase>()
            {
                {GamePhaseEnum.PlaceUnits, new PlaceUnitsGamePhase()},
                {GamePhaseEnum.PlayerTurn, new PlayerTurnGamePhase()},
                {GamePhaseEnum.EnemyTurn, new EnemyTurnGamePhase()},
            };
        }

        public void SetGamePhase(GamePhaseEnum newGamePhase)
        {
			if (isBattleFinished)
				return;
            if (newGamePhase == currentGamePhase)
                return;

            if (firstGamePhase == GamePhaseEnum.NONE && (newGamePhase == GamePhaseEnum.PlayerTurn || newGamePhase == GamePhaseEnum.EnemyTurn))
            {
                conditions.Init();
                firstGamePhase = newGamePhase;
            }

            if (newGamePhase == firstGamePhase)
                currentTurn++;

            if (gamePhasesDict.ContainsKey(currentGamePhase))
                gamePhasesDict[currentGamePhase].Stop();
            if (gamePhasesDict.ContainsKey(newGamePhase))
                gamePhasesDict[newGamePhase].Begin();

            BattleEngineLogs.Log(LogVerbosity.Low, "Change gamephase ({0} => {1})", currentGamePhase, newGamePhase);
            currentGamePhase = newGamePhase;
            onGameStateChanged.Invoke(currentGamePhase);
        }
        #endregion

        #region EndBattle
        private void Defeat()
        {
            StartCoroutine(__DefeatCoroutine());
        }

        private IEnumerator __DefeatCoroutine()
        {
            UIManager.instance.ShowPanel<UI_DefeatPanel>();
            BattleEngineLogs.Log(LogVerbosity.Low, "DEFEAT");
            EndBattle();
            yield return new WaitForSeconds(5f);
            onDefeat.Invoke();
        }

        private void Victory()
        {
            StartCoroutine(__VictoryCoroutine());

        }

        private IEnumerator __VictoryCoroutine()
        {
            UIManager.instance.ShowPanel<UI_VictoryPanel>();
            BattleEngineLogs.Log(LogVerbosity.Low, "WIN");
            EndBattle();
            yield return new WaitForSeconds(5f);
            onVictory.Invoke();
        }

        private void EndBattle()
        {
            isBattleFinished = true;
            SetGamePhase(GamePhaseEnum.EndBattle);
            SetCanSelectPlayerShips(false);
            UnselectPlayerShip();
            conditions.Dispose();
        }
        #endregion

        #region ShipSelection

        public void SetCanSelectPlayerShips(bool canSelect)
        {
            List<Ship> ships = GetShips(ShipOwner.Player);

            foreach (Ship ship in ships)
            {
                ship.IsSelectable = canSelect;
                if (!canSelect)
                {
                    ship.Cell.OnSelected = null;
                }
                else
                {
                    ship.Cell.OnSelected = (c) => SelectPlayerShip(ship as PlayerShip);
                }
            }
        }

        public void SelectPlayerShip(PlayerShip ship)
        {
            UnselectPlayerShip();
            selectedPlayerShip = ship;
            selectedPlayerShip.Select();

        }

        public void UnselectPlayerShip()
        {
            if (selectedPlayerShip == null)
                return;
            selectedPlayerShip.Unselect();

        }

        #endregion

        #region ShipList
        public void RemoveShip(Ship ship)
        {
            ships.Remove(ship);
            onShipListUpdate.Invoke();
        }

        public void AddShip(Ship ship)
        {
            ships.Add(ship);
            onShipListUpdate.Invoke();
            ship.transform.parent = shipsParent;
        }
        #endregion

        #region Getters

        public CellCollection GetPlayerSideCells()
        {
            CellCollection cells = GridMap.GetAllCells();

            return new CellCollection(cells.Where((c) => c.GridPosition.x < BattleManager.instance.GridXSeparation).ToList());
        }

        public CellCollection GetSeparationLine()
        {
            Vector2Int start = new Vector2Int(BattleManager.instance.GridXSeparation, 0);
            Vector2Int end = new Vector2Int(BattleManager.instance.GridXSeparation, BattleManager.instance.GridMap.Size.y - 1);

            return GridMap.GetCellLine(start, end, false);
        }

        public List<Ship> GetShips(ShipOwner shipsOwner = ShipOwner.All)
        {
            List<Ship> goodOwnerShips = new List<Ship>();

            foreach (Ship ship in ships)
            {
                if (shipsOwner.HasFlag(ship.Owner))
                    goodOwnerShips.Add(ship);
            }

            return (goodOwnerShips);
        }

        public List<T> GetShips<T>(ShipOwner shipOwner = ShipOwner.All) where T : Ship
        {
            List<Ship> ships = GetShips(shipOwner);
            List<T> typedShips = ships.Select((s) => s as T).ToList();

            return (typedShips);
        }

        public int CurrentTurn
        {
            get => currentTurn;
        }

        public UnityEvent OnShipListUpdate
        {
            get => onShipListUpdate;
        }

        public UnityEvent OnVictory
        {
            get => onVictory;
        }

        public UnityEvent OnDefeat
        {
            get => onDefeat;
        }

        public UnityEvent<GamePhaseEnum> OnGameStateChanged
        {
            get => onGameStateChanged;
        }

        public UnityEvent<PlayerShip> OnShipSelected
        {
            get => onShipSelected;
        }

        public baseGrid GridMap
        {
            get
            {
                if (mapManager == null)
                    mapManager = GameObject.FindObjectOfType<baseGrid>();
                return mapManager;
            }
        }

        public PlayerShip SelectedPlayerShip
        {
            get => selectedPlayerShip;
        }

        public GamePhaseEnum GameState
        {
            get => currentGamePhase;
            set => SetGamePhase(value);
        }

        public int GridXSeparation
        {
            get => Mathf.Clamp((GridMap.Size.x / 2) + playfieldSeparationDelta, 0, GridMap.Size.x - 1);
        }
        #endregion

        #region Gizmos
        private void OnDrawGizmos()
        {
            DrawFieldSeparationGizmo();
        }

        private void DrawFieldSeparationGizmo()
        {
            if (enabled)
                return;
            Vector2Int gridMiddle = new Vector2Int(GridMap.Size.x / 2, GridMap.Size.y / 2);

            gridMiddle.x += playfieldSeparationDelta;
            gridMiddle.x = Mathf.Clamp(gridMiddle.x, 0, GridMap.Size.x - 1);
            Vector3 gridMiddleWorld = GridMap.GetWorldPosition(gridMiddle);

            Gizmos.color = Color.green;
            Vector3 upPoint = new Vector3(gridMiddleWorld.x, GridMap.Bounds.center.y + GridMap.Bounds.extents.y);
            Vector3 downPoint = new Vector3(gridMiddleWorld.x, GridMap.Bounds.center.y - GridMap.Bounds.extents.y);

            Gizmos.DrawLine(upPoint, downPoint);
        }
        #endregion
    }
}