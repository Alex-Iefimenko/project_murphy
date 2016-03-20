using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomObjects {

	private GameObject room;
	private Furniture[] furniture;
	private List<ICharacter> characters = new List<ICharacter>();

	public List<ICharacter> Characters { get { return characters; } }

	public RoomObjects (GameObject currentRoom)
	{
		room = currentRoom;
		furniture = room.GetComponentsInChildren<Furniture>();
	}

	public void ComeIn (Collider2D otherCollider)
	{
		ICharacter otherCharacter = otherCollider.GetComponent<Character>();
		if (otherCharacter != null && !characters.Contains(otherCharacter)) characters.Add(otherCharacter);
	}

	public void ComeOut (Collider2D otherCollider)
	{
		ICharacter otherCharacter = otherCollider.GetComponent<Character>();
		if (otherCharacter != null && characters.Contains(otherCharacter)) characters.Remove(otherCharacter);
	}
	
	public void Untrack (ICharacter character) {
		characters.Remove(character);
	}

	public Furniture GetFreeRoomObject ()
	{
		Furniture resultObject = null;
		List<Furniture> unoccypiedObjects = new List<Furniture>();
		unoccypiedObjects = furniture.Where(item => item.IsFree).ToList<Furniture>();
		if (unoccypiedObjects.Count > 0) 
			resultObject = Helpers.GetRandomArrayValue<Furniture>(unoccypiedObjects);
		return resultObject;
	}

	public Furniture GetRoomObject (string name)
	{
		Furniture resultObject = null;
		resultObject = furniture.SingleOrDefault(item => item.Name == name);
		return resultObject;
	}

	public Vector3 GetRandomRoomPoint ()
	{
		Vector3 collCenter = room.collider2D.bounds.center;
		Vector3 collExt = room.collider2D.bounds.extents * 0.85f;
		float x = Random.Range(collCenter.x - collExt.x, collCenter.x + collExt.x);
		float y = Random.Range(collCenter.y - collExt.y, collCenter.y + collExt.y);
		return new Vector3(x, y, room.transform.position.z);
	}

	public Vector3 DoorExitPoint ()
	{
		Neighbor neighbor = Helpers.GetRandomArrayValue<Neighbor>(room.GetComponent<Room>().neighbors.Values.ToArray());
		return neighbor.ExitPoint;
	}

	public Vector3 ClosestDoorExit (Vector3 position)
	{
		Neighbor[] neighbor = room.GetComponent<Room>().neighbors.Values.ToArray();
		return neighbor.OrderBy(v => Vector2.Distance(v.ExitPoint, position)).First().ExitPoint;
	}

	// Check if Room continse hostile Character
	public ICharacter ContainsHostile (Enums.CharacterSides side) 
	{
		return characters.Find(v => SidesRelations.Instance.IsEnemies(side, v.Side));
	}
	
	// Check if Room continse unconscious Character
	public ICharacter ContainsUnconscious ()
	{
		return characters.Find(v => v.IsUnconscious && !v.Lock);
	}
	
	// Check if Room continse dead Character
	public ICharacter ContainsDead ()
	{
		return characters.Find(v => v.IsDead && !v.Lock);
	}
	
	// Check if Room continse wounded Character
	public ICharacter ContainsWounded (Enums.CharacterSides side)
	{
		return characters.
			Find(v => v.IsWounded && !v.Lock && !SidesRelations.Instance.IsEnemies(side, v.Side));
	}
	
}
