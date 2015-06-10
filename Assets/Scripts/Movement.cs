using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Character speed
	public float speed = 1f;
	// List with points of current NPC walking path
	private ArrayList movementPath = new ArrayList();
	// NPC Collider
	private Collider2D npcCollider; 
	// Current room field
	[HideInInspector] public Room currentRoom;
	[HideInInspector] public Room targetRoom;
	[HideInInspector] public GameObject targetRoomObject;

	// Use this for initialization
	void Awake () {
		// Get Door collider object
		npcCollider = gameObject.GetComponent<Collider2D>();
	}

	// Updates current room value
	void OnTriggerEnter2D (Collider2D otherCollider) 
	{
		if (otherCollider.GetComponent<Room>() && otherCollider.GetComponent<Room>() != currentRoom)
			currentRoom = otherCollider.GetComponent<Room>();
	}

	// Update is called once per frame
	void Update () {
		if (movementPath != null && movementPath.Count != 0) 
			Move ((Vector3)movementPath[0]);
		if (targetRoomObject && movementPath.Count == 0)
			MoveTowardsObject ();
	}

	// Create movement path with no params to center of the room
	public void NewMovementPath (Room localTargetRoom, bool randRoomObject) 
	{
		movementPath = ShipState.GetStepsToRoom(currentRoom, localTargetRoom);
		targetRoom = localTargetRoom;
		if (randRoomObject)
		{
			if (movementPath.Count >= 1) movementPath.RemoveAt(movementPath.Count - 1); 
			movementPath.Add(localTargetRoom.GetUnoccupiedRoomObject().transform.position);
		}

		// Place for movement path visaulization points code
		//
	}

	// Create movement to exact target object
	public void NewMovementPath (GameObject localTargetObject) 
	{
		Room targetObjectRoom = Helpers.GetCurretntRoomOf(localTargetObject);
		targetRoomObject = localTargetObject;
		NewMovementPath(targetObjectRoom, false);
	}

	// Move character towards next movements point. Delete it if reached
	private void Move (Vector3 nextPoint)
	{
		nextPoint.z = transform.position.z;
		transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
		if (npcCollider.OverlapPoint(nextPoint)) movementPath.RemoveAt(0);
	}

	// Follow for certain (dynamic object)
	private void MoveTowardsObject ()
	{
		if (currentRoom.HasObject (targetRoomObject) && !IsNearObject (targetRoomObject)) 
		{
			Vector3 nextPoint = targetRoomObject.transform.position;
			nextPoint.z = transform.position.z;
			transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);;
		}
	}

	// Check if there are no movement points in character Path
	public bool IsStaying () 
	{
		return (movementPath.Count == 0 && (targetRoomObject == null || IsNearObject(targetRoomObject)));
	}

	// Check if current Char is near object
	public bool IsNearObject (GameObject exactObject)
	{
		bool result = false;
		if (exactObject.collider2D && npcCollider.bounds.Intersects(exactObject.collider2D.bounds)) result = true;
		return result;
	}

}
