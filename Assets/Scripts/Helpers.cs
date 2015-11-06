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

	// Returns current room of some object
	public static Room GetCurretntRoomOf (GameObject sObject)
	{
		Room result = null;
		if (sObject.GetComponent<Movement> ())
			result = sObject.GetComponent<Movement> ().CurrentRoom;
		else if (sObject.GetComponent<Furniture> ())
			result = sObject.GetComponent<Furniture> ().currentRoom; 
		return result;
	}
}
