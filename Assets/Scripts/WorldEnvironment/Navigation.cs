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
	private ICharacter player;
	public ICharacter Player { get { return player; } set { player = value; } }

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
	// if ship was hidden by roof
	private bool hidden = true;
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
		if (!moved && !scaled) CameraSizeUpdate ();
		if (player != null && currentPoint && player.GObject.collider2D.bounds.Intersects(currentPoint.renderer.bounds)) 
			Destroy(currentPoint);

		// Editor debug
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) 
			MouseControl ();
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
		if (touchCount == 1) OneTouch ();
		else if (touchCount == 2) TwoTouches ();
		else
		{
			if (!isResetPreviously) Reset ();
		}
	}

	void OneTouch () 
	{
		isResetPreviously = false;
		Touch touch1 = Input.GetTouch(0);
		if (touch1.phase == TouchPhase.Began && lastFingerId == -1)
		{ 	 
			touchPosition = Camera.main.ScreenToWorldPoint(touch1.position);
			lastFingerId = touch1.fingerId;
		} 
		else if (touch1.phase == TouchPhase.Moved && lastFingerId == touch1.fingerId && !scaled)
		{
			moved = true;
			Move(Camera.main.ScreenToWorldPoint(touch1.position));
			touchPosition = Camera.main.ScreenToWorldPoint(touch1.position);
		}
		else if (touch1.phase == TouchPhase.Ended && !moved && !scaled)
		{
			RaycastHit2D hit;
			hit = Physics2D.Raycast (new Vector2(touchPosition.x, touchPosition.y), Vector2.zero, 20, layer);
			if (hit)  NavigatePlayerTo(hit);
			Reset ();
		}
	}

	void TwoTouches () 
	{
		isResetPreviously = false;
		Touch touch1 = Input.GetTouch(0);
		Touch touch2 = Input.GetTouch(1);
		if (touch2.phase == TouchPhase.Began)
		{
			scaled = true;
			touchDistance = Vector2.Distance(touch1.position, touch2.position);
		}
		else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
		{
			Scale(Vector2.Distance(touch1.position, touch2.position));
			touchDistance = Vector2.Distance(touch1.position, touch2.position);
			if (!hidden && Camera.main.orthographicSize >= cameraSizeMax - 1f) HideShip ();
			else if (hidden) ShowShip ();
		}

	}

	void NavigatePlayerTo(RaycastHit2D hit)
	{
		player.Navigate(hit.collider.GetComponent<Room>() as IRoom);
		if (currentPoint) Destroy(currentPoint);
		Vector3 position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, player.GObject.transform.position.z);
		currentPoint = (GameObject)Instantiate(pointer, position, Quaternion.identity);
	}

	void Scale (float newDistance)
	{
		float result = (touchDistance - newDistance) * Time.deltaTime;
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + result, cameraSizeMin, cameraSizeMax);
	}

	void Move (Vector3 newPosition)
	{
		Vector3 result = (touchPosition - newPosition) * speed;
		// Block far camera movement
		Bounds cameraBound = CameraPositionBounds ();
		result.x = Mathf.Clamp (Camera.main.transform.position.x + result.x, cameraBound.min.x, cameraBound.max.x);
		result.y = Mathf.Clamp (Camera.main.transform.position.y + result.y, cameraBound.min.y, cameraBound.max.y);
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
			bool walks = hit && player != null && player.IsActive;
			if (walks) NavigatePlayerTo (hit);
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			Scale(speed / Time.deltaTime);
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			Scale(-speed / Time.deltaTime);
			if (!hidden && Camera.main.orthographicSize >= cameraSizeMax - 1f) HideShip ();
			else if (hidden) ShowShip ();
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
		else if (hidden)
		{
			ShowShip ();
		}

		Bounds cameraBounds = CameraPositionBounds ();
		Vector3 position = Camera.main.transform.position;
		if (Camera.main.transform.position.x <= cameraBounds.min.x * 0.8f && isResetPreviously)
		{
			position.x += 10f * speed * Time.deltaTime;
		}
		else if (Camera.main.transform.position.x >= cameraBounds.max.x * 0.8f && isResetPreviously)
		{
			position.x -= 10f * speed * Time.deltaTime;
		}
		if (Camera.main.transform.position.y <= cameraBounds.min.y * 0.8f && isResetPreviously)
		{
			position.y += 10f * speed * Time.deltaTime;
		}
		else if (Camera.main.transform.position.y >= cameraBounds.max.y * 0.8f && isResetPreviously)
		{
			position.y -= 10f * speed * Time.deltaTime;
		}
		Camera.main.transform.position = position;
	}


	Vector2 CameraSize ()
	{
		Vector2 cameraSize = new Vector2 (
			Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth, Camera.main.pixelHeight)).x - Camera.main.ScreenToWorldPoint(new Vector3 (0,0)).x, 
			Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth, Camera.main.pixelHeight)).y - Camera.main.ScreenToWorldPoint(new Vector3 (0,0)).y
			);
		return cameraSize;
	}

	Bounds CameraPositionBounds ()
	{
		Vector2 cameraSize = CameraSize ();
		Vector3 size = new Vector3(2 * (ship.renderer.bounds.extents.x + cameraSize.x * 0.4f), 
		                           2 * (ship.renderer.bounds.extents.y + cameraSize.y * 0.4f), 
		                           0);
		Bounds result = new Bounds(ship.transform.position, size);
		return result;
	}

	void HideShip ()
	{
		for (int i = 0; i < ShipState.Inst.allRooms.Length; i++) ShipState.Inst.allRooms[i].ChangeRoof(true);
		hidden = true;
	}

	void ShowShip ()
	{
		for (int i = 0; i < ShipState.Inst.allRooms.Length; i++) ShipState.Inst.allRooms[i].ChangeRoof(false);
		hidden = false;
	}
}
