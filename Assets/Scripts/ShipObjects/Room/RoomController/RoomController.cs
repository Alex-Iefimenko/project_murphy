using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour, IRoomController {

	public event CharacterEnterHandler OnCharacterEnter;
	public event CharacterEnterHandler OnCharacterLeave;

	public GameObject GObject { get { return gameObject; } }
	public Bounds RoomBounds { get { return collider2D.bounds; } }
	public Transform RoomTransaform { get { return collider2D.transform; } }

	private IRoom self;
	private List<Neighbor> neighbors = new List<Neighbor>();

	public List<Neighbor> Neighbors { get { return neighbors; } }

	void Start ()
	{
		self = GetComponent<Room>() as IRoom;
	}

	// Add Character to Rooms Character List
	void OnTriggerEnter2D (Collider2D otherCollider) 
	{
		ICharacter character = otherCollider.GetComponent<Character>();
		if (OnCharacterEnter != null && character != null) OnCharacterEnter (character);
	}
	
	// Delete Character from Rooms Character List
	void OnTriggerExit2D (Collider2D otherCollider) 
	{
		ICharacter character = otherCollider.GetComponent<Character>();
		if (OnCharacterEnter != null && character != null) OnCharacterLeave (character);
	}

	public void AddNeighbor (IRoom newNeighbor, Door betweenDoor)
	{
		if (GetNeighbor(newNeighbor) == null) 
				neighbors.Add (new Neighbor(newNeighbor, self, betweenDoor));
	}

	public Neighbor GetNeighbor (IRoom neighbor)
	{
		return neighbors.Find(v => v.Room == neighbor);
	}
}
