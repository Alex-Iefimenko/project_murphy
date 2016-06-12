using UnityEngine;
using System.Collections;

public class Neighbor {

	public IRoom Room { get; set; } 
	public float Distance { get; set; }
	public Door LinkingDoor { get; set; }
	public Vector3 EntrancePoint { get; set; }
	public Vector3 ExitPoint { get; set; }

	public Neighbor (IRoom neighbor, IRoom targetRoom, Door betweenDoor)
	{
		Room = neighbor;
		Distance = Vector2.Distance(targetRoom.GObject.transform.position, neighbor.GObject.transform.position);
		LinkingDoor = betweenDoor;
		DistinctEntrances (LinkingDoor.GetEntrancePoints());
	}

	private void DistinctEntrances (Vector3[] entrances)
	{
		float distance1 = Vector3.Distance(Room.GObject.transform.position, entrances[0]);
		float distance2 = Vector3.Distance(Room.GObject.transform.position, entrances[1]);
		if (distance1 < distance2)
		{
			EntrancePoint = entrances[0];
			ExitPoint = entrances[1];
		}
		else
		{
			EntrancePoint = entrances[1];
			ExitPoint = entrances[0];
		}
	}

}
