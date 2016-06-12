using UnityEngine;
using System.Collections;

public interface IRoom {

	void Init (IRoomController contr, IRoomStats rStats, IRoomObjectTracker objs, RoomStatesHandler rStates, IRoomMotion mtn);

	// Controller
	GameObject GObject { get; }
	System.Collections.Generic.List<Neighbor> Neighbors { get; }
	void AddNeighbor (IRoom newNeighbor, Door betweenDoor);
	Neighbor GetNeighbor (IRoom neighbor);

	// Objects
	System.Collections.Generic.List<ICharacter> Characters { get; }
	ICharacter ContainsUnconscious { get; }
	ICharacter ContainsDead { get; }
	ICharacter ContainsHostile (Enums.CharacterSides side);
	ICharacter ContainsWounded (GameObject character, Enums.CharacterSides side);

	void Untrack (ICharacter character);

	Furniture GetRoomObject (string name);
	Furniture GetFreeRoomObject ();
	Vector3 GetRandomRoomPoint ();
	Vector3 DoorExitPoint ();
	Vector3 ClosestDoorExit (Vector3 position);

	// Stats
	Enums.RoomTypes Type { get; } 

	bool IsLocked { get; set; }
	bool IsDangerous { get; }

	bool IsRadioactive { get; }
	void ReduceRadiation (float amount);
	bool IsHazardous { get; }
	void CleanHazard (float amount);
	bool IsOnFire { get; }
	void ReduceFire (float amount);
	bool IsBroken { get; }
	void Damage (float amount);
	void Repair (float amount);

	// States
	bool ForceState<T> (float amount) where T : RoomStateBase;

	// Motion
	void FlyUp (Vector3 point);
	void FlyAway (Vector3 point);
	void ChangeRoof (bool swch);
	Beacon Gateway { get; set; }
}
