using UnityEngine;
using System.Collections.Generic;

public class Door : MonoBehaviour {

	private Animator doorAnimator;
	private int numberOfNPC = 0;

	// Use this for initialization
	void Awake () {
		doorAnimator = GetComponentInChildren<Animator>();
	}
	
	// Method adds Neighbor object to each rooms connected to this door
	public void ConnectNeighbors ()
	{
		Vector3[] entrances = GetEntrancePoints ();
		Vector3 entranceOne = entrances[0];
		Vector3 entranceTwo = entrances[1];
		Room neighborOne = null;
		Room neighborTwo = null;
		for (int i = 0; i < ShipState.Inst.allRooms.Length; i++)
		{
			if (ShipState.Inst.allRooms[i].collider2D.OverlapPoint(entranceOne))
			    neighborOne = ShipState.Inst.allRooms[i];
			if (ShipState.Inst.allRooms[i].collider2D.OverlapPoint(entranceTwo))
			    neighborTwo = ShipState.Inst.allRooms[i];
			if (neighborOne != null && neighborTwo != null) break;
		}
		if (neighborOne != null && neighborTwo != null)
		{
			neighborOne.AddNeighbor(neighborTwo, this);
			neighborTwo.AddNeighbor(neighborOne, this);
		}
	}

	public Vector3[] GetEntrancePoints ()
	{
		BoxCollider2D coll = GetComponent<BoxCollider2D>();
		Vector3 size = transform.up * coll.size.y / 2f * transform.localScale.y;
		return new Vector3[2] { transform.position + size, transform.position - size };
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Character>() != null)
		{
			if (numberOfNPC == 0) doorAnimator.SetBool("IsOpen", true);
			numberOfNPC += 1;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Character>() != null)
		{
			if (numberOfNPC == 1) doorAnimator.SetBool("IsOpen", false);
			numberOfNPC -= 1;
		}
	}
}
