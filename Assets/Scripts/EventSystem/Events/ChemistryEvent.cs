using UnityEngine;
using System.Collections;

public class ChemistryEvent : EventBase {
	
	public new float amount = 20f;
	public new int attempts = 5;
	
	public override void StopEvent () {}
	
	public override bool TryEvent ()
	{
		IRoom room = ShipState.Inst.RandomNamedRoom();
		return room.ForceState<ChemistryHazardRoomState>(amount);
	}
	
}