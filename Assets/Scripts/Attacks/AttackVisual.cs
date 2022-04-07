using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackVisual : MonoBehaviour
{
	[SerializeField] protected UnityEvent onHit = new UnityEvent();
	protected Vector2Int from = Vector2Int.zero;
	protected Vector2Int to = Vector2Int.zero;

	public void Setup(Vector2Int from, Vector2Int to, UnityAction onHitCallback)
	{
		this.from = from;
		this.to = to;
		this.onHit.AddListener(onHitCallback);

		Animation();
	}

	protected virtual void Animation()
	{
		onHit.Invoke();
	}

}
