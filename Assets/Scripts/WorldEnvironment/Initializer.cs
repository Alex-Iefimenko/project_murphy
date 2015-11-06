using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

	// Use this for initialization step by step
	void Start () {
		// Calls all neccessary function to build ship structure
		InitShip ();
		// Generate NPC stats
		InitCharacters ();
	}
	
	// Initialize vars and build ship graph
	void InitShip ()
	{
		ShipState.Inst.Init();
	}

	void InitCharacters ()
	{
		for (int i = 0; i < ShipState.Inst.allCharacters.Length; i++) ShipState.Inst.allCharacters[i].Init();
	}
}
