using UnityEngine;
using System.Collections;

public delegate void RotationHandler (Vector3 point);

public interface IMovement
{
	IRoom CurrentRoom { get; }

	MovementTarget Target { get; }

	bool IsMoving { get; }

	GameObject GObject { get; }

	event RotationHandler LookOn;

	IMovement Walk ();

	IMovement Run ();

	void ToRoom (IRoom room);

	void ToFurniture (IRoom room, string item);

	void ToCharacter (ICharacter character);
	
	void ToItem (Item item);

	void ToPoint (Vector3 point);
	 
	void LookAt (Vector3 point);

	void Pull (IMovable other);
	
	bool IsNearObject (GameObject exactObject);
	
	void AdjustPostion (Vector3 endPoint);
	
	void Purge ();
}
