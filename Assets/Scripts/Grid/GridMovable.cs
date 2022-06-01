using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using NaughtyAttributes;

namespace Kebab.BattleEngine.Map
{
	public class GridMovable : GridPlacable
	{
		[SerializeField] private float moveAnimationSpeed = 1f;
		[SerializeField] private Ease moveAnimationEaseCurve = Ease.Linear;
		[Header("Debug")]
		[SerializeField] private Vector2Int debug_moveTarget = new Vector2Int();

		public void MoveTo(Vector2Int moveGridPosition, UnityAction onEndMove = null)
		{
			if (moveGridPosition == cell.GridPosition)
				return;
			Vector3 target = BattleManager.instance.GridMap.GetWorldPosition(moveGridPosition);
			float moveTime = Vector3.Distance(transform.position, target) / moveAnimationSpeed;

			if (target == null)
				return;

			SetCell(BattleManager.instance.GridMap.GetCell(moveGridPosition));
			transform.DOMove(target, moveTime).SetEase(moveAnimationEaseCurve).OnComplete(() =>
			{
				if (onEndMove != null)
					onEndMove.Invoke();
			});
		}

		public void MoveTo(Vector2 moveWorldPosition, UnityAction onEndMove = null)
		{
			Vector2 gridTarget = BattleManager.instance.GridMap.GetNearestCell(moveWorldPosition).GridPosition;

			MoveTo(gridTarget, onEndMove);
		}

		#region Debug
#if UNITY_EDITOR
		[Button("MoveTo")]
		public void debug_MoveTo()
		{
			MoveTo(debug_moveTarget);
		}

#endif
		#endregion
	}
}