using UnityEngine;
using System.Collections;

public class SpaceObjectCall : EventBase {
	
	public GameObject vessel;
	public new int attempts = 1;
	
	public override void StopEvent () {}
	
	public override bool TryEvent ()
	{
		GameObject[] beacons = GameObject.FindGameObjectsWithTag("Beacon");
		for (int i = 0; i < beacons.Length; i++)
		{
			Beacon beacon = beacons[i].GetComponent<Beacon>();
			if (beacon.Free) 
			{
				beacon.RequestDocking(vessel);
				return true;
			}
		}
		return false;
	}
	
}