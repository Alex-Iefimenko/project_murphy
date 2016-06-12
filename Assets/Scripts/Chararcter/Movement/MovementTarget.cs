using UnityEngine;
using System.Collections;

public class MovementTarget
{
	public GameObject GObject { get; private set; }
	public IRoom Room { get; private set; }
	public Vector3 Position { get { return endPosition(); } }
	public Vector3 Center { get { return centerPosition(); } }
	public bool IsDynamic { get; private set; }

	private Transform transform;
	private Vector3 point;
	private Furniture furniture;
	private GameObject character;

	private delegate Vector3 PositionDeleagte();
	PositionDeleagte endPosition;
	PositionDeleagte centerPosition;

	public MovementTarget (GameObject self, IRoom room)
	{
		Room = room;
		GObject = room.GObject;
		IsDynamic = false;
		character = self;
		transform = room.GObject.transform;
		endPosition = ClosestEntrance;
		centerPosition = TransformPositon;
	}

	public MovementTarget (IRoom room, string item)
	{
		Room = room;
		IsDynamic = false;
		Furniture obj = item == "Random" ? room.GetFreeRoomObject() : room.GetRoomObject(item);
		if (obj != null) 
		{
			GObject = obj.gameObject;
			transform = obj.gameObject.transform;
			endPosition = TransformPositon;
			furniture = obj;
			centerPosition = FurniturePosition;
		}
		else
		{
			point = room.GetRandomRoomPoint();
			endPosition = PointPosition;
			centerPosition = RandomDirection;
		} 
	}
	
	public MovementTarget (GameObject self, ICharacter target)
	{
		Room = target.CurrentRoom;
		GObject = target.GObject;
		IsDynamic = target.IsActive;
		character = self;
		transform = target.GObject.transform;
		endPosition = ClosestCollPoint;
		centerPosition = TransformPositon;
	}

	public MovementTarget (GameObject self, Item item)
	{
		Room = ShipState.Inst.RoomByPoint(item.transform.position);
		GObject = item.gameObject;
		IsDynamic = false;
		character = self;
		transform = item.gameObject.transform;
		endPosition = ClosestCollPoint;
		centerPosition = TransformPositon;
	}

	public MovementTarget (Vector3 newPoint)
	{
		Room = ShipState.Inst.RoomByPoint(newPoint);
		GObject = Room.GObject;
		IsDynamic = false;
		transform = Room.GObject.transform;
		point = newPoint;
		endPosition = PointPosition;
		centerPosition = RandomDirection;
	}

	//
	// Adapter methods
	//
	
	private Vector3 ClosestEntrance ()
	{
		return Room.ClosestDoorExit(character.transform.position);
	}

	private Vector3 TransformPositon ()
	{
		return transform.position;
	}

	private Vector3 PointPosition ()
	{
		return point;
	}

	private Vector3 FurniturePosition ()
	{
		return furniture.Direction;
	}

	private Vector3 ClosestCollPoint ()
	{
		// Terget Collider size
		Vector3 direction = (character.collider2D.bounds.center - GObject.collider2D.bounds.center).normalized;
		float size1 = GObject.collider2D.bounds.extents.x;
		// Character collider size
		float size2 = character.collider2D.bounds.extents.x;
		// Point
		Vector3 point = GObject.collider2D.bounds.center + (size1 + size2) * direction;
		return point;
	}

	private Vector3 RandomDirection ()
	{
		return new Vector3 (Random.Range(-1, 1), Random.Range(-1, 1));
	}
}
