using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

	void Awake () {

	}


	// Use this for initialization step by step
	void Start () {
		// Calls all neccessary function to build ship structure
		BuildShipsStructure ();
		// Checks respondability of objects to ingame time events
		// Broadcaster.UpdateRespondability ();
		// Generate NPC stats
		GenStats ();
	}
	
	// Initialize vars and build ship graph
	void BuildShipsStructure ()
	{
		// Create Lists of Rooms and Doors
		ShipState.UpdateRoomAndDoorsLists();
		// Init relations in Doors
		foreach (GameObject door in ShipState.allDoors) door.GetComponent<Door>().GetLinkedRooms();
		// Init relations in Rooms
		foreach (Room room in ShipState.allRooms.Values) 
		{
			room.GetAttachedDoors ();
			room.GetNeighbors ();
		}
		// Inititialize ship as graph
		ShipState.CreateShipStructureGraph ();
	}

	void GenStats ()
	{
		foreach (CharacterMain character in ShipState.allCharacters) character.Init();
	}
}
