using UnityEngine;
using System.Collections;

public class CleanRadiationState : StateBase {
	
	private new int stateIndex = 16;

	public override int StateKind { get { return this.stateIndex; } }
	
	public CleanRadiationState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (Room room) 
	{
		return room.Stats.IsRadioactive ();
	}

	public override void Actualize () { 
		base.Actualize (); 
		movement.Walk ().ToPoint (movement.CurrentRoom.Objects.GetRandomRoomPoint());
	}
	
	public override void Execute () 
	{
		base.Execute ();
		movement.CurrentRoom.Deactivate(stats.CleanRadiation);
		if (movement.IsMoving == false) OnSubStateChange (1);
	}
	
	public override bool DisableCondition () 
	{
		return movement.CurrentRoom.Stats.IsRadioactive() == false;
	}
	
}
