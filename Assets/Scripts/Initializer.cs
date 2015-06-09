using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {


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
		foreach (GameObject room in ShipState.allRooms.Values) 
		{
			room.GetComponent<Room>().GetAttachedDoors ();
			room.GetComponent<Room>().GetNeighbors ();
		}
		// Inititialize ship as graph
		ShipState.CreateShipStructureGraph ();
	}

	void GenStats ()
	{
		foreach (GameObject npc in ShipState.allNPC) npc.GetComponent<NPC>().InitGenerateCharacterStats();
	}
}
