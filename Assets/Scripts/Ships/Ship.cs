using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Kebab.DesignData;
using Kebab.BattleEngine.Map;
using Kebab.BattleEngine.Attacks;
using Kebab.BattleEngine.Difficulty;
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

		public void SetupData(SO_Ship shipData)
		{
			if (shipData == null)
				return;
			this.shipData = shipData;
			spriteRenderer.sprite = shipData.sprite;
		}

		public List<Cell> GetMoveRangeCells()
		{
			return BattleManager.instance.GridMap.GetCellRange(GridPosition, shipData.speed, false);
		}

		public void ApplyDamages(int damages)
		{
			if (damages == ON_MISS_DAMAGE_VALUE)
			{
				onHit.Invoke(ON_MISS_DAMAGE_VALUE);
				return;
			}
			currentHealth -= damages;
			onHit.Invoke(damages);

			if (currentHealth <= 0)
			{
				Destroy(gameObject);
				onDestroy.Invoke();
			}
		}

		public void Attack(SO_Attack attack, Ship target, UnityAction onAttackEnd)
		{
			ShipTypesDesignData shipTypes = DesignDataManager.Get<ShipTypesDesignData>();
			float distance = Vector2Int.Distance(GridPosition, target.GridPosition);
			float precision = attack.precision;
			float damages = attack.damages.GetRandom();
			float typeDamagesMultiplicator = shipTypes.GetDamageMultiplicator(ShipType, target.ShipType);

			Debug.LogFormat("{0}-->{1}---->{2}", shipTypes.types[ShipType], shipTypes.types[target.ShipType], typeDamagesMultiplicator);
			damages *= typeDamagesMultiplicator;

			if (!attack.ignoreFlac)
				damages *= (1 - target.Flac);
			if (!attack.ignoreArmor)
				damages -= target.Armor;

			if (distance >= attack.normalDistanceRange.max)
				precision *= 0.5f;
			else if (distance <= attack.normalDistanceRange.min)
				precision *= 1.5f;

			if (attack.attackVisual != null)
			{
				AttackVisual visual = Instantiate(attack.attackVisual, transform.parent);

				visual.Setup(
					GridPosition,
					target.GridPosition,
					() =>
					{
						ApplyDamagesToTarget(
							attack.ignoreEvade,
							Mathf.RoundToInt(damages),
							Mathf.RoundToInt(precision),
							target
						);
						if (onAttackEnd != null)
							onAttackEnd.Invoke();
					}
				);
			}
			else
			{
				ApplyDamagesToTarget(attack.ignoreEvade, Mathf.RoundToInt(damages), Mathf.RoundToInt(precision), target);
				if (onAttackEnd != null)
					onAttackEnd.Invoke();
			}
		}

		private void ApplyDamagesToTarget(bool ignoreEvade, int damages, int precision, Ship target)
		{
			Debug.Log("Precision : " + precision + " Evade : " + target.Evade);

			if (target.Owner == ShipOwner.Player)
				damages = Mathf.RoundToInt(damages * DifficultyManager.instance.CurrentDifficulty.enemyDamageMultiplicator);

			if (ignoreEvade || precision > target.Evade)
				target.ApplyDamages(Mathf.RoundToInt(damages));
			else
				target.ApplyDamages(ON_MISS_DAMAGE_VALUE);
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
			get => shipData.speed;
		}

		public int Armor
		{
			get => shipData.armor;
		}

		public int Evade
		{
			get => shipData.evade;
		}

		public float Flac
		{
			get => shipData.flac;
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

		public SO_Ship ShipData
		{
			get => shipData;
		}
		#endregion

	}
}