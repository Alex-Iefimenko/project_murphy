using UnityEngine;
using System.Collections;

public class CleanRadiationState : StateBase {
	
	private int stateIndex = 16;
	
	public CleanRadiationState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		return room.Stats.IsRadioactive();
	}

	public override void Actualize () { 
		base.Actualize (); 
		character.Movement.Walk().ToPoint(character.Movement.CurrentRoom.Objects.GetRandomRoomPoint());
	}
	
	public override void ExecuteStateActions () 
	{
		character.Movement.CurrentRoom.Deactivate(character.Stats.CleanRadiation);
		if (character.Movement.IsMoving == false)
			character.View.SetSubState(1);
		base.ExecuteStateActions ();
	}
	
	public override bool PurgeCondition () 
	{
		return character.Movement.CurrentRoom.Stats.IsRadioactive() == false;
	}
	
}
