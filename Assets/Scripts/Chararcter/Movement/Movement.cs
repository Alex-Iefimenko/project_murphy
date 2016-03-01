using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Movement : MonoBehaviour, IMovement
{
	//
	// Variables
	//
	
	// Character speed
	private float walkSpeed;
	private float runSpeed;
	private float currentSpeed;

	// Path components
	private List<Vector3> movementPath = new List<Vector3> ();
	private MovementTarget target;
	private Room currentRoom;
	private IMovable pulledObject;
	
	// Components
	private Collider2D charColl; 
	private ICharacter character;

	// Execution Delegate
	delegate void MultiDelegate(); 
	private MultiDelegate onMove;

	//
	// Initializing
	//
	
	void Awake ()
	{
		character = (ICharacter)gameObject.GetComponent<CharacterMain> ();
		charColl = gameObject.GetComponent<Collider2D> ();
		walkSpeed = character.Stats.WalkSpeed;
		runSpeed = character.Stats.RunSpeed;
		currentSpeed = walkSpeed;
	}
	
	//
	// Public Properties
	//
	
	public Room CurrentRoom { get { return currentRoom; } }

	public MovementTarget Target { get { return target; } }

	public bool IsMoving { get { return movementPath.Count != 0; } }

	//
	// Public Methods
	//

	public IMovement Walk ()
	{
		currentSpeed = walkSpeed;
		return this;
	}

	public IMovement Run ()
	{
		currentSpeed = runSpeed;
		return this;
	}

	public void ToRoom (Room room)
	{
		Navigate (new MovementTarget (character, room));
	}

	public void ToFurniture (Room room, string item)
	{
		Navigate (new MovementTarget (room, item));
	}
	
	public void ToCharacter (ICharacter targetCharacter)
	{
		Navigate (new MovementTarget (character, targetCharacter));
	}

	public void ToItem (Item item)
	{
		Navigate (new MovementTarget (character, item));
	}

	public void ToPoint (Vector3 point)
	{
		Navigate (new MovementTarget (point));
	}

	public bool IsNearObject (GameObject exactObject)
	{
		return (exactObject.collider2D != null && charColl.bounds.Intersects (exactObject.collider2D.bounds));
	}

	public void Pull (IMovable other)
	{
		pulledObject = other;
		other.Lock = true;
		onMove += Pull;
	}
	
	public void AdjustPostion (Vector3 endPoint)
	{
		character.View.RotateTowards (endPoint);
		StartCoroutine (Adjust (endPoint));
	}
	
	public void Purge ()
	{
		StopAllCoroutines ();
		movementPath.Clear ();
		target = null;
		if (pulledObject != null) pulledObject.Lock = false;
		pulledObject = null;
		transform.rotation = Quaternion.identity;
		onMove = null;
	}

	//
	// Private Methods
	//
	
	private void Navigate (MovementTarget newTarget)
	{
		Purge ();
		target = newTarget;
		movementPath = ShipState.Inst.GetStepsToRoom (currentRoom, target.Room);
		movementPath.Add (target.Position);
		character.View.RotateTowards (movementPath[0]);
		onMove += Move;
		onMove += UpdateDynamic;
	}

	// Updates current room value
	private void OnTriggerEnter2D (Collider2D otherCollider)
	{
		Room room = otherCollider.GetComponent<Room> ();
		if (room != null && room != currentRoom) currentRoom = room;
	}

	private void Update ()
	{
		if (onMove != null) onMove();
	}

	private void Move ()
	{
		Vector3 nextPoint = movementPath[0];
		nextPoint.z = transform.position.z;
		transform.position = Vector3.MoveTowards (transform.position, nextPoint, currentSpeed * Time.deltaTime);
		if (charColl.OverlapPoint (nextPoint)) UpdatePath ();
	}

	private void UpdatePath ()
	{
		movementPath.RemoveAt (0);
		if (movementPath.Count > 0) 
		{
			if (target.IsDynamic) Navigate (target);
			character.View.RotateTowards (movementPath[0]);
		}
		else 
		{
			AdjustPostion (target.Position);
			onMove -= Move;
			onMove -= UpdateDynamic;
			character.View.RotateTowards (target.Center);
		}
	}

	private void UpdateDynamic ()
	{
		if (target.IsDynamic && movementPath.Count == 1)
		{
			movementPath[0] = target.Position;
		}
	}

	private void Pull ()
	{
		Transform pulledTransf = pulledObject.GObject.transform;
		pulledTransf.position = Vector3.MoveTowards (pulledTransf.position, 
		                                             GetClosestCollPoint(character.GObject, pulledTransf.gameObject), 
		                                             currentSpeed * Time.deltaTime);
		float t = Mathf.Atan2 ((transform.position.y - pulledTransf.position.y), 
		                       (transform.position.x - pulledTransf.position.x)) * Mathf.Rad2Deg + 90f;
		pulledTransf.eulerAngles = new Vector3 (pulledTransf.eulerAngles.x, pulledTransf.eulerAngles.y, t);

	}
	
	private IEnumerator Adjust (Vector3 endPoint)
	{
		while (transform.position != endPoint) 
		{
			endPoint.z = transform.position.z;
			transform.position = Vector3.MoveTowards (transform.position, endPoint, currentSpeed * Time.deltaTime);
			yield return null;
		}
	}
	
	private Vector3 GetClosestCollPoint (GameObject tObject, GameObject refObject)
	{
		Vector3 direction = (refObject.transform.position - tObject.transform.position).normalized;
		float dist1 = Mathf.Sqrt (tObject.collider2D.bounds.SqrDistance (refObject.transform.position + 10f * direction));
		float dist2 = Vector3.Distance (tObject.transform.position, refObject.transform.position + 10f * direction);
		float size = dist2 - dist1;
		Vector3 point = tObject.transform.position + size * direction;
		return point;
	}

}

