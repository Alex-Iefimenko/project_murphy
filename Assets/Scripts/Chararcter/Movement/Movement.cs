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
	private bool isDynamic;
	private GameObject target;
	private GameObject anchoredObject;
	
	// Components
	private Collider2D charColl; 
	private ICharacter character;
	
	//
	// Initializing
	//
	
	void Awake () {
		character = (ICharacter)gameObject.GetComponent<CharacterMain>();
		charColl = gameObject.GetComponent<Collider2D>();
		speed = character.Stats.Speed;
	}
	
	//
	// Public Methods
	//
	
	public Room CurrentRoom
	{
		get { return currentRoom; }
	}
	
	public void Navigate(Room room, bool full=true) 
	{
		StopAllCoroutines();
		isDynamic = false;
		target = room.gameObject;
		movementPath = ShipState.Inst.GetStepsToRoom(currentRoom, room);
		if (!full) movementPath.RemoveAt(movementPath.Count - 1);
		character.View.RotateTowards(movementPath[0]);
	}
	
	public void NavigateTo(Room room, Furniture item=null)
	{
		if (item == null) item = room.GetUnoccupiedRoomObject();
		Navigate(room);
		if (item != null)
		{
			target = item.gameObject;
			item.isFree = false;
			movementPath[movementPath.Count - 1] = item.GetComponent<SpriteRenderer>().bounds.center;
		}
		else
		{
			target = null;
			movementPath[movementPath.Count - 1] = room.GetRandomRoomPoint();
		}
	}

	public void NavigateTo(ICharacter character) 
	{
		Navigate(character.Movement.CurrentRoom);		
		target = character.GObject;
		isDynamic = !(character.Stats.IsDead() || character.Stats.IsUnconscious());
		movementPath[movementPath.Count - 1] = GetClosestCollPoint(target, gameObject);
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

	public void Anchor(GameObject other)
	{
		anchoredObject = other;
		AdjustPostion(transform.position - (other.transform.position - transform.position), false);
	}

	public void AdjustPostion (Vector3 endPoint, bool stopOnTouch=false)
	{
		StartCoroutine(Adjust(endPoint, stopOnTouch));
	}

	public void Purge ()
	{
		movementPath.Clear();
		target = null;
		isDynamic = false;
		anchoredObject = null;
		transform.rotation = Quaternion.identity;
	}
	
	//
	// Private Methods
	//
	
	// Update is called once per frame
	void Update () {
		if (movementPath.Count != 0) Move ();
		if (anchoredObject != null) Pull ();
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
		if (isDynamic && movementPath.Count == 1) movementPath[movementPath.Count - 1] = GetClosestCollPoint(target, gameObject);
		Vector3 nextPoint = movementPath[0];
		nextPoint.z = transform.position.z;
		transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
		if (charColl.OverlapPoint(nextPoint)) UpdatePath();
	}
	
	private void UpdatePath()
	{
		if (isDynamic && movementPath.Count > 1) NavigateTo(target.GetComponent<CharacterMain>() as ICharacter);
		if (movementPath.Count > 1) character.View.RotateTowards (movementPath[1]);
		if (movementPath.Count == 1) AdjustPostion(movementPath[0], isDynamic);
		movementPath.RemoveAt(0);
	}

	private void Pull ()
	{
		Transform objTransf = anchoredObject.transform;
		anchoredObject.transform.position = 
			Vector3.MoveTowards(objTransf.position, GetClosestCollPoint(gameObject, anchoredObject), speed * Time.deltaTime);
		float t = Mathf.Atan2((transform.position.y - objTransf.position.y), 
		                      (transform.position.x - objTransf.position.x)) * Mathf.Rad2Deg + 90f;
		anchoredObject.transform.eulerAngles = new Vector3 (objTransf.eulerAngles.x, objTransf.eulerAngles.y, t);
	}
	
	private IEnumerator Adjust (Vector3 endPoint, bool stopOnTargetTouch)
	{
		while(transform.position != endPoint)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
			if (stopOnTargetTouch && charColl.bounds.Intersects (target.collider2D.bounds)) yield break;
			yield return null;
		}
		if (target != null) character.View.RotateTowards (target.transform.position);
	}

	private Vector3 GetClosestCollPoint (GameObject tObject, GameObject refObject)
	{
		Vector3 direction = (refObject.transform.position - tObject.transform.position).normalized;
		float dist1 = Mathf.Sqrt(tObject.collider2D.bounds.SqrDistance(refObject.transform.position + 10f * direction));
		float dist2 = Vector3.Distance(tObject.transform.position, refObject.transform.position + 10f * direction);
		float size = dist2 - dist1;
		Vector3 point = tObject.transform.position + size * direction;
		return point;
	}
}

