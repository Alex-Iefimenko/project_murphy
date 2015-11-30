using UnityEngine;

public class Furniture : MonoBehaviour {

	// Dummy class for furniture items identification

	// Property for room identification
//	[HideInInspector] public Room currentRoom;
	[HideInInspector] public bool isFree = true;

	// Add NPC to Rooms NPC List
	void OnTriggerEnter2D (Collider2D otherCollider) 
	{
		if (otherCollider.GetComponent<CharacterMain> () != null) 
			isFree = false;
	}
	
	// Delete NPC from Rooms NPC List
	void OnTriggerExit2D (Collider2D otherCollider) 
	{
		if (otherCollider.GetComponent<CharacterMain> () != null)
			isFree = true;
	}
}
