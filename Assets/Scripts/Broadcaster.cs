using UnityEngine;
using System.Collections;

static class Broadcaster {
	/*
	// Is respondable to tick
	static bool npcRespondToTick = false;
	static bool doorRespondToTick = false;
	static bool roomRespondToTick = false;

	// Is respondable to tick
	static bool npcRespondToAction = false;
	static bool doorRespondToAction = false;
	static bool roomRespondToAction = false;

	// Is respondable to tick
	static bool npcRespondToDay = false;
	static bool doorRespondToDay = false;
	static bool roomRespondToDay = false;


	// Check if objects respondable for Tick, Action, Day
	public static void UpdateRespondability ()
	{
		npcRespondToTick  = typeof(NPC).GetMethod ("Tick") != null;
		doorRespondToTick = typeof(Door).GetMethod ("Tick") != null;
		roomRespondToTick = typeof(Room).GetMethod ("Tick") != null;

		npcRespondToAction  = typeof(NPC).GetMethod ("Action") != null;
		doorRespondToAction = typeof(Door).GetMethod ("Action") != null;
		roomRespondToAction = typeof(Room).GetMethod ("Action") != null;

		npcRespondToDay  = typeof(NPC).GetMethod ("Day") != null;
		doorRespondToDay = typeof(Door).GetMethod ("Day") != null;
		roomRespondToDay = typeof(Room).GetMethod ("Day") != null;
	}
*/
	// Broadcast Tick
	public static void BroadcastTick () {
		//if (npcRespondToTick)  
		foreach (GameObject npc in ShipState.allNPC) npc.GetComponent<CharacterTasks>().Tick();
		//if (doorRespondToTick) foreach (GameObject door in ShipState.allDoors) door.GetComponent<Door>().Tick();
		//if (roomRespondToTick) foreach (GameObject room in ShipState.allRooms) room.GetComponent<Room>().Tick();
	}

	// Broadcast Action
	public static void BroadcastAction () {
		//if (npcRespondToAction)  foreach (GameObject npc in ShipState.allNPC) npc.GetComponent<NPC>().Action();
		//if (doorRespondToAction) foreach (GameObject door in ShipState.allDoors) door.GetComponent<Door>().Tick();
		//if (roomRespondToAction) foreach (GameObject room in ShipState.allRooms) room.GetComponent<Room>().Tick();
	}

	// Broadcast Day
	public static void BroadcastDay () {
		//if (npcRespondToDay)  foreach (GameObject npc in ShipState.allNPC) npc.GetComponent<NPC>().Day();
		//if (doorRespondToDay) foreach (GameObject door in ShipState.allDoors) door.GetComponent<Door>().Tick();
		//if (roomRespondToDay) foreach (GameObject room in ShipState.allRooms) room.GetComponent<Room>().Tick();
	}
}
