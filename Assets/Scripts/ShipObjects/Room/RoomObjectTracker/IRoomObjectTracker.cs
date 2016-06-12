using UnityEngine;
using System.Collections;

public interface IRoomObjectTracker {

	System.Collections.Generic.List<ICharacter> Characters { get; }
	void Untrack (ICharacter character);

	Furniture GetFreeRoomObject ();
	Furniture GetRoomObject (string name);
	Vector3 GetRandomRoomPoint ();

	Vector3 DoorExitPoint ();
	Vector3 ClosestDoorExit (Vector3 position);

	ICharacter ContainsHostile (Enums.CharacterSides side);
	ICharacter ContainsUnconscious ();
	ICharacter ContainsDead ();
	ICharacter ContainsWounded (GameObject character, Enums.CharacterSides side);
}
