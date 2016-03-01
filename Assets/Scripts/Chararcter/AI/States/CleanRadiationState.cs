using UnityEngine;
using System.Collections;

public class CleanRadiationState : StateBase {
	
	private int stateIndex = 16;
	
	public CleanRadiationState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool EnableCondition (Room room) 
	{
		return room.Stats.IsRadioactive();
	}

	public override void Actualize () { 
		base.Actualize (); 
		character.Movement.Walk().ToPoint(character.Movement.CurrentRoom.Objects.GetRandomRoomPoint());
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		character.Movement.CurrentRoom.Deactivate(character.Stats.CleanRadiation);
		if (character.Movement.IsMoving == false)
			character.View.SetSubState(1);
	}
	
	public override bool DisableCondition () 
	{
		return character.Movement.CurrentRoom.Stats.IsRadioactive() == false;
	}
	
}
