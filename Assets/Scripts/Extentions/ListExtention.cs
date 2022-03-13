using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtention
{
	private static System.Random rng = new System.Random();

	public static List<T> Shuffle<T>(this List<T> list)
	{
		List<T> tmpList = new List<T>(list);

		int n = tmpList.Count;  

		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = tmpList[k];  
			tmpList[k] = tmpList[n];  
			tmpList[n] = value;  
		} 

		return (tmpList);
	}

	public static bool IsNullOrEmpty<T>(this List<T> list)
	{
		if (list == null || list.Count == 0)
			return (true);
		return (false);
	}

	public static T GetRandom<T>(this List<T> list)
	{
		if (list.Count == 0)
			return (default(T));
		if (list.Count == 1)
			return (list[0]);

		return (list[UnityEngine.Random.Range(0, list.Count)]);
	}
}
