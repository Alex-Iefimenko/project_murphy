using UnityEngine;
using System.Collections;

public class CleanChemistryState : StateBase {
	
	private new int stateIndex = 17;

	public override int StateKind { get { return this.stateIndex; } }
	
	public CleanChemistryState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (Room room) 
	{
		return room.Stats.IsHazardous();
	}

	public override void Actualize () { 
		base.Actualize (); 
		movement.Walk ().ToPoint (movement.CurrentRoom.Objects.GetRandomRoomPoint());
	}
	
	public override void Execute () 
	{
		base.Execute ();
		movement.CurrentRoom.ChemistryClearing (stats.CleanChemistry);
		if (movement.IsMoving == false) OnSubStateChange (1);
	}
	
	public override bool DisableCondition () 
	{
		return movement.CurrentRoom.Stats.IsHazardous() == false;
	}
	
}
