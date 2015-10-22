using UnityEngine;

public interface IMovement {

	Room CurrentRoom { get; }

	void Navigate(Room room);

	void NavigateTo(Room room);

	void NavigateTo(Room room, Furniture item);

	void NavigateTo(CharacterMain character);

	bool IsMoving();

	bool IsNearObject(GameObject exactObject);

	void Purge();

}
