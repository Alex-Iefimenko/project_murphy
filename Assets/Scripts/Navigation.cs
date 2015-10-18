using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour 
{
	// speed of Camera movement
	public float speed = 1f;
	// Min value of Camera size
	public float cameraSizeMin = 2f;
	// Max value of Camera size 
	public float cameraSizeMax = 10f;
	// ShipObject
	private GameObject ship; 
	public GameObject prefab; 
	// last finger touch id
	private int lastFingerId = -1;
	// last touch position in px (screen dimention)
	private Vector3 touchPosition;
	// last touches distance
	private float touchDistance;
	// is touch control is in reset state
	private bool isResetPreviously  = true;
	// initial room touch
	//private Room initialRoom;

	// Player object
	private GameObject player;
	// Mask for filtering depth of raycast
	private string layerName = "Ship";
	private int layer;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
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
		CameraSizeUpdate ();

	}

	// Reset state of touch control
	public void Reset ()
	{
		lastFingerId = -1;
		isResetPreviously = true;
	}
		
	// Touch control method
	void TouchControl () 
	{
//		int touchCount  = Input.touchCount;
//		if (touchCount > 0) isResetPreviously = false;
//		// Touch control for one finger input: slide, move, info
//		if (touchCount == 1)
//		{
//			Touch touch = Input.GetTouch(0);
//			// Beginning of touch phase: 
//			if (touch.phase == TouchPhase.Began && lastFingerId == -1 )
//			{ 	 
//				lastFingerId = touch.fingerId;
//				touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
//			} 
//			// Touch moved: camera moving
//			else if (touch.phase == TouchPhase.Moved && lastFingerId == touch.fingerId)
//			{
//				Camera.main.transform.position += (Camera.main.ScreenToWorldPoint(touch.position) - touchPosition) * speed;
//				touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
//			}
//			// Touch ended:
//			else if (touch.phase == TouchPhase.Ended)
//			{
//				Vector3 worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
//				RaycastHit2D hit;
//				hit = Physics2D.Raycast (new Vector2(worldTouch.x,worldTouch.y), Vector2.zero, 20, layer);
//				if (hit) 
//				{
//					Movement playerMovement = player.GetComponent<Movement>();
//					playerMovement.NewMovementPath(hit.collider.GetComponent<Room>(), false);
//				}
//				Reset ();
//			}
//		}
//		// touch control for twi finger input: screen resize
//		else if (touchCount == 2)
//		{
//			isResetPreviously = false;
//			Touch touch1 = Input.GetTouch(0);
//			Touch touch2 = Input.GetTouch(1);
//
//			// Beginning of touches phase: Get initial distance between fingers
//			if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began) 
//			{
//				touchDistance = Vector2.Distance(touch1.position, touch2.position);
//			}
//			// Touches moved: Changes Camera size consider distance delta
//			else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved) 
//			{
//
//				Camera.main.orthographicSize += (touchDistance - Vector2.Distance(touch1.position, touch2.position)) * Time.deltaTime;
//				touchDistance = Vector2.Distance(touch1.position, touch2.position);
//			}
//			// Touch ended: reset state
//			else if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
//			{
//				Reset ();
//			}
//		}
//		else
//		{
//			if (!isResetPreviously) Reset ();
//		}
	}

	// Alias method for debug in editor. 
	// Dublicates functionality of TouchControl for mouse
	void MouseControl ()
	{
//		if (Input.GetMouseButtonDown(0))
//		{
//			touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		}
//		else if (Input.GetMouseButton(0))
//		{
//			Camera.main.transform.position += (Camera.main.ScreenToWorldPoint(Input.mousePosition) - touchPosition) * speed;
//			touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		}
//		else if (Input.GetMouseButtonUp(0))
//		{
//			Vector3 worldTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			RaycastHit2D hit;
//			hit = Physics2D.Raycast (new Vector2(worldTouch.x,worldTouch.y), Vector2.zero, 20, layer);
//			if (hit && player) 
//			{
//				Movement playerMovement = player.GetComponent<Movement>();
//				playerMovement.NewMovementPath(hit.collider.GetComponent<Room>(), false);
//			}
//		}
//
//		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
//		{
//			Camera.main.orthographicSize += speed;
//		}
//		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
//		{
//			Camera.main.orthographicSize -= speed;
//		}

	}

	// Updates  camera resize
	void CameraSizeUpdate ()
	{
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, cameraSizeMin, cameraSizeMax);
		if (Camera.main.orthographicSize <= cameraSizeMin + 0.5f && isResetPreviously)
		{
			Camera.main.orthographicSize += 2 * speed * Time.deltaTime;
		}
		else if (Camera.main.orthographicSize >= cameraSizeMax - 1f && isResetPreviously)
		{
			Camera.main.orthographicSize -= 2 * speed * Time.deltaTime;
		}

		// Block far camera movement
		Vector3 v3Camera = Camera.main.transform.position;
		Vector2 cameraSize = new Vector2 (
			Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth, Camera.main.pixelHeight)).x - Camera.main.ScreenToWorldPoint(new Vector3 (0,0)).x, 
			Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth, Camera.main.pixelHeight)).y - Camera.main.ScreenToWorldPoint(new Vector3 (0,0)).y
			);

		// OX
		float xMin = ship.transform.position.x - ship.renderer.bounds.size.x / 2f - 0.45f * cameraSize.x;
		float xMax = ship.transform.position.x + ship.renderer.bounds.size.x / 2f + 0.45f * cameraSize.x;
		v3Camera.x = Mathf.Clamp (v3Camera.x, xMin, xMax);
		
		// OY
		float yMin = ship.transform.position.y - ship.renderer.bounds.size.y / 2f - 0.45f * cameraSize.y;
		float yMax = ship.transform.position.y + ship.renderer.bounds.size.y / 2f + 0.45f * cameraSize.y;
		v3Camera.y = Mathf.Clamp (v3Camera.y, yMin, yMax);

		Camera.main.transform.position = v3Camera;
	}
}
