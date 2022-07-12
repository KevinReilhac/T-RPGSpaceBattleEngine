using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ARange<T>
{
	public T min = default(T);
	public T max = default(T);

	public ARange(T min, T max)
	{
		this.min = min;
		this.max = max;
	}

	public abstract T GetRandom();
	public abstract T Clamp(T value);
	public abstract bool IsInRange(T value);

	public override string ToString()
	{
		return ($"({min}, {max})");
	}
}

[System.Serializable]
public class RangeInt : ARange<int>
{
	public RangeInt(int min, int max) : base(min, max) {}

	public override int GetRandom()
	{
		return Random.Range(min, max);
	}

	public override int Clamp(int value)
	{
		return (Mathf.Clamp(value, min, max));
	}

	public override bool IsInRange(int value)
	{
		return (value >= min && value <= max);
	}
}

[System.Serializable]
public class RangeFloat : ARange<float>
{
	public RangeFloat(float min, float max) : base(min, max) {}

	public override float GetRandom()
	{
		return (Random.Range(min, max));
	}

	public override float Clamp(float value)
	{
		return (Mathf.Clamp(value, min, max));
	}

	public override bool IsInRange(float value)
	{
		return (value >= min && value <= max);
	}
}