using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShipState {

	private static ShipState instance;
	private static readonly object locker = new object();

	public Door[] allDoors;
	public Room[] allRooms;
	public CharacterMain[] allCharacters;
	public CharacterMain player;
	public Dictionary<Enums.RoomTypes, Room> specRooms;
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

	public void CountCharacters ()
	{
		allCharacters = StoreComponent<CharacterMain>(GameObject.FindGameObjectsWithTag("Character"));
		player = System.Array.Find(allCharacters, v => v.Type == Enums.CharacterTypes.Murphy);
		if (player) GameObject.FindGameObjectWithTag("GameController").GetComponent<Navigation>().Player = player;
	}

	// Returns list of world points for NPC traveling
	public List<Vector3> GetStepsToRoom (Room initialRoom, Room targetRoom) {
		if (initialRoom == targetRoom) 
			return new List<Vector3>() { };
		else
			return shipStructure.GetStepsToRoom(initialRoom, targetRoom);
	}

	public Room RandomNamedRoom ()
	{
		return Helpers.GetRandomArrayValue<Room>(specRooms.Values.ToArray());
	}

	public Room RandomRoom ()
	{
		return Helpers.GetRandomArrayValue<Room>(allRooms);
	}

	public Room RoomByPoint(Vector3 point)
	{
		Room room = null;
		room = allRooms.Single(v => v.collider2D.OverlapPoint(point));
		return room;
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
		AddSpecRooms();
		for (int i = 0; i < allRooms.Length; i++ ) allRooms[i].neighbors.Clear();
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
		specRooms = new Dictionary<Enums.RoomTypes, Room> ();
		for (int i = 0; i < allRooms.Length; i++)
		{
			if (allRooms[i].roomType != Enums.RoomTypes.Nothing) specRooms.Add (allRooms[i].roomType, allRooms[i]);
		} 
	}
	
}
