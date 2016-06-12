using UnityEngine;
using System.Collections;

public class Beacon : MonoBehaviour {

	public float beaconTimeout = 15f;
	private float currentTimeout = 0f;
	public Texture2D requestButton;
	private GUIStyle buttonStyle = GUIStyle.none;
	private SpriteRenderer dockingAnimation;
	private GameObject vessel;
	private bool request = false;
	private bool docked = false;

	public bool Free { get { return !request && !docked; } }

	void Awake () 
	{
		dockingAnimation = GetComponentInChildren<SpriteRenderer>();
		dockingAnimation.enabled = false;
	}

	void OnGUI ()
	{
		bool startDocking = false;
		if (currentTimeout > 0f) currentTimeout -= Time.deltaTime;
		if (request && !docked && currentTimeout <= 0f) request = false;
		if (request && !docked) startDocking = RequestButton ();
		if (startDocking) Dock ();
	}

	public void RequestDocking (GameObject ship)
	{
		vessel = ship;
		request = true;
		currentTimeout = beaconTimeout;
	}

	public void OnDock ()
	{
		dockingAnimation.enabled = false;
	}
	
	public void OnUnDock ()
	{
		vessel = null;
		request = false;
		docked = false;
	}

	private void Dock ()
	{
		dockingAnimation.enabled = true;
		docked = true;
		Vector3 directionVector = (transform.position - Vector3.zero) / Vector3.Distance(transform.position, Vector3.zero);
		directionVector = Quaternion.Euler(0, 0, Random.Range(-30, 30)) * directionVector;
		Vector3 instantiatePoint = transform.position + Random.Range(25, 35) * directionVector;
		GameObject ship = (GameObject)Instantiate(vessel, instantiatePoint, transform.rotation);
		IRoom room = ship.GetComponent<Room>() as IRoom;
		room.FlyUp(transform.position);
		room.Gateway = this;
	}

	private bool RequestButton ()
	{
		// Button Size
		Vector3 animFinfish = Camera.main.WorldToScreenPoint(dockingAnimation.bounds.max);
		Vector3 animStart = Camera.main.WorldToScreenPoint(dockingAnimation.bounds.min);
		float minSideSize = Mathf.Min(animFinfish.x - animStart.x, animFinfish.y - animStart.y);
		Vector2 size = new Vector2(minSideSize, minSideSize * requestButton.height / requestButton.width);
		// Button place
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		Vector2 position = new Vector2(screenPoint.x - size.x / 2f, Camera.main.pixelHeight - screenPoint.y  - size.y / 2f);
		// Button ready
		return GUI.Button(new Rect(position.x, position.y, size.x, size.y), requestButton, buttonStyle);
	}

}
