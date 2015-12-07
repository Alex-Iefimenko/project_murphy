using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Helpers : MonoBehaviour {

	// Returns random array table
	public static T GetRandomArrayValue<T> (List<T> array)
	{
		T value = array[Random.Range (0, array.Count)];
		return value;
	}

	public static T GetRandomArrayValue<T> (T[] array)
	{
		T value = array[Random.Range (0, array.Length)];
		return value;
	}

}
