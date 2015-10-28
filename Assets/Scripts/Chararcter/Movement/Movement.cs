using UnityEngine;
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
		string path = "Characters/Controllers/" + side + "/" + type + "/" + Random.Range(1, 3).ToString() + "/";
		animContrs = Resources.LoadAll (path).Cast<RuntimeAnimatorController>().ToArray();
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

	public void NavigateTo(ICharacter character) 
	{
		Debug.Log ("METHOD NOT IMPLEMENTED YET!");
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

	public void Purge ()
	{
		movementPath.Clear();
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
	
	// Method for handle sprite Character representation update
	private void UpdateAnimator (Vector3 nextPoint)
	{
//		Vector3 p1 = transform.position;
//		Vector3 p2 = nextPoint;
//		float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
//		if ( 45f <= angle && angle <= 135f ) 
//			charAnim.runtimeAnimatorController = animContrs [3];
//		else if ( -135f <= angle && angle <= -45f )
//			charAnim.runtimeAnimatorController = animContrs [0];
//		else if ( -45f <= angle && angle <= 45f )
//			charAnim.runtimeAnimatorController = animContrs [2];
//		else if ( 135f <= angle || angle <= -135f )
//			charAnim.runtimeAnimatorController = animContrs [1];
	}

}
