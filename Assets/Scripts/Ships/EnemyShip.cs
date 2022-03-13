using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShip : Ship
{
	public override ShipOwner Owner => ShipOwner.Enemy;

	UnityAction onEndPlay = null;

	public void Play(UnityAction onEndPlay)
	{
		this.onEndPlay = onEndPlay;
		StartCoroutine(__PlayCoroutine());
	}

	private IEnumerator __PlayCoroutine()
	{
		for (int i = 0; i < MaxActionPoints; i++)
		{
			bool moved = false;
			Move(() => moved = true);
			yield return new WaitUntil(() => moved);
		}
		onEndPlay.Invoke();
	}

	private void Move(UnityAction action)
	{
		List<Cell> moveCells = GetMoveRangeCells();

		MoveTo(moveCells.GetRandom().GridPosition, action);
	}
}
