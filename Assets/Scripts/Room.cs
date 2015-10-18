using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	// Room type
	public enum RoomTypes { 
		Nothing, MedBay, Safety, Engine, Control, PowerSource, LiveSupport, 
		Engineering, Dinnery, LivingQuarters, Disposal, Science
	};
	public RoomTypes roomType;

	// Room objects
	public List<Door> doors = new List<Door>();     // Door list
	public List<Room> neighbors = new List<Room>(); // Neighbors list 
	[HideInInspector] public ArrayList npcs = new ArrayList();	  // NPC list
	[HideInInspector] public Furniture[] furniture;				  // Room furniture list

	// Room collider object
	//private Collider2D roomCollider;

	// Room state
	public float maxDurability = 200f;
	public float currentDurability; 
	public Dictionary<string, float> roomStatus = new Dictionary<string, float> { {"fire", 0f}, {"radiation", 0f}, {"chemistry", 0f} };
	
	// Use this for initialization
	void Awake () {
		//roomCollider = gameObject.GetComponent<Collider2D>();
		currentDurability = maxDurability;
		furniture = GetComponentsInChildren<Furniture>();
		foreach (Furniture item in furniture) item.currentRoom = this;
	}

	// Get Attached doors to cuurent room
	public void GetAttachedDoors ()
	{
		foreach (GameObject door in ShipState.allDoors)
		{
			Door currentDoor = door.GetComponent<Door>();
			if (currentDoor.linkedRooms.ContainsKey(this)) doors.Add (currentDoor);
		}
	}

	// Get all neighbors of current room
	public void GetNeighbors ()
	{
		foreach (Door door in doors)
		{
			foreach (Room room in door.linkedRooms.Keys) if (room != this) neighbors.Add(room);		
		}
	}


	// Add NPC to Rooms NPC List
//	void OnTriggerEnter2D (Collider2D otherCollider) 
//	{
//		if (otherCollider.GetComponent<CharacterTasks>() && !npcs.Contains(otherCollider.GetComponent<CharacterTasks>()))
//			npcs.Add(otherCollider.GetComponent<CharacterTasks>());
//	}
//
//	// Delete NPC from Rooms NPC List
//	void OnTriggerExit2D (Collider2D otherCollider) 
//	{
//		if (otherCollider.GetComponent<CharacterTasks>() && npcs.Contains(otherCollider.GetComponent<CharacterTasks>()))
//			npcs.Remove(otherCollider.GetComponent<CharacterTasks>());
//	}
//
//	// Check if Room continse hostile NPC
//	public CharacterTasks ContainsHostile (bool hostile, bool killAim) 
//	{
//		foreach (CharacterTasks npc in npcs)
//		{
//			if (npc.stats.isHostile == hostile && killAim)
//			{
//				if (npc.currentState != CharacterTasks.States.Dead) return npc;
//			}
//			else if (npc.stats.isHostile == hostile && !killAim)
//			{
//				if (npc.currentState != CharacterTasks.States.Dead && npc.currentState != CharacterTasks.States.Unconscious) return npc;
//			}
//		}
//		return null;
//	}
//
//	// Check if Room continse unconscious NPC
//	public CharacterTasks ContainsUnconscious ()
//	{
//		foreach (CharacterTasks npc in npcs)
//			if (npc.currentState == CharacterTasks.States.Unconscious) return npc;
//		return null;
//	}
//
//	// Check if Room continse dead NPC
//	public CharacterTasks ContainsDead ()
//	{
//		foreach (CharacterTasks npc in npcs)
//			if (npc.currentState == CharacterTasks.States.Dead) return npc;
//		return null;
//	}
//
//	// Check if Room continse wounded NPC
//	public CharacterTasks ContainsWounded ()
//	{
//		foreach (CharacterTasks npc in npcs)
//			if (npc.stats.health < npc.stats.maxHealth) return npc;
//		return null;
//	}
//
//	// Check if some object in current room
//	public bool HasObject (GameObject sObject)
//	{
//		bool result = false;
//		if (sObject.GetComponent<CharacterTasks> () && npcs.Contains (sObject.GetComponent<CharacterTasks> ()))
//			result = true;
//		else if (sObject.GetComponent<Furniture> () && System.Array.IndexOf(furniture, sObject.GetComponent<Furniture> ()) != -1)
//			result = true;
//		return result;
//	}

	// Returns free room furniture object
	public Furniture GetUnoccupiedRoomObject ()
	{
		Furniture resultObject = null;
		List<Furniture> unoccypiedObjects = new List<Furniture>();
		foreach (Furniture item in furniture)
			if (item.isFree) unoccypiedObjects.Add (item);
		if (unoccypiedObjects.Count > 0) 
			resultObject = Helpers.GetRandomArrayValue<Furniture>(unoccypiedObjects);
		return resultObject;
	}
}
