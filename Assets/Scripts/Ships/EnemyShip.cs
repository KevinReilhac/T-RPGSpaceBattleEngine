using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Kebab.BattleEngine.Difficulty;
using Kebab.BattleEngine.Map;
using Kebab.Extentions.ListExtention;

using Kebab.BattleEngine.Ships.AI;
namespace Kebab.BattleEngine.Ships
{
	public class EnemyShip : Ship
	{
		[SerializeField] private ShipAIType aiType;

		private baseShipAI ai = null;

		public override ShipOwner Owner => ShipOwner.Enemy;

		UnityAction onEndPlay = null;

		protected override void Awake()
		{
			base.Awake();
			SetupAI();
		}

		private void SetupAI()
		{
			if (aiType.GetType(out Type type))
			{
				ai = gameObject.AddComponent(type) as baseShipAI;
				ai.SetupShip(this);
			}
			else
			{
				Debug.LogErrorFormat("{0} AI not found", aiType.typeName);
				DestroyIt();
			}
		}

		public void Play(UnityAction onEndPlay)
		{
			if (ai == null)
			{
				onEndPlay.Invoke();
				return;
			}

			this.onEndPlay = onEndPlay;
			StartCoroutine(__PlayCoroutine());
		}

		public override int MaxHealth
		{
			get => Mathf.RoundToInt(shipData.health * DifficultyManager.instance.CurrentDifficulty.enemyLifeMultiplicator);
		}

		private IEnumerator __PlayCoroutine()
		{
			for (int i = 0; i < MaxActionPoints; i++)
			{
				if (BattleManager.instance.GetShips(ShipOwner.Player).Count == 0)
				{
					onEndPlay.Invoke();
					yield break;
				}
				bool waitCondition = false;
				ai.Play(() => waitCondition = true);
				yield return new WaitUntil(() => waitCondition);
			}
			onEndPlay.Invoke();
		}

		private void Move(UnityAction action)
		{
			List<Cell> moveCells = GetMoveRangeCells();

			MoveTo(moveCells.GetRandom().GridPosition, action);
		}
	}
}