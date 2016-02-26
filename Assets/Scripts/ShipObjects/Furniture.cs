using UnityEngine;

public class Furniture : MonoBehaviour {

	// Dummy class for furniture items identification

	// Property for room identification
	private bool isFree = true;
	public enum Directions{ Auto = 0, Left = 1, Right = 2, Up = 3, Down = 4 };
	public Directions direction = Directions.Auto;
	public string Name { get; set; }
	public Vector3 Position { get; set; }
	public Vector3 Direction { get; set; }
	public bool IsFree { get { return isFree; } set { isFree = value; } }

	// Getting position and furniture rotation direction
	void Awake ()
	{
		Name = transform.parent.gameObject.name;
		Position = transform.position;
		Direction = GetFurnitureDirection();
	}

	// Add NPC to Rooms NPC List
	void OnTriggerEnter2D (Collider2D otherCollider) 
	{
		if (otherCollider.GetComponent<CharacterMain> () != null) 
			isFree = false;
	}
	
	// Delete NPC from Rooms NPC List
	void OnTriggerExit2D (Collider2D otherCollider) 
	{
		if (otherCollider.GetComponent<CharacterMain> () != null)
			isFree = true;
	}

	Vector3 GetFurnitureDirection()
	{
		Vector3 result = transform.position;
		{
			switch (direction)
			{
			case Directions.Auto:
				result = transform.parent.position;
				break;
			case Directions.Down:
				result += new Vector3(0, -1, 0);
				break;
			case Directions.Up:
				result += new Vector3(0, 1, 0);
				break;
			case Directions.Left:
				result += new Vector3(-1, 0, 0);
				break;
			case Directions.Right:
				result += new Vector3(1, 0, 0);
				break;
			}
		}
		return result;
	}
		

}