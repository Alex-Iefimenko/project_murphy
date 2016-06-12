using UnityEngine;
using System.Collections;

public class Roof : MonoBehaviour {

	private Animator roofAnimator;
	private SpriteRenderer roofRenderer;

	// Use this for initialization
	void Awake () {
		roofRenderer = GetComponent<SpriteRenderer>();
		roofRenderer.sortingOrder = 32767;
		roofAnimator = GetComponent<Animator>();
		Switch (true);
		RoomMotion roomMotion = GetComponentInParent<RoomMotion>();
		if (roomMotion != null) roomMotion.OnRoofChange += Switch;
	}
	
	public void Switch(bool state)
	{
		if (roofAnimator.GetBool("RoofEnabled") != state) roofAnimator.SetBool("RoofEnabled", state);
	}

}
