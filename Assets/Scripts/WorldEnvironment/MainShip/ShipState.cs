using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipState {

	private static ShipState instance;
	private static readonly object locker = new object();

	public Door[] allDoors;
	public Room[] allRooms;
	public CharacterMain[] allCharacters;
	public Dictionary<Room.RoomTypes, Room> specRooms;
	private ShipStructure shipStructure;

	private ShipState () { }

	public static ShipState Inst
	{
		get
		{
			lock (locker)
			{
				if (instance == null) instance = new ShipState();
			}
			return instance;
		}
	}

	public void Init ()
	{
		CreateRoomAndDoorsLists ();
		CreateShipStructureGraph ();
	}

	// Returns list of world points for NPC traveling
	public List<Vector3> GetStepsToRoom (Room initialRoom, Room targetRoom) {
		if (initialRoom == targetRoom) 
			return new List<Vector3>() { targetRoom.gameObject.transform.position };
		else
			return shipStructure.GetStepsToRoom(initialRoom, targetRoom);
	}

	// Initialize method for creating ship structure as graph
	private void CreateShipStructureGraph ()
	{
		shipStructure = new ShipStructure(allRooms);
	}

	// Updtates lists of doors and rooms 
	private void CreateRoomAndDoorsLists ()
	{	
		allDoors = StoreComponent<Door>(GameObject.FindGameObjectsWithTag("Door"));
		allRooms = StoreComponent<Room>(GameObject.FindGameObjectsWithTag("Room"));
		allCharacters = StoreComponent<CharacterMain>(GameObject.FindGameObjectsWithTag("Character"));
		AddSpecRooms();
		allDoors[0].ConnectNeighbors();
		for (int i = 0; i < allDoors.Length; i++ ) allDoors[i].ConnectNeighbors();
	}

	private T[] StoreComponent<T> (GameObject[] list) where T : UnityEngine.Component
	{
		T[] componentsList = new T[list.Length];
		for (int i = 0; i < list.Length; i++)
		{
			componentsList[i] = list[i].GetComponent<T>();
		}
		return componentsList;
	}

	private void AddSpecRooms ()
	{
		specRooms = new Dictionary<Room.RoomTypes, Room> ();
		for (int i = 0; i < allRooms.Length; i++)
		{
			if (allRooms[i].roomType != Room.RoomTypes.Nothing) specRooms.Add (allRooms[i].roomType, allRooms[i]);
		} 
	}
}
