using UnityEngine;
using System.Collections;

public class Scrolling : MonoBehaviour {

	public float speed = 1;
	public bool horizontal = false;
	private SpriteRenderer[] sprites;
	private Vector3 sidePoint;
	private Vector3 frontPoint;
	private Vector3 translation;

	// Use this for initialization
	void Start () {
		sprites = GetComponentsInChildren<SpriteRenderer> ();

		// set initial position
		int number = 0;
		foreach (SpriteRenderer sprite in sprites)
		{
			float x = (horizontal) ? transform.position.x + sprite.bounds.size.x * number : transform.position.x;
			float y = (!horizontal) ? transform.position.y + sprite.bounds.size.y * number : transform.position.y;
			sprite.transform.position = new Vector3(x, y, transform.position.z);
			number++;
		}

		SetTranslation (number);
	}

	// Applies translation rules for background
	void SetTranslation (int number)
	{
		sidePoint = transform.position;
		frontPoint = transform.position;
		if (horizontal)
		{
			sidePoint.x = sidePoint.x - sprites[0].bounds.size.x;
			frontPoint.x = frontPoint.x + sprites[0].bounds.size.x * (number -1);
			translation = new Vector3(speed, 0, 0);
		}
		else 
		{
			sidePoint.y = sidePoint.y - sprites[0].bounds.size.y;
			frontPoint.y = frontPoint.y + sprites[0].bounds.size.y * (number -1);
			translation = new Vector3(0, speed, 0);
		}
	}

	// Update is called once per frame
	void Update () {
		foreach (SpriteRenderer sprite in sprites)
		{
			sprite.transform.Translate(- translation * Time.deltaTime);
			if ((sprite.transform.position - sidePoint).magnitude <= 0.01) sprite.transform.position = frontPoint;
		}
	}


}
