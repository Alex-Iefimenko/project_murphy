﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Movement : MonoBehaviour, IMovement {

	//
	// Variables
	//

	// Character speed
	private float speed = 0.5f;

	// Path components
	private List<Vector3> movementPath = new List<Vector3>();
	private Room currentRoom;

	// Components
	private Collider2D charColl; 
	private Animator charAnim;
	private RuntimeAnimatorController[] animContrs = new RuntimeAnimatorController[4];

	//
	// Initializing
	//
	
	void Awake () {
		ICharacter currentCharater = (ICharacter)gameObject.GetComponent<CharacterMain>();
		charColl   = gameObject.GetComponent<Collider2D>();
		charAnim   = gameObject.GetComponent<Animator>();
		string side = System.Enum.GetName (typeof(CharacterMain.CharacterSides), currentCharater.Side);
		string type = System.Enum.GetName (typeof(CharacterMain.CharacterTypes), currentCharater.Type);
		animContrs = Resources.LoadAll ("Characters/Controllers/" + side + "/" + type + "/").Cast<RuntimeAnimatorController>().ToArray();
		speed = currentCharater.Stats.Speed;
	}

	//
	// Public Methods
	//

	public Room CurrentRoom
	{
		get { return currentRoom; }
	}

	public void Navigate(Room room) 
	{
		movementPath = ShipState.GetStepsToRoom(currentRoom, room);
	}

	public void NavigateTo(Room room) 
	{
		Furniture item = room.GetUnoccupiedRoomObject();
		NavigateTo(room, item);
	}

	public void NavigateTo(Room room, Furniture item)
	{
		Navigate(room);
		movementPath[-1] = item.gameObject.transform.position;
	}

	public void NavigateTo(CharacterMain character) 
	{

	}
	
	public bool IsMoving () 
	{
		return movementPath.Count != 0;
	}

	// Check if current Char is near object
	public bool IsNearObject (GameObject exactObject)
	{
		return (exactObject.collider2D != null && charColl.bounds.Intersects (exactObject.collider2D.bounds));
	}

	//
	// Private Methods
	//

	// Update is called once per frame
	void Update () {
		if (movementPath.Count != 0) Move ();
	}

	// Updates current room value
	void OnTriggerEnter2D (Collider2D otherCollider) 
	{
		Room room = otherCollider.GetComponent<Room>();
		if (room != null && room != currentRoom) currentRoom = otherCollider.GetComponent<Room>();
	}

	// Move character towards next movements point. Delete it if reached
	private void Move ()
	{
		Vector3 nextPoint = movementPath[0];
		nextPoint.z = transform.position.z;
		transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
		if (charColl.OverlapPoint(nextPoint)) UpdatePath();
		//{	
		//	if (movementPath.Count == 1) AdjustPostion();
		//}
	}

	private void UpdatePath()
	{
		movementPath.RemoveAt(0);
		UpdateAnimator (movementPath[0]);
	}

	private void AdjustPostion ()
	{
//		Vector3 pos = (Vector3)movementPath[0];
//		transform.position = new Vector3(pos.x, pos.y, transform.position.z);
//		transform.rotation = endPointRotation;
	}
	
	// Method for handle sprite NPC representation update
	private void UpdateAnimator (Vector3 nextPoint)
	{
		Vector3 p1 = transform.position;
		Vector3 p2 = nextPoint;
		float angle = Mathf.Atan2(p2.y-p1.y, p2.x-p1.x)*180 / Mathf.PI;
		if ( 45f <= angle && angle <= 135f ) 
			charAnim.runtimeAnimatorController = animContrs [3];
		else if ( -135f <= angle && angle <= -45f )
			charAnim.runtimeAnimatorController = animContrs [0];
		else if ( -45f <= angle && angle <= 45f )
			charAnim.runtimeAnimatorController = animContrs [2];
		else if ( 135f <= angle || angle <= -135f )
			charAnim.runtimeAnimatorController = animContrs [1];
	}















//	// Create movement path with no params to center of the room
//	public void NewMovementPath (Room localTargetRoom, bool randRoomObject) 
//	{
//		transform.rotation = Quaternion.identity;
//		movementPath = ShipState.GetStepsToRoom(currentRoom, localTargetRoom);
//		targetRoom = localTargetRoom;
//		if (randRoomObject)
//		{
//			if (movementPath.Count >= 1) movementPath.RemoveAt(movementPath.Count - 1);
//			GameObject furniture = localTargetRoom.GetUnoccupiedRoomObject();
//			furniture.GetComponent<Furniture>().isFree = false;
//			movementPath.Add(furniture.transform.position);
//			endPointRotation = furniture.transform.rotation;
//		}
//	}

//	// Create movement to exact target object
//	public void NewMovementPath (GameObject localTargetObject) 
//	{
//		targetRoomObject = localTargetObject;
//		Room targetObjectRoom = Helpers.GetCurretntRoomOf(localTargetObject);
//		if (targetObjectRoom != currentRoom) NewMovementPath(targetObjectRoom, false);
//	}
//
//	// Move character towards next movements point. Delete it if reached
//	private void Move (Vector3 nextPoint)
//	{
//		nextPoint.z = transform.position.z;
//		transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
//		UpdateAnimator (nextPoint);
//		if (npcCollider.OverlapPoint(nextPoint)) 
//		{	
//			if (movementPath.Count == 1) AdjustPostion();
//			movementPath.RemoveAt(0);
//			if (targetRoomObject && currentRoom == Helpers.GetCurretntRoomOf(targetRoomObject)) movementPath.Clear();
//		}
//	}
//
//	private void AdjustPostion ()
//	{
//		Vector3 pos = (Vector3)movementPath[0];
//		transform.position = new Vector3(pos.x, pos.y, transform.position.z);
//		transform.rotation = endPointRotation;
//	}
//
//	// Follow for certain (dynamic object)
//	private void MoveTowardsObject ()
//	{
//		if (!IsNearObject (targetRoomObject)) 
//			Move (targetRoomObject.transform.position);
//	}
//
//	// Method for handle sprite NPC representation update
//	private void UpdateAnimator (Vector3 nextPoint)
//	{
//		Vector3 p1 = transform.position;
//		Vector3 p2 = nextPoint;
//		float angle = Mathf.Atan2(p2.y-p1.y, p2.x-p1.x)*180 / Mathf.PI;
//		if ( 45f <= angle && angle <= 135f ) 
//			cAnimator.runtimeAnimatorController = controllers [3];
//		else if ( -135f <= angle && angle <= -45f )
//			cAnimator.runtimeAnimatorController = controllers [0];
//		else if ( -45f <= angle && angle <= 45f )
//			cAnimator.runtimeAnimatorController = controllers [2];
//		else if ( 135f <= angle || angle <= -135f )
//			cAnimator.runtimeAnimatorController = controllers [1];
//	}

//
//	// Check if current Char is near object
//	public bool IsNearObject (GameObject exactObject)
//	{
//		bool result = false;
//		result = (exactObject.collider2D && npcCollider.bounds.Intersects (exactObject.collider2D.bounds));
//		return result;
//	}

}