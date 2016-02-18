using UnityEngine;
using System.Collections;

public class Roof : MonoBehaviour {

	private Animator roofAnimator;
	private SpriteRenderer roofRenderer;

	// Use this for initialization
	void Start () {
		roofRenderer = GetComponent<SpriteRenderer>();
		roofRenderer.sortingOrder = 32767;
		roofAnimator = GetComponent<Animator>();
	}
	
	public void ShowRoof()
	{
		if (!roofAnimator.GetBool("RoofEnabled")) roofAnimator.SetBool("RoofEnabled", true);
	}

	public void HideRoof()
	{
		if (roofAnimator.GetBool("RoofEnabled")) roofAnimator.SetBool("RoofEnabled", false);
	}
}
