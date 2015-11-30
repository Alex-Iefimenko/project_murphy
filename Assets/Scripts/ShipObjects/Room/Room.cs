using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using E = Enums;

public class Room : MonoBehaviour {
	
	public E.RoomTypes roomType;

	// Room objects
	public Dictionary<Room, Neighbor> neighbors = new Dictionary<Room, Neighbor>();
	
	// Room Components
	public RoomStats Stats { get;  private set; } 
	public RoomObjects Objects { get; private set; } 
	public RoomStatesHandler SatesHandler { get; private set; } 

	// Use this for initialization
	void Awake () {
		Stats = new RoomStats();
		Objects = new RoomObjects(this.gameObject);
		SatesHandler = new RoomStatesHandler(this);
	}
	
	public void AddNeighbor (Room newNeighbor, Door betweenDoor)
	{
		if (!neighbors.ContainsKey(newNeighbor)) 
			neighbors.Add (newNeighbor, new Neighbor(newNeighbor, this, betweenDoor));
	}

	// Add Character to Rooms Character List
	void OnTriggerEnter2D (Collider2D otherCollider) 
	{
		Objects.ComeIn (otherCollider);
	}

	// Delete Character from Rooms Character List
	void OnTriggerExit2D (Collider2D otherCollider) 
	{
		Objects.ComeOut (otherCollider);
	}

	public void Repair (float amount)
	{
		Stats.Durability += amount;
	}

	public void Extinguish (float amount)
	{
		Stats.FireLevel -= amount;
	}

	public void Deactivate (float amount)
	{
		Stats.RadiationLevel -= amount;
	}

	public void ChemistryClearing (float amount)
	{
		Stats.RadiationLevel -= amount;
	}

}
