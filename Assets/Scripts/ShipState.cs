using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipState : MonoBehaviour {

	public static Hashtable shipGraph = new Hashtable();
	//public static GameObject[] allRooms;
	public static GameObject[] allDoors;
	public static GameObject[] allNPC;
	public static Dictionary<int, GameObject> allRooms;
	
	//Awake function
	void Awake () {

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Initialize method for creating ship structure as graph
	// Structure {room: [[neighbor1 details], [neighbor1 details], ...], ...}
	public static void CreateShipStructureGraph ()
	{
		// Find all "Room" tagged object in iterate over them
		UpdateRoomAndDoorsLists ();
		foreach (GameObject room in allRooms.Values)
		{
			// Get Room component as current room
			Room currentRoom = room.GetComponent<Room>();
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
	{
		allDoors = GameObject.FindGameObjectsWithTag("Door");
		allNPC   = GameObject.FindGameObjectsWithTag("NPC");

		GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
		allRooms = new Dictionary<int, GameObject>();
		int corridorDummy = System.Enum.GetNames(typeof(Room.RoomTypes)).Length;
		foreach (GameObject room in rooms)
		{
			int key = (int)room.GetComponent<Room>().roomType;
			if (key == 0) 
			{
				key = corridorDummy;
				corridorDummy += 1;
			}
			allRooms.Add(key, room);
		}
	}

	// Returns list of world points for NPC traveling
	// https://gist.github.com/joninvski/701720
	public static ArrayList GetStepsToRoom (Room initialRoom, Room targetRoom) {
		// Initializing
		Hashtable distances = new Hashtable();
		Hashtable predecessors = new Hashtable();

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
			ArrayList result = new ArrayList();
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
