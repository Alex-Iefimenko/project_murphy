using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public float speed = 0.05f;
	private Vector2 direction = new Vector2(1, 0);
	public GameObject Target { get; set; }

	// Update is called once per frame
	void Update () {
		Vector3 transition = new Vector3(direction.x * speed, direction.y * speed, 0f) * Time.deltaTime;
		transform.Translate(transition);
	}

	void OnTriggerEnter2D (Collider2D otherCollider)
	{
		if (otherCollider.gameObject == Target) Destroy (this.gameObject);
	}
}
