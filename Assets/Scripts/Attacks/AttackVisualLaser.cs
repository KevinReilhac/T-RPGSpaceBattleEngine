using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AttackVisualLaser : AttackVisual
{
	[Header("References")]
	[SerializeField] private LineRenderer lineRenderer = null;
	[Header("Settings")]
	[SerializeField] private float animationTime = 1f;
	[SerializeField, CurveRange(0f, 0f, 1f, 1f)] private AnimationCurve laserWidthCurve = null;

	private float startWidth = 0f;

	private void Awake()
	{
		startWidth = lineRenderer.startWidth;
	}

	protected override void Animation()
	{
		Vector3[] positions = new Vector3[] { BattleManager.instance.GridMap.GetWorldPosition(from), BattleManager.instance.GridMap.GetWorldPosition(to) };

		lineRenderer.SetPositions(positions);
		StartCoroutine(__AnimationCoroutine());
	}

	private IEnumerator __AnimationCoroutine()
	{
		float halfTime = animationTime / 2f;

		for (float t = 0; t < halfTime; t += Time.deltaTime)
		{
			SetLineWidth(startWidth * laserWidthCurve.Evaluate(t / halfTime));
			yield return null;
		}

		onHit.Invoke();

		for (float t = halfTime; t < animationTime; t += Time.deltaTime)
		{
			SetLineWidth(startWidth * laserWidthCurve.Evaluate(t / halfTime));
			yield return null;
		}
	}

	private void SetLineWidth(float width)
	{
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;
	}
}
