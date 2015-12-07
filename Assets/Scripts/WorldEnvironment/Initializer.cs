using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

	// Use this for initialization step by step
	void Start () {
		ShipState.Inst.Init();
		// Generate NPC stats
		InitCharacters ();
		// Calls all neccessary function to build ship structure
		ShipState.Inst.CountCharacters();
	}

	void InitCharacters ()
	{
		// Predefined charcters
		GameObject[] allCharacters = GameObject.FindGameObjectsWithTag("Character");
		for (int i = 0; i < allCharacters.Length; i++) allCharacters[i].GetComponent<CharacterMain>().Init();
		// Generated Characters
		GameObject[] allCreators = GameObject.FindGameObjectsWithTag("CharacterCreater");
		for (int i = 0; i < allCreators.Length; i++) allCreators[i].GetComponent<CharacterCreater>().CreateCharacter();
	}
}
