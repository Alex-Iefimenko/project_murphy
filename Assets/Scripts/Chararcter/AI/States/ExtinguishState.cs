using UnityEngine;
using System.Collections;

public class ExtinguishState : StateBase {

	private new int stateIndex = 7;

	public override int StateKind { get { return this.stateIndex; } }
	
	public ExtinguishState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return room.IsOnFire;
	}

	public override void Actualize () { 
		base.Actualize (); 
		movement.Walk ().ToPoint (movement.CurrentRoom.GetRandomRoomPoint());
	}
	
	public override void Execute () 
	{
		base.Execute ();
		movement.CurrentRoom.ReduceFire (stats.FireExtinguish);
		if (movement.IsMoving == false) OnSubStateChange (1);
	}
	
	public override bool DisableCondition () 
	{
		return movement.CurrentRoom.IsOnFire == false;
	}
}
