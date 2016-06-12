using UnityEngine;
using System.Collections;

public class RoomFactory  {

	public static void CreateRoom (RoomConstructor room)
	{
		IRoomController controller = room.gameObject.AddComponent<RoomController> () as IRoomController;

		IRoomStats stats = room.gameObject.AddComponent<RoomStats> () as IRoomStats;
		stats.Init (room.roomType);

		IRoomObjectTracker objects = 
			new RoomObjectsTracker (controller, room.gameObject.GetComponentsInChildren<Furniture> ()) as IRoomObjectTracker;

		IRoomMotion motion = room.gameObject.AddComponent<RoomMotion> () as IRoomMotion;
		motion.Init (objects);

		RoomStatesHandler states = new RoomStatesHandler (controller, stats, objects);

		IRoom facade = room.gameObject.AddComponent<Room> () as IRoom;
		facade.Init (controller, stats, objects, states, motion);

		MonoBehaviour.Destroy (room);
	}



}
