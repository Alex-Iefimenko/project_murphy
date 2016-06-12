using UnityEngine;
using System.Collections;

public class Engines : MonoBehaviour {

	private ParticleSystem[] particalSystem;

	// Use this for initialization
	void Awake () {
		particalSystem = GetComponentsInChildren<ParticleSystem>();
		Switch (false);
		RoomMotion roomMotion = GetComponentInParent<RoomMotion>();
		if (roomMotion != null) roomMotion.OnEnginesChange += Switch;
	}
	
	public void Switch (bool state)
	{
		for (int i = 0; i < particalSystem.Length; i++) particalSystem[i].enableEmission = state;
	}

}
