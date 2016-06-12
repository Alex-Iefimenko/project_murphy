using UnityEngine;
using System.Collections;

public delegate void CharacterEnterHandler (ICharacter character); 

public interface IRoomController {

	event CharacterEnterHandler OnCharacterEnter;
	event CharacterEnterHandler OnCharacterLeave;

	GameObject GObject { get; }
	Bounds RoomBounds { get; }
	Transform RoomTransaform { get; }
	System.Collections.Generic.List<Neighbor> Neighbors { get; }
	void AddNeighbor (IRoom newNeighbor, Door betweenDoor);
	Neighbor GetNeighbor (IRoom neighbor);
}
