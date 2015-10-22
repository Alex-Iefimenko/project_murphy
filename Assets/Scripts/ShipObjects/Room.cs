using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	// Room type
	public enum RoomTypes { 
		Nothing, MedBay, Safety, 
		Engine, Control, PowerSource, 
		LiveSupport, Engineering, Dinnery, 
		LivingQuarters, Disposal, Science
	};
	public RoomTypes roomType;

	// Room objects
	[HideInInspector] public List<Door> doors = new List<Door>();
	[HideInInspector] public List<Room> neighbors = new List<Room>();
	private List<CharacterMain> characters = new List<CharacterMain>();
	private Furniture[] furniture;
	
	// Room state
	public RoomState State { get; set; } 
	
	// Use this for initialization
	void Awake () {
		State = new RoomState();
		furniture = GetComponentsInChildren<Furniture>();
		foreach (Furniture item in furniture) item.currentRoom = this;
	}

	// Get Attached doors to curent room
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

	// Add Character to Rooms Character List
	void OnTriggerEnter2D (Collider2D otherCollider) 
	{
		CharacterMain otherCharacter = otherCollider.GetComponent<CharacterMain>();
		if (otherCharacter != null && !characters.Contains(otherCharacter)) characters.Add(otherCharacter);
	}

	// Delete Character from Rooms Character List
	void OnTriggerExit2D (Collider2D otherCollider) 
	{
		CharacterMain otherCharacter = otherCollider.GetComponent<CharacterMain>();
		if (otherCharacter != null && characters.Contains(otherCharacter)) characters.Remove(otherCharacter);
	}

	// Check if Room continse hostile Character
	public ICharacter ContainsHostile (ICharacter character) 
	{
		foreach (CharacterMain visitor in characters)
		{
			if (SidesRelations.Instance.IsEnemies(character, visitor)) return visitor;
		}
		return null;
	}

	// Check if Room continse unconscious Character
	public ICharacter ContainsUnconscious ()
	{
		foreach (ICharacter character in characters)
		{
			if (character.Stats.IsUnconscious()) return character;
		}
		return null;
	}

	// Check if Room continse dead Character
	public ICharacter ContainsDead ()
	{
		foreach (ICharacter character in characters)
		{
			if (character.Stats.IsDead()) return character;
		}
		return null;
	}

	// Check if Room continse wounded Character
	public ICharacter ContainsWounded (ICharacter character)
	{
		foreach (ICharacter visitor in characters)
		{
			if (visitor.Stats.IsWounded() && visitor != character) return visitor;
		}
		return null;
	}

//	// Check if some object in current room
//	public bool HasObject (GameObject sObject)
//	{
//		bool result = false;
//		if (sObject.GetComponent<CharacterTasks> () && Characters.Contains (sObject.GetComponent<CharacterTasks> ()))
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
