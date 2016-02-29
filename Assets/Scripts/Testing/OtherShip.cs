using UnityEngine;
using System.Collections;

public class OtherShip : MonoBehaviour {
	
	public bool startTrader = false;
	public bool startPirate = false;

	private EventSource eventSource;

	void Awake ()
	{
		eventSource = GameObject.FindGameObjectWithTag("EventSource").GetComponent<EventSource>();
	}

	// Update is called once per frame
	void Update () {
		if (startTrader)
		{
			startTrader = false;
			eventSource.ForceState("LightTraders");
		}

		if (startPirate)
		{
			startPirate = false;
			eventSource.ForceState("MediumTerrorist");
		}
	}
}
