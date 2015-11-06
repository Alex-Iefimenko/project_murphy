using UnityEngine;
using System.Collections;

public class WorldTime : MonoBehaviour {
	
	public float dayLength = 30f;
	public float actionLength = 5f;
	public float tickLength = 0.5f;

	private float day = 30f;
	private float action = 5f;
	private float tick = 0.5f;

	// Update is called once per frame
	void Update () {
		day -= Time.deltaTime;
		action -= Time.deltaTime;
		tick -= Time.deltaTime;

		SendTimeEvent ();
	}

	// Detect if world time event occurs
	void SendTimeEvent ()
	{
		if (day <= 0f) 
		{
			day = dayLength;
			Broadcaster.Instance.BroadcastDay();
		}
		if (action <= 0f) 
		{
			action = actionLength;
			Broadcaster.Instance.BroadcastAction();
		}
		if (tick <= 0f)
		{
			tick = tickLength;
			Broadcaster.Instance.BroadcastTick();
		}
	}
}
