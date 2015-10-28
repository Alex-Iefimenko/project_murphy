using UnityEngine;
using System.Collections;

public class RepairState : StateBase {

	private int stateIndex = 6;
	
	public RepairState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(character.Movement.CurrentRoom);
	}
	
	public override void ExecuteStateActions () 
	{
		character.Movement.CurrentRoom.Repair(character.Stats.Repair);
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (character.Movement.CurrentRoom.State.IsBroken() == false)
			character.PurgeActions();
	}

}
