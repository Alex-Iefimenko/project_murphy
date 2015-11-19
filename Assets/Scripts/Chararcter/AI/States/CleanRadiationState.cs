using UnityEngine;
using System.Collections;

public class CleanRadiationState : StateBase {
	
	private int stateIndex = 16;
	
	public CleanRadiationState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		return room.State.IsRadioactive();
	}

	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(character.Movement.CurrentRoom);
	}
	
	public override void ExecuteStateActions () 
	{
		character.Movement.CurrentRoom.Deactivate(character.Stats.CleanRadiation);
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (character.Movement.CurrentRoom.State.IsRadioactive() == false)
			character.PurgeActions();
	}
	
}
