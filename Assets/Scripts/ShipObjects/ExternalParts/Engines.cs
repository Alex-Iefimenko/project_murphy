using UnityEngine;
using System.Collections;

public class Engines : MonoBehaviour {

	private ParticleSystem[] particalSystem;

	// Use this for initialization
	void Start () {
		particalSystem = GetComponentsInChildren<ParticleSystem>();
		SwitchOff ();
	}
	
	public void SwitchOn ()
	{
		for (int i = 0; i < particalSystem.Length; i++) particalSystem[i].enableEmission = true;
	}

	public void SwitchOff ()
	{
		for (int i = 0; i < particalSystem.Length; i++) particalSystem[i].enableEmission = false;
	}
}
