using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Sprite))]
public class AttackVisualProjectile : AttackVisual
{
	[SerializeField] private float moveSpeed = 1;
	[SerializeField] private Ease easeCurve = Ease.Linear;

	protected override void Animation()
	{
		Vector3 worldFrom = BattleManager.instance.GridMap.GetWorldPosition(from);
		Vector3 worldTo = BattleManager.instance.GridMap.GetWorldPosition(to);
		float distance = Vector3.Distance(worldFrom, worldTo);

		transform.right = worldTo - worldFrom;
		transform.DOMove(worldTo, distance / moveSpeed)
			.ChangeStartValue(worldFrom)
			.SetEase(easeCurve)
			.OnComplete(() => {
				onHit.Invoke();
				Destroy(gameObject);
			});
	}
}
