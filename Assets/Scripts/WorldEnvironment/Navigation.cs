using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour 
{
	// Camera Navigation Params
	public float speed = 1f;
	public float cameraSizeMin = 2f;
	public float cameraSizeMax = 10f;

	// Navigation view
	public GameObject pointer;
	private GameObject currentPoint;

	// World identifier
	private GameObject ship;
	private string layerName = "Ship";

	// Player object
	private CharacterMain player;
	public CharacterMain Player { get { return player; } set { player = value; } }

	// Touch Controll
	// last finger touch id
	private int lastFingerId = -1;
	// last touch position in px (screen dimention)
	private Vector3 touchPosition;
	// last touches distance
	private float touchDistance;
	// if camera was scaled
	private bool scaled = false;
	// if world was scaled
	private bool moved = false;
	// is touch control is in reset state
	private bool isResetPreviously  = true;
	// Mask for filtering depth of raycast
	private int layer;


	void Awake ()
	{
		layer = 1 << LayerMask.NameToLayer(layerName);
		ship = GameObject.FindGameObjectWithTag("Ship");
	}

	// Update is called once per frame
	void Update () 
	{
		TouchControl ();

		// Editor debug
		//if (Application.platform == RuntimePlatform.WindowsEditor) 
		MouseControl ();
		if (!moved && !scaled) CameraSizeUpdate ();
		if (player && currentPoint && player.collider2D.bounds.Intersects(currentPoint.renderer.bounds)) 
			Destroy(currentPoint);
	}

	// Reset state of touch control
	public void Reset ()
	{
		lastFingerId = -1;
		isResetPreviously = true;
		scaled = false;
		moved = false;
	}
		
	// Touch control method
	void TouchControl () 
	{
		int touchCount  = Input.touchCount;
		// Touch control
		if (touchCount > 0)
		{
			isResetPreviously = false;
			Touch touch1 = Input.GetTouch(0);
			Touch touch2 = Input.GetTouch(1);
			touchPosition = Camera.main.ScreenToWorldPoint(touch1.position);
			
			if (touch1.phase == TouchPhase.Began && lastFingerId == -1 )
			{ 	 
				lastFingerId = touch1.fingerId;
			} 
			else if (touchCount == 2 && touch2.phase == TouchPhase.Began)
			{
				scaled = true;
				touchDistance = Vector2.Distance(touch1.position, touch2.position);
			}
			else if (touchCount == 2 && (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved))
			{
				Scale(Vector2.Distance(touch1.position, touch2.position));
				touchDistance = Vector2.Distance(touch1.position, touch2.position);
			}
			else if (touch1.phase == TouchPhase.Moved && lastFingerId == touch1.fingerId && !scaled)
			{
				moved = true;
				Move(Camera.main.ScreenToWorldPoint(touch1.position));
			}
			else if (touchCount == 1 && touch1.phase == TouchPhase.Ended && !moved && !scaled)
			{
				RaycastHit2D hit;
				hit = Physics2D.Raycast (new Vector2(touchPosition.x, touchPosition.y), Vector2.zero, 20, layer);
				if (hit)  NavigatePlayerTo(hit);
				Reset ();
			}
		}
		else
		{
			if (!isResetPreviously) Reset ();
		}
	}


	void NavigatePlayerTo(RaycastHit2D hit)
	{
		player.Navigate(hit.collider.GetComponent<Room>(), true);
		if (currentPoint) Destroy(currentPoint);
		currentPoint = (GameObject)Instantiate(pointer, hit.collider.transform.position, Quaternion.identity);
	}

	void Scale (float newDistance)
	{
		float result = (touchDistance - newDistance) * Time.deltaTime;
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + result, cameraSizeMin, cameraSizeMax);
	}

	void Move (Vector3 newPosition)
	{
		Vector3 result = (newPosition - touchPosition) * speed;
		// Block far camera movement
		Vector2 cameraSize = new Vector2 (
			Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth, Camera.main.pixelHeight)).x - Camera.main.ScreenToWorldPoint(new Vector3 (0,0)).x, 
			Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth, Camera.main.pixelHeight)).y - Camera.main.ScreenToWorldPoint(new Vector3 (0,0)).y
			);
		// OX
		float xMin = ship.transform.position.x - ship.renderer.bounds.size.x / 2f - 0.35f * cameraSize.x;
		float xMax = ship.transform.position.x + ship.renderer.bounds.size.x / 2f + 0.35f * cameraSize.x;
		result.x = Mathf.Clamp (Camera.main.transform.position.x + result.x, xMin, xMax);
		// OY
		float yMin = ship.transform.position.y - ship.renderer.bounds.size.y / 2f - 0.35f * cameraSize.y;
		float yMax = ship.transform.position.y + ship.renderer.bounds.size.y / 2f + 0.345f * cameraSize.y;
		result.y = Mathf.Clamp (Camera.main.transform.position.y + result.y, yMin, yMax);
		result.z = Camera.main.transform.position.z;
		Camera.main.transform.position = result;
	}

	// Alias method for debug in editor. 
	// Dublicates functionality of TouchControl for mouse
	void MouseControl ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else if (Input.GetMouseButton(0))
		{
			Move(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Vector3 worldTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit;
			hit = Physics2D.Raycast (new Vector2(worldTouch.x, worldTouch.y), Vector2.zero, 20, layer);
			if (hit && player) NavigatePlayerTo (hit);
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			Scale(speed / Time.deltaTime);
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			Scale(-speed / Time.deltaTime);
		}
	}

	// Updates  camera resize
	void CameraSizeUpdate ()
	{
		if (Camera.main.orthographicSize <= cameraSizeMin + 0.5f && isResetPreviously)
		{
			Camera.main.orthographicSize += 2 * speed * Time.deltaTime;
		}
		else if (Camera.main.orthographicSize >= cameraSizeMax - 1f && isResetPreviously)
		{
			Camera.main.orthographicSize -= 2 * speed * Time.deltaTime;
		}
	}
}
