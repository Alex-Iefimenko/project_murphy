using UnityEngine;
using System.Collections;

public class CleanChemistryState : StateBase {
	
	private new int stateIndex = 17;

	public override int StateKind { get { return this.stateIndex; } }
	
	public CleanChemistryState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return room.IsHazardous;
	}

	public override void Actualize () { 
		base.Actualize (); 
		movement.Walk ().ToPoint (movement.CurrentRoom.GetRandomRoomPoint());
	}
	
	public override void Execute () 
	{
		base.Execute ();
		movement.CurrentRoom.CleanHazard (stats.CleanChemistry);
		if (movement.IsMoving == false) OnSubStateChange (1);
	}
	
	public override bool DisableCondition () 
	{
		return movement.CurrentRoom.IsHazardous == false;
	}
	
}
