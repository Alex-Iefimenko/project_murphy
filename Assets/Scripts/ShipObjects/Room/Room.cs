using UnityEngine;
using E = Enums;

public class Room : MonoBehaviour, IRoom {

	private IRoomController controller;
	private IRoomStats stats;
	private IRoomObjectTracker objects;
	private RoomStatesHandler states;
	private IRoomMotion motion;

	public void Init (IRoomController contr, IRoomStats rStats, IRoomObjectTracker objs, RoomStatesHandler rStates, IRoomMotion mtn) 
	{
		controller = contr;
		stats = rStats;
		objects = objs;
		states = rStates;
		motion = mtn;
	}

	// Controller
	public GameObject GObject { get { return controller.GObject; } }
	public System.Collections.Generic.List<Neighbor> Neighbors { get { return controller.Neighbors; } }
	public void AddNeighbor (IRoom newNeighbor, Door betweenDoor) { controller.AddNeighbor(newNeighbor, betweenDoor); }
	public Neighbor GetNeighbor (IRoom neighbor) { return controller.GetNeighbor (neighbor); }
	
	// Objects
	public System.Collections.Generic.List<ICharacter> Characters { get { return objects.Characters; } }
	public ICharacter ContainsUnconscious { get { return objects.ContainsUnconscious (); } }
	public ICharacter ContainsDead { get { return objects.ContainsDead (); } }
	public ICharacter ContainsHostile (Enums.CharacterSides side) { return objects.ContainsHostile (side); }
	public ICharacter ContainsWounded (GameObject character, Enums.CharacterSides side) { 
		return objects.ContainsWounded (character, side); 
	}
	public void Untrack (ICharacter character) { objects.Untrack (character); }
	public Furniture GetRoomObject (string name) { return objects.GetRoomObject (name); }
	public Furniture GetFreeRoomObject () { return objects.GetFreeRoomObject (); }
	public Vector3 GetRandomRoomPoint () { return objects.GetRandomRoomPoint (); }
	public Vector3 DoorExitPoint () { return objects.DoorExitPoint (); }
	public Vector3 ClosestDoorExit (Vector3 position) { return objects.ClosestDoorExit (position); }
	
	// Stats
	public Enums.RoomTypes Type { get { return stats.Type; } } 
	public bool IsLocked { get { return stats.Locked; } set { stats.Locked = value;} }
	public bool IsDangerous { get { return stats.IsDangerous; } } 
	public bool IsRadioactive { get { return stats.IsRadioactive; } } 
	public void ReduceRadiation (float amount) { stats.ReduceRadiation (amount); }
	public bool IsHazardous { get { return stats.IsHazardous; } } 
	public void CleanHazard (float amount) { stats.CleanHazard (amount); }
	public bool IsOnFire { get { return stats.IsOnFire; } } 
	public void ReduceFire (float amount) { stats.ReduceFire (amount); }
	public bool IsBroken { get { return stats.IsBroken; } } 
	public void Damage (float amount) { stats.Damage (amount); }
	public void Repair (float amount) { stats.Repair (amount); }
	
	// States
	public bool ForceState<T> (float amount) where T : RoomStateBase { return states.ForceState<T> (amount); }
	
	// Motion
	public void FlyUp (Vector3 point) { motion.FlyUp (point); }
	public void FlyAway (Vector3 point) { motion.FlyAway (point); }
	public void ChangeRoof (bool swch) { motion.ChangeRoof (swch); }
	public Beacon Gateway { get { return motion.Gateway; } set { motion.Gateway = value;} }

}
