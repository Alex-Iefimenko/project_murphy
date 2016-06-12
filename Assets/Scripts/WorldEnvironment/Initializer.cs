using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

	// Use this for initialization step by step
	void Start () {
		Ship.Inst.Init();
		// Generate NPC stats
		InitCharacters ();
		// Calls all neccessary function to build ship structure
		Ship.Inst.CountCharacters();
	}

	void InitCharacters ()
	{
		// Generated Characters
		GameObject[] allCreators = GameObject.FindGameObjectsWithTag("CharacterCreater");
		for (int i = 0; i < allCreators.Length; i++) allCreators[i].GetComponent<CharacterCreater>().CreateCharacter();
		// Generated Characters
		GameObject[] groupsCreators = GameObject.FindGameObjectsWithTag("CharacterGroupCreater");
		for (int i = 0; i < groupsCreators.Length; i++) groupsCreators[i].GetComponent<CharacterGroupCreater>().CreateCharacters();
	}
}
		                                 