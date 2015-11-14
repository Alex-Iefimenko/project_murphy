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
		StopCoroutine("Adjust");
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
		target = item.gameObject;
		movementPath[movementPath.Count - 1] = item.GetComponent<SpriteRenderer>().bounds.center;
	}

	public void NavigateTo(ICharacter character) 
	{
		Navigate(character.Movement.CurrentRoom);		
		target = character.GObject;
		isDynamic = true;
		movementPath[movementPath.Count - 1] = target.transform.position;
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
		StartCoroutine("Adjust", transform.position - (other.transform.position - transform.position));
	}

	public void AdjustPostion (Vector3 endPoint)
	{
		StartCoroutine("Adjust", endPoint);
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
		Vector3 nextPoint = movementPath[0];
		nextPoint.z = transform.position.z;
		transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
		if (charColl.OverlapPoint(nextPoint)) UpdatePath();
	}
	
	private void UpdatePath()
	{
		if (isDynamic) NavigateTo(target.GetComponent<CharacterMain>() as ICharacter);
		if (movementPath.Count > 1) character.View.RotateTowards (movementPath[1]);
		if (movementPath.Count == 1) AdjustPostion(movementPath[0]);
		movementPath.RemoveAt(0);
	}

	private void Pull ()
	{
		Vector3 direction = (anchoredObject.transform.position - transform.position).normalized;
		float dist1 = Mathf.Sqrt(charColl.bounds.SqrDistance(anchoredObject.transform.position + 10f * direction));
		float dist2 = Vector3.Distance(transform.position, anchoredObject.transform.position + 10f * direction);
		float size = dist2 - dist1;
		Vector3 point = transform.position + size * direction;
		anchoredObject.transform.position = Vector3.MoveTowards(anchoredObject.transform.position, point, speed * Time.deltaTime);
		float t = Mathf.Atan2((transform.position.y - anchoredObject.transform.position.y), 
		                      (transform.position.x - anchoredObject.transform.position.x)) * Mathf.Rad2Deg + 90f;
		anchoredObject.transform.eulerAngles = new Vector3 (anchoredObject.transform.eulerAngles.x, 
		                                                    anchoredObject.transform.eulerAngles.y, 
		                                                    t);
			
	}
	
	private IEnumerator Adjust (Vector3 endPoint)
	{
		while(transform.position != endPoint)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
			yield return null;
		}
		if (target != null) character.View.RotateTowards (target.transform.position);
	}
	
}

