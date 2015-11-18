using UnityEngine;
using System.Collections;

public class CleanChemistryState : StateBase {
	
	private int stateIndex = 17;
	
	public CleanChemistryState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(character.Movement.CurrentRoom);
	}
	
	public override void ExecuteStateActions () 
	{
		character.Movement.CurrentRoom.ChemistryClearing(character.Stats.CleanChemistry);
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (character.Movement.CurrentRoom.State.IsHazardous() == false)
			character.PurgeActions();
	}
	
}
