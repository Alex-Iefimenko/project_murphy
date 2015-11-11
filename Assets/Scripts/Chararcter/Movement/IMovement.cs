using UnityEngine;

public interface IMovement {

	Room CurrentRoom { get; }

	void Navigate(Room room, bool full=true);

	// refactor to random in next object
	void NavigateTo(Room room);

	void NavigateTo(Room room, Furniture item);

	void NavigateTo(ICharacter character);

	void Anchor(GameObject other);

	bool IsMoving();

	bool IsNearObject(GameObject exactObject);

	void Purge();

}
