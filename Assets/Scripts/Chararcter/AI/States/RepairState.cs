using UnityEngine;
using System.Collections;

public class RepairState : StateBase {

	private new int stateIndex = 6;

	public override int StateKind { get { return this.stateIndex; } }

	public RepairState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return room.IsBroken;	
	}

	public override void Actualize () { 
		base.Actualize (); 
		movement.Walk ().ToFurniture (movement.CurrentRoom, "Random");
	}
	
	public override void Execute () 
	{
		base.Execute ();
		movement.CurrentRoom.Repair(stats.Repair);
		if (movement.IsMoving == false) OnSubStateChange(1);
	}
	
	public override bool DisableCondition () 
	{
		return movement.CurrentRoom.IsBroken == false;
	}

}
