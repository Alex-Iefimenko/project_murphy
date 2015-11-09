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
	
	public void Navigate(Room room) 
	{
		isDynamic = false;
		target = room.gameObject;
		movementPath = ShipState.Inst.GetStepsToRoom(currentRoom, room);
		character.View.RotateTowards(movementPath[0]);
		StopCoroutine("Adjust");
	}
	
	public void NavigateTo(Room room, Furniture item)
	{
		Navigate(room);
		target = item.gameObject;
		movementPath[movementPath.Count - 1] = item.GetComponent<SpriteRenderer>().bounds.center;
	}
	
	public void NavigateTo(Room room) 
	{
		Furniture item = room.GetUnoccupiedRoomObject();
		NavigateTo(room, item);
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
	}
	
	private void UpdatePath()
	{
		if (isDynamic) NavigateTo(target.GetComponent<CharacterMain>() as ICharacter);
		if (movementPath.Count > 1) character.View.RotateTowards (movementPath[1]);
		if (movementPath.Count == 1) AdjustPostion();
		movementPath.RemoveAt(0);
	}
	
	private void AdjustPostion ()
	{
		character.View.RotateTowards (target.transform.position);
		StartCoroutine("Adjust", movementPath[0]);
	}
	
	IEnumerator Adjust (Vector3 endPoint)
	{
		while(transform.position != endPoint)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
			yield return null;
		}
	}
}

