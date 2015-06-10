using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Hashtable linkedRooms = new Hashtable();
	//private Collider2D doorCollider;
	private Animator[] doorsAnimators;
	private int numberOfNPC = 0;

	// Use this for initialization
	void Awake () {
		// Get Door collider object
		//doorCollider = gameObject.GetComponent<Collider2D>();
		doorsAnimators = GetComponentsInChildren<Animator>();
	}
	
	// Method returns Linked to door Room as key and closest to it point as value
	public void GetLinkedRooms ()
	{
		Vector3 entranceOne = transform.position + transform.up * GetComponent<BoxCollider2D>().size.y / 2f * transform.localScale.y;
		Vector3 entranceTwo = transform.position - transform.up * GetComponent<BoxCollider2D>().size.y / 2f * transform.localScale.y;

		foreach (GameObject room in ShipState.allRooms.Values)
		{
			Room currentRoom = room.GetComponent<Room>();
			if (room.collider2D.OverlapPoint(entranceOne) || room.collider2D.OverlapPoint(entranceTwo))
			{
				linkedRooms.Add (currentRoom, Helpers.GetClosestPointTo(currentRoom.transform.position, entranceOne, entranceTwo));
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<NPC>() && numberOfNPC == 0)
		{
			foreach (Animator anim in doorsAnimators) anim.SetBool("IsOpen", true);
			numberOfNPC += 1;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<NPC>() && numberOfNPC == 1)
		{
			foreach (Animator anim in doorsAnimators) anim.SetBool("IsOpen", false);
			numberOfNPC -= 1;
		}
	}
}
