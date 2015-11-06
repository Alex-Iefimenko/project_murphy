using UnityEngine;
using System.Collections;

public class Neighbor {

	public Room Room { get; set; } 
	public float Distance { get; set; }
	public Door LinkingDoor { get; set; }
	public Vector3 EntrancePoint { get; set; }
	public Vector3 ExistPoint { get; set; }

	public Neighbor (Room neighbor, Room targetRoom, Door betweenDoor)
	{
		Room = neighbor;
		Distance = Vector2.Distance(targetRoom.transform.position, neighbor.transform.position);
		LinkingDoor = betweenDoor;
		DistinctEntrances (LinkingDoor.GetEntrancePoints());
	}

	private void DistinctEntrances (Vector3[] entrances)
	{
		float distance1 = Vector3.Distance(Room.transform.position, entrances[0]);
		float distance2 = Vector3.Distance(Room.transform.position, entrances[1]);
		if (distance1 < distance2)
		{
			EntrancePoint = entrances[0];
			ExistPoint = entrances[1];
		}
		else
		{
			EntrancePoint = entrances[1];
			ExistPoint = entrances[0];
		}
	}

}
