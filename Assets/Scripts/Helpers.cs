using UnityEngine;
using System.Collections;

public class Helpers : MonoBehaviour {

	// Returns door object connecting two rooms
	// Two loops : if one door match other
	public static Door GetDoorBetweenRooms (Room roomOne, Room roomTwo) 
	{
		foreach (Door roomOneDoor in roomOne.doors)
		{
			foreach (Door roomTwoDoor in roomTwo.doors)
			{
				if (roomOneDoor == roomTwoDoor) return roomOneDoor;
			}
		}
		return null;
	}

	// Get closest point to third point
	public static Vector3 GetClosestPointTo (Vector3 comparePoint, Vector3 point1, Vector3 point2)
	{
		float distance1 = Vector3.Distance(comparePoint, point1);
		float distance2 = Vector3.Distance(comparePoint, point2);
		if (distance1 < distance2)
		{
			return point1;
		}
		else
		{
			return point2;
		}
	}

	// Displays current ship Graph as hash table
	public static void DebugShipGrahp()
	{
		// Debug
		foreach (Room key in ShipState.shipGraph.Keys)
		{
			string s = key.transform.position.ToString() + ": [ ";
			foreach (ArrayList neighbor in (ArrayList)ShipState.shipGraph[key])
			{
				Room cR = (Room)neighbor[0];
				float d = (float)neighbor[1];
				Door dr = (Door)neighbor[2];
				s += "[ " + cR.transform.position.ToString() + ", " + d.ToString() + ", " + dr.transform.position.ToString() + " ]";
			}
			s += " ]";
			print (s);
		}
	}

	// Returns random array table
	public static T GetRandomArrayValue<T> (ArrayList array)
	{
		T value = (T)array[Random.Range (0, array.Count)];
		return value;
	}

	// Returns current room of some object
	public static Room GetCurretntRoomOf (GameObject sObject)
	{
		Room result = null;
		if (sObject.GetComponent<Movement> ())
			result = sObject.GetComponent<Movement> ().currentRoom;
		else if (sObject.GetComponent<Furniture> ())
			result = sObject.GetComponent<Furniture> ().currentRoom; 
		return result;
	}
}
