using UnityEngine;
using System.Collections;

public class OtherShip : MonoBehaviour {

	public GameObject prefab;
	public bool start = false;


	// Update is called once per frame
	void Update () {
		if (start)
		{
			start = false;
			GameObject ship = (GameObject)Instantiate(prefab, new Vector3(22f, -10f, 0f), transform.rotation);
			Room room = ship.GetComponent<Room>();
			room.Flier.Fly(GameObject.FindGameObjectWithTag("Beacon").transform.position, false);
		}
	}
}
