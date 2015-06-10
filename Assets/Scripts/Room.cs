using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	// Room type
	public enum RoomTypes{Nothing, MedBay, Safety, Engine, Control, PowerSource, LiveSupport, Engineering, Dinnery, LivingQuarters, Disposal, Science};
	public RoomTypes roomType;

	// Room objects
	public ArrayList doors = new ArrayList();     // Door list
	public ArrayList neighbors = new ArrayList(); // Neighbors list 
	private ArrayList npcs = new ArrayList();	  // NPC list
	public Furniture[] furniture;				  // Room furniture list

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
	
	// Update is called once per frame
	void Update () {
	
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
	void OnTriggerEnter2D (Collider2D otherCollider) 
	{
		if (otherCollider.GetComponent<NPC>() && !npcs.Contains(otherCollider.GetComponent<NPC>()))
			npcs.Add(otherCollider.GetComponent<NPC>());
	}

	// Delete NPC from Rooms NPC List
	void OnTriggerExit2D (Collider2D otherCollider) 
	{
		if (otherCollider.GetComponent<NPC>() && npcs.Contains(otherCollider.GetComponent<NPC>()))
			npcs.Remove(otherCollider.GetComponent<NPC>());
	}

	// Check if Room continse hostile NPC
	public NPC ContainsHostile (bool hostile, bool killAim) 
	{
		foreach (NPC npc in npcs)
		{
			if (npc.stats.isHostile == hostile && killAim)
			{
				if (npc.currentState != NPC.States.Dead) return npc;
			}
			else if (npc.stats.isHostile == hostile && !killAim)
			{
				if (npc.currentState != NPC.States.Dead && npc.currentState != NPC.States.Unconscious) return npc;
			}
		}
		return null;
	}

	// Check if Room continse unconscious NPC
	public NPC ContainsUnconscious ()
	{
		foreach (NPC npc in npcs)
			if (npc.currentState == NPC.States.Unconscious) return npc;
		return null;
	}

	// Check if Room continse dead NPC
	public NPC ContainsDead ()
	{
		foreach (NPC npc in npcs)
			if (npc.currentState == NPC.States.Dead) return npc;
		return null;
	}

	// Check if Room continse wounded NPC
	public NPC ContainsWounded ()
	{
		foreach (NPC npc in npcs)
			if (npc.stats.health < npc.stats.maxHealth) return npc;
		return null;
	}

	// Check if some object in current room
	public bool HasObject (GameObject sObject)
	{
		bool result = false;
		if (sObject.GetComponent<NPC> () && npcs.Contains (sObject.GetComponent<NPC> ()))
			result = true;
		else if (sObject.GetComponent<Furniture> () && System.Array.IndexOf(furniture, sObject.GetComponent<Furniture> ()) != -1)
			result = true;
		return result;
	}

	// Returns free room furniture object
	public GameObject GetUnoccupiedRoomObject ()
	{
		GameObject resultObject = null;
		ArrayList unoccypiedObjects = new ArrayList();
		foreach (Furniture item in furniture)
			if (item.isFree) unoccypiedObjects.Add (item);
		if (unoccypiedObjects.Count > 0) 
			resultObject = Helpers.GetRandomArrayValue<Furniture>(unoccypiedObjects).gameObject;
		return resultObject;
	}
}
