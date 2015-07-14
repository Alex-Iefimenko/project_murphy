using UnityEngine;
using System.Collections;
using System.Linq;

public class Movement : MonoBehaviour {

	// Character speed
	public float speed = 1f;
	// List with points of current NPC walking path
	[HideInInspector] public ArrayList movementPath = new ArrayList();
	// NPC Collider
	private Collider2D npcCollider; 
	// NPC Sprite rendering
	private Animator cAnimator;
	private RuntimeAnimatorController[] controllers = new RuntimeAnimatorController[4];
	// Current room field
	[HideInInspector] public Room currentRoom;
	[HideInInspector] public Room targetRoom;
	[HideInInspector] public GameObject targetRoomObject;


	// Use this for initialization
	void Awake () {
		// Get Door collider object
		npcCollider = gameObject.GetComponent<Collider2D>();
		cAnimator   = gameObject.GetComponent<Animator>();
		controllers = Resources.LoadAll ("NPC/Crew/").Cast<RuntimeAnimatorController>().ToArray();
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
		else if (targetRoomObject && movementPath.Count == 0)
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
		targetRoomObject = localTargetObject;
		Room targetObjectRoom = Helpers.GetCurretntRoomOf(localTargetObject);
		if (targetObjectRoom != currentRoom) NewMovementPath(targetObjectRoom, false);
	}

	// Move character towards next movements point. Delete it if reached
	private void Move (Vector3 nextPoint)
	{
		nextPoint.z = transform.position.z;
		transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
		UpdateAnimator (nextPoint);
		if (npcCollider.OverlapPoint(nextPoint)) 
		{	
			if (movementPath.Count > 0) movementPath.RemoveAt(0);
			if (targetRoomObject && currentRoom == Helpers.GetCurretntRoomOf(targetRoomObject)) movementPath.Clear();
		}
	}

	// Follow for certain (dynamic object)
	private void MoveTowardsObject ()
	{
		if (!IsNearObject (targetRoomObject)) 
			Move (targetRoomObject.transform.position);
	}

	// Method for handle sprite NPC representation update
	private void UpdateAnimator (Vector3 nextPoint)
	{
		Vector3 p1 = transform.position;
		Vector3 p2 = nextPoint;
		float angle = Mathf.Atan2(p2.y-p1.y, p2.x-p1.x)*180 / Mathf.PI;
		if ( 45f <= angle && angle <= 135f ) 
			cAnimator.runtimeAnimatorController = controllers [3];
		else if ( -135f <= angle && angle <= -45f )
			cAnimator.runtimeAnimatorController = controllers [0];
		else if ( -45f <= angle && angle <= 45f )
			cAnimator.runtimeAnimatorController = controllers [2];
		else if ( 135f <= angle || angle <= -135f )
			cAnimator.runtimeAnimatorController = controllers [1];
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
		result = (exactObject.collider2D && npcCollider.bounds.Intersects (exactObject.collider2D.bounds));
		return result;
	}

}
