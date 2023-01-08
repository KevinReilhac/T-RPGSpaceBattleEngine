using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Kebab.DesignData;
using Kebab.BattleEngine.Audio;
using Kebab.BattleEngine.Map;
using Kebab.BattleEngine.Attacks;
using Kebab.BattleEngine.Difficulty;
using Kebab.BattleEngine.Logs;
using Kebab.BattleEngine.Ships.AI;


[System.Flags]
public enum ShipOwner
{
	All = -1,
	None = 0,
	Player = 1,
	Enemy = 2
}

namespace Kebab.BattleEngine.Ships
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class Ship : GridMovable
	{
		public const int ON_MISS_DAMAGE_VALUE = -999999999;

		[Header("References")]
		[SerializeField] private UnityEvent onDestroy = null;
		[SerializeField] protected SO_Ship shipData = null;

		private SpriteRenderer spriteRenderer = null;
		private UnityEvent<int> onHit = new UnityEvent<int>();
		private ShipBuff buffs = new ShipBuff();

		protected int currentActionPoints = 2;
		protected int currentHealth = 0;

		virtual protected void Awake()
		{
			BattleManager.instance.AddShip(this);
			spriteRenderer = GetComponent<SpriteRenderer>();
			ResetStats();
		}

		public void ResetStats()
		{
			currentHealth = MaxHealth;
			currentActionPoints = MaxActionPoints;
		}

		public void UseActionPoint()
		{
			currentActionPoints--;
		}

		public void ResetActionPoints()
		{
			currentActionPoints = MaxActionPoints;
		}

		public void SetupData(SO_Ship shipData)
		{
			if (shipData == null)
				return;
			this.shipData = shipData;
			spriteRenderer.sprite = shipData.sprite;
		}

		public List<Cell> GetMoveRangeCells()
		{
			return BattleManager.instance.GridMap.GetCellRange(GridPosition, Speed, false);
		}

		public void ApplyDamages(int damages)
		{
			if (damages == ON_MISS_DAMAGE_VALUE)
			{
				BattleEngineLogs.Log(LogVerbosity.High, "{0} Evade attack", name);
				onHit.Invoke(ON_MISS_DAMAGE_VALUE);
				return;
			}
			currentHealth -= damages;
			onHit.Invoke(damages);
			BattleEngineLogs.Log(LogVerbosity.High, "{0} damages on", damages, name);

			if (currentHealth <= 0)
			{
				DestroyIt();
			}
		}

		public void Attack(SO_Attack attack, Ship target, UnityAction onAttackEnd)
		{
			int damages = GetDamages(attack, target);
			int precision = GetPrecision(attack, target);

			AudioManager.instance.PlaySFX(attack.onStartClip);

			BattleEngineLogs.Log(LogVerbosity.High, "{0} attack {1} with {2}", name, target.name, attack.name);
			if (attack.attackVisual != null)
			{
				AttackVisual visual = Instantiate(attack.attackVisual, transform.parent);

				visual.Setup(GridPosition, target.GridPosition, () =>
					{
						onAttackEnd?.Invoke();
						ApplyDamagesToTarget(attack.ignoreEvade, damages, precision, target);
						AudioManager.instance.PlaySFX(attack.onOnHitClip);
					}
				);
			}
			else
			{
				AudioManager.instance.PlaySFX(attack.onOnHitClip);
				ApplyDamagesToTarget(attack.ignoreEvade, damages, precision, target);
				onAttackEnd?.Invoke();
			}
		}

		public int GetDamages(SO_Attack attack, Ship target)
		{
			ShipTypesDesignData shipTypes = DesignDataManager.Get<ShipTypesDesignData>();

			float distance = Vector2Int.Distance(GridPosition, target.GridPosition);
			float damages = attack.damages.GetRandom();
			float typeDamagesMultiplicator = shipTypes.GetTypeDamageMultiplicator(ShipType, target.ShipType);

			damages *= typeDamagesMultiplicator;

			if (!attack.ignoreFlac)
				damages *= (1 - target.Flac);
			if (!attack.ignoreArmor)
				damages -= target.Armor;
			if (target.Owner == ShipOwner.Player)
				damages *= DifficultyManager.instance.CurrentDifficulty.enemyDamageMultiplicator;

			return (Mathf.RoundToInt(damages));
		}

		public int GetPrecision(SO_Attack attack, Ship target)
		{
			float distance = Vector2Int.Distance(GridPosition, target.GridPosition);
			float precision = attack.precision;

			if (distance >= attack.normalDistanceRange.max)
				precision *= 0.5f;
			else if (distance <= attack.normalDistanceRange.min)
				precision *= 1.5f;
			return (Mathf.RoundToInt(precision));
		}

		private void ApplyDamagesToTarget(bool ignoreEvade, int damages, int precision, Ship target)
		{
			if (ignoreEvade || precision > target.Evade)
			{
				target.ApplyDamages(Mathf.RoundToInt(damages));
			}
			else
				target.ApplyDamages(ON_MISS_DAMAGE_VALUE);
		}

		public void DestroyIt()
		{
			Destroy(gameObject);
			BattleManager.instance.RemoveShip(this);
			onDestroy.Invoke();
			BattleEngineLogs.Log(LogVerbosity.High, "{0} destroyed", name);
		}

		virtual public ShipOwner Owner => ShipOwner.None;

		#region Getter
		public int CurrentActionPoints
		{
			get => currentActionPoints;
		}

		public int MaxActionPoints
		{
			get => shipData.actionPoints;
		}

		public int CurrentHealth
		{
			get => currentHealth;
		}

		public virtual int MaxHealth
		{
			get => shipData.health;
		}

		public int Speed
		{
			get => shipData.speed + buffs.speed;
		}

		public int Armor
		{
			get => shipData.armor + buffs.armor;
		}

		public int Evade
		{
			get => shipData.evade + buffs.evade;
		}

		public float Flac
		{
			get => shipData.flac + buffs.flac;
		}

		public int ShipType
		{
			get => shipData.type;
		}

		public string ShipName
		{
			get => shipData.name;
		}

		public List<SO_Attack> Attacks
		{
			get => shipData.attacks;
		}

		public UnityEvent<int> OnHit
		{
			get => onHit;
		}

		public UnityEvent OnDestroy
		{
			get => onDestroy;
		}

		public SO_Ship ShipData
		{
			get => shipData;
		}

		public ShipBuff Buffs
		{
			get => buffs;
		}
		#endregion

	}
}