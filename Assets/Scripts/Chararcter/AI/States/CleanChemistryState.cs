using UnityEngine;
using System.Collections;

public class CleanChemistryState : StateBase {
	
	private int stateIndex = 17;
	
	public CleanChemistryState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		return room.Stats.IsHazardous();
	}

	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(character.Movement.CurrentRoom);
	}
	
	public override void ExecuteStateActions () 
	{
		character.Movement.CurrentRoom.ChemistryClearing(character.Stats.CleanChemistry);
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (character.Movement.CurrentRoom.Stats.IsHazardous() == false)
			character.PurgeActions();
	}
	
}
