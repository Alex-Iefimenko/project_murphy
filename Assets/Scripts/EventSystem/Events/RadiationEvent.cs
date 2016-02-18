﻿using UnityEngine;
using System.Collections;

public class RadiationEvent : EventBase {
	
	public new float amount = 20f;
	public new int attempts = 5;
	
	public override void StopEvent () {}
	
	public override bool TryEvent ()
	{
		Room room = ShipState.Inst.RandomRoom();
		return room.SatesHandler.ForceState<RadiationHazardRoomState>(amount);
	}
	
}