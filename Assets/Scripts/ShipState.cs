using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipState : MonoBehaviour {

	public static Hashtable shipGraph = new Hashtable();
	//public static GameObject[] allRooms;
	public static GameObject[] allDoors;
	public static CharacterMain[] allCharacters;
	public static Dictionary<int, Room> allRooms;

	// Initialize method for creating ship structure as graph
	// Structure {room: [[neighbor1 details], [neighbor1 details], ...], ...}
	public static void CreateShipStructureGraph ()
	{
		// Find all "Room" tagged object in iterate over them
		UpdateRoomAndDoorsLists ();
		foreach (Room room in allRooms.Values)
		{
			// Get Room component as current room
			Room currentRoom = room;
			// Create empty array for current room neighbors details
			ArrayList currentRoomNeighbors = new ArrayList();
			// For each neighbor fullfil itself, distance to current room, similar door and add it to array
			// Add this array to neighbors list
			foreach (Room neighbor in currentRoom.neighbors) 
			{
				ArrayList currentRoomNeighbor = new ArrayList();
				currentRoomNeighbor.Add  (neighbor);
				currentRoomNeighbor.Add  (Vector2.Distance(currentRoom.transform.position, neighbor.transform.position));
				currentRoomNeighbor.Add  (Helpers.GetDoorBetweenRooms(currentRoom, neighbor));
				currentRoomNeighbors.Add (currentRoomNeighbor);
			}
			shipGraph.Add(currentRoom, currentRoomNeighbors);
		}
	}

	// Updtates lists of doors and rooms 
	public static void UpdateRoomAndDoorsLists ()
	{	GameObject[] charactersObjects = GameObject.FindGameObjectsWithTag("Character");
		allCharacters = new CharacterMain[charactersObjects.Length];
		for (int i = 0; i < charactersObjects.Length; i++ )
			allCharacters[i] = charactersObjects[i].GetComponent<CharacterMain>();
		allDoors = GameObject.FindGameObjectsWithTag("Door");
		GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
		allRooms = new Dictionary<int, Room>();
		int corridorDummy = System.Enum.GetNames(typeof(Room.RoomTypes)).Length;
		foreach (GameObject room in rooms)
		{
			int key = (int)room.GetComponent<Room>().roomType;
			if (key == 0) 
			{
				key = corridorDummy;
				corridorDummy += 1;
			}
			allRooms.Add(key, room.GetComponent<Room>());
		}
	}

	// Returns list of world points for NPC traveling
	// https://gist.github.com/joninvski/701720
	public static List<Vector3> GetStepsToRoom (Room initialRoom, Room targetRoom) {
		// Initializing
		Hashtable distances = new Hashtable();
		Hashtable predecessors = new Hashtable();

		if (initialRoom == targetRoom) return new List<Vector3>() { targetRoom.gameObject.transform.position };

		foreach (Room node in shipGraph.Keys)
		{
			distances[node] = float.PositiveInfinity;
			predecessors[node] = null;
		}
		distances[initialRoom] = 0f;

		// Bellman-Ford algorithm itself
		for (int i = 0; i < shipGraph.Count - 1; i++)
		{
			foreach (Room room in shipGraph.Keys)
			{
				ArrayList neighbors = (ArrayList)shipGraph[room];
				foreach (ArrayList neighbor in neighbors)
				{
					if ((float)distances[neighbor[0]] > (float)distances[room] + (float)neighbor[1])
					{
						distances[neighbor[0]] = (float)distances[room] + (float)neighbor[1];
						predecessors[neighbor[0]] = room;
					}
				}
			}
		}
		// Restoring of closest path
		if ((float)distances[targetRoom] == float.PositiveInfinity)
		{
			return null;
		}
		else
		{
			List<Vector3> result = new List<Vector3>();
			Room stepRoom = targetRoom;
			Room nextStepRoom;
			Door betweenDoor;
			result.Add (targetRoom.transform.position);
			while (stepRoom != null)
			{
				nextStepRoom = (Room)predecessors[stepRoom];
				if (nextStepRoom) 
				{ 
					betweenDoor = Helpers.GetDoorBetweenRooms(stepRoom, nextStepRoom);
					result.Add ((Vector3)betweenDoor.linkedRooms[stepRoom]);
					result.Add ((Vector3)betweenDoor.linkedRooms[nextStepRoom]);
				}
				stepRoom = nextStepRoom;
			}

			result.Reverse();
			return result;
		}
	}
}
