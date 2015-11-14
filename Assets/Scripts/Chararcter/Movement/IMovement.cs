using UnityEngine;
using System.Collections;

public interface IMovement {

	Room CurrentRoom { get; }

	void Navigate(Room room, bool full=true);

	void NavigateTo(Room room, Furniture item=null);

	void NavigateTo(ICharacter character);

	void Anchor(GameObject other);

	bool IsMoving();

	bool IsNearObject(GameObject exactObject);

	void Purge();

	void AdjustPostion (Vector3 endPoint);

}
