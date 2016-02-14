using UnityEngine;
using System.Collections;

public class OtherShip : MonoBehaviour {

	public GameObject traderShip;
	public bool startTrader = false;
	public GameObject pirateShip;
	public bool startPirate = false;

	// Update is called once per frame
	void Update () {
		if (startTrader)
		{
			startTrader = false;
			GameObject ship = (GameObject)Instantiate(traderShip, new Vector3(22f, -10f, 0f), transform.rotation);
			Room room = ship.GetComponent<Room>();
			room.Flier.FlyUp(GameObject.FindGameObjectWithTag("Beacon").transform.position);
		}

		if (startPirate)
		{
			startPirate = false;
			GameObject ship = (GameObject)Instantiate(pirateShip, new Vector3(22f, -10f, 0f), transform.rotation);
			Room room = ship.GetComponent<Room>();
			room.Flier.FlyUp(GameObject.FindGameObjectWithTag("Beacon").transform.position);
		}
	}
}
